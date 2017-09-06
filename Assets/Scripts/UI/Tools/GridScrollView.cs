﻿/// <summary>
/// Base on UGUI ScrollView and GridLayoutGroup. Scroll it as "grid style".
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class GridScrollView : MonoBehaviour, IBeginDragHandler, IEndDragHandler
{
    public enum States
    {
        Idle,
        UserScrolling,
        TweenToPosition,
    }
    private States statusNow = States.Idle;
    public States Status { get { return statusNow; } }
    private States statusLast = States.TweenToPosition;

    public GridLayoutGroup gridLayoutGroup;
    private ScrollRect scrollRect;
    public ScrollRect ScrollRectRef { get { return scrollRect; } }

    private TweenRectTransformMoveTo gridMover;

    public int index = 0;
    private int indexLast = 0;

    /// <summary>
    /// The turn page threshold. 0.0f < value < 1.0f
    /// </summary>
    public float turnPageThreshold = 0.1f;
    public float turnPageDuration = 0.25f;

    public delegate void IndexChangeHandler(int indexNow, int indexLast);
    public event IndexChangeHandler onIndexChangeEvent;

    private ContentSizeFitter contentSizeFitter;

    private Vector2 beginFingerDragPoint;
    private Vector2 endFingerDragPoint;

    /// <summary>
    /// Gets the grids count.
    /// It help you to know what gridIndex number you can set (0 ~ (gridsCount - 1))
    /// </summary>
    /// <returns>The grids count.</returns>
    public int GetGridsCount()
    {
        return GridsCount();
    }

    /// <summary>
    /// Raises the begin drag event.
    /// "Don't" call this function by your own, it depend on Unity's callback system
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnBeginDrag(PointerEventData data)
    {
        if (statusNow == States.Idle)
        {
            beginFingerDragPoint = data.position;
            statusNow = States.UserScrolling;
        }
    }

    /// <summary>
    /// Raises the end drag event.
    /// "Don't" call this function by your own, it depend on Unity's callback system
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnEndDrag(PointerEventData data)
    {
        if (statusNow == States.UserScrolling)
        {
            endFingerDragPoint = data.position;
            index = CalcGridIndexViaPosition();

            statusNow = States.TweenToPosition;
        }
    }

    private void OnTweenComplete()
    {
        gridMover.Callback.onCompleteEvent -= this.OnTweenComplete;

        // tween position complete, back to Idle
        if (statusNow == States.TweenToPosition)
            statusNow = States.Idle;
    }

    private int GridsCount()
    {
        return gridLayoutGroup.transform.childCount;
    }

    private float OneGridLength()
    {
        float oneCellLegnth = 0f;
        switch (gridLayoutGroup.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                oneCellLegnth = gridLayoutGroup.cellSize.y + gridLayoutGroup.spacing.y;
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                oneCellLegnth = gridLayoutGroup.cellSize.x + gridLayoutGroup.spacing.x;
                break;
            case GridLayoutGroup.Constraint.Flexible:
                Debug.LogError("can not use GridLayoutGroup.Constraint.Flexible. Destroy itself.");
                this.enabled = false;
                GameObject.Destroy(this);
                break;
            default:
                break;
        }
        return oneCellLegnth;
    }

    private float AnchoredPosition()
    {
        float anchoredPosition = 0f;
        RectTransform rt = scrollRect.content.gameObject.GetComponent<RectTransform>();
        switch (gridLayoutGroup.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                anchoredPosition = rt.anchoredPosition.y;
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                anchoredPosition = rt.anchoredPosition.x;
                break;
            case GridLayoutGroup.Constraint.Flexible:
                Debug.LogError("can not use GridLayoutGroup.Constraint.Flexible. Destroy itself.");
                this.enabled = false;
                GameObject.Destroy(this);
                break;
            default:
                break;
        }
        return anchoredPosition;
    }

    private int CalcGridIndexViaPosition()
    {
        int gridsCount = GridsCount();
        int origIndex = index;

        if (gridsCount > 0)
        {
            float oneGridLength = OneGridLength();

            float anchoredPosition = AnchoredPosition();

            float posFactor = 0f;
            Vector2 fingerVector = endFingerDragPoint - beginFingerDragPoint;

            switch (gridLayoutGroup.constraint)
            {
                case GridLayoutGroup.Constraint.FixedColumnCount:
                    {
                        if (fingerVector.y > 0)
                        {
                            posFactor = oneGridLength * (1f - turnPageThreshold);
                        }
                        else if (fingerVector.y < 0)
                        {
                            posFactor = oneGridLength * turnPageThreshold;
                        }
                        else
                        {
                            return origIndex;
                        }
                    }break;
                case GridLayoutGroup.Constraint.FixedRowCount:
                    {
                        if (fingerVector.x > 0)
                        {
                            posFactor = oneGridLength * turnPageThreshold;
                        }
                        else if (fingerVector.x < 0)
                        {
                            posFactor = oneGridLength * (1f - turnPageThreshold);
                        }
                        else
                        {
                            return origIndex;
                        }
                    }break;
            }

            float pos = Mathf.Abs(anchoredPosition) + posFactor;

            int calcIndex = 0;
            while (calcIndex < gridsCount)
            {
                float leftVal = oneGridLength * (float)calcIndex;
                float rightVal = oneGridLength * (float)(calcIndex + 1);

                if (pos >= leftVal && pos < rightVal)
                {
                    break;
                }
                else
                {
                    calcIndex++;
                    if (calcIndex >= gridsCount)
                    {
                        calcIndex = gridsCount - 1;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return calcIndex;
        }
        else
        {
            return 0;
        }
    }

    private Vector2 CalcPositionViaIndex(int toIndex)
    {
        int gridsCount = GridsCount();
        if (gridsCount > 0)
        {
            float oneGridLength = OneGridLength();
            float toPos = toIndex * oneGridLength;

            Vector2 toVec2 = Vector2.zero;

            RectTransform rt = scrollRect.content.gameObject.GetComponent<RectTransform>();
            switch (gridLayoutGroup.constraint)
            {
                case GridLayoutGroup.Constraint.FixedColumnCount:
                    toVec2 = new Vector2(rt.anchoredPosition.x, toPos);
                    break;
                case GridLayoutGroup.Constraint.FixedRowCount:
                    toVec2 = new Vector2(-toPos, rt.anchoredPosition.y);
                    break;
                case GridLayoutGroup.Constraint.Flexible:
                    Debug.LogError("can not use GridLayoutGroup.Constraint.Flexible. Destroy itself.");
                    GameObject.Destroy(this);
                    break;
                default:
                    break;
            }
            return toVec2;
        }
        else
        {
            return Vector2.zero;
        }
    }

    private void UpdateStatus()
    {
        bool isStatusChanged = false;
        if (statusLast != statusNow)
        {
            isStatusChanged = true;
            statusLast = statusNow;
        }
        switch (statusNow)
        {
            case States.Idle:
                {
                    if (isStatusChanged)
                    {
                        // allow user to scroll it
                        scrollRect.enabled = true;
                    }

                    // if gridIndex changed by the other script, ready to tween position
                    if (index != indexLast)
                    {
                        statusNow = States.TweenToPosition;
                        break;
                    }
                }
                break;
            case States.UserScrolling:
                {
                    index = indexLast;  // while user is scrolling, can't assign index via the other script
                }
                break;
            case States.TweenToPosition:
                {
                    if (isStatusChanged)
                    {
                        // not allow user scroll it
                        scrollRect.enabled = false;

                        // fix grid index if it is invalid
                        int gridsCount = GridsCount();
                        if (index >= gridsCount)
                            index = gridsCount - 1;
                        else if (index < 0)
                            index = 0;

                        // calculate the position it should tween to
                        Vector2 tweenPosTo = CalcPositionViaIndex(index);

                        // setup the tween and run it
                        gridMover.useNowAsFrom = true;
                        gridMover.vectorTo = tweenPosTo;
                        gridMover.Callback.onCompleteEvent += this.OnTweenComplete;
                        gridMover.Run();

                        // if index changed, send event let who want to know,
                        // only send this event at States.TweenToPosition status,
                        // bcz only this status really change grids position.
                        if (index != indexLast)
                        {
                            if (onIndexChangeEvent != null)
                                onIndexChangeEvent(index, indexLast);
                        }
                        // remember the gridIndex
                        indexLast = index;

                        break;
                    }

                    index = indexLast;  // while tween to position has not complete, can't assign index via the other script
                }
                break;
        }
    }

    private void Start()
    {
        // setup ScrollRect
        scrollRect = gameObject.GetComponent<ScrollRect>();
        if (scrollRect == null)
        {
            Debug.LogError("This script must be added on a game object with ScrollRect component. Now destroy itself");
            GameObject.Destroy(this);
            return;
        }
        switch (gridLayoutGroup.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                scrollRect.horizontal = false;
                scrollRect.vertical = true;
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                scrollRect.horizontal = true;
                scrollRect.vertical = false;
                break;
            case GridLayoutGroup.Constraint.Flexible:
                Debug.LogError("can not use GridLayoutGroup.Constraint.Flexible. Destroy itself.");
                GameObject.Destroy(this);
                return;
            default:
                break;
        }

        // setup GridLayoutGroup
        RectTransform rt = gridLayoutGroup.gameObject.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.anchoredPosition = Vector2.zero;

        // setup ContentSizeFitter to minsize
        contentSizeFitter = gridLayoutGroup.gameObject.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
            contentSizeFitter = gridLayoutGroup.gameObject.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        // setup tween (mover)
        gridMover = this.gameObject.AddComponent<TweenRectTransformMoveTo>();
        gridMover.tweenTarget = scrollRect.content.gameObject;
        gridMover.autoStart = false;
        gridMover.loop = iTween.LoopType.none;
        gridMover.ease = iTween.EaseType.easeOutCubic;
        gridMover.delay = 0f;
        gridMover.duration = turnPageDuration;
    }

    private void Update()
    {
        if (turnPageThreshold <= 0f)
            turnPageThreshold = 0.01f;
        if (turnPageThreshold >= 1f)
            turnPageThreshold = 0.99f;

        if (turnPageDuration < 0f)
            turnPageDuration = 0f;
        gridMover.duration = turnPageDuration;

        if (gridLayoutGroup.constraintCount <= 0)
        {
            Debug.LogError("gridLayoutGroup.constraintCount <= 0; it must greater than 0, destroy itself.");
            GameObject.Destroy(this);
            return;
        }

        UpdateStatus();
    }
}
