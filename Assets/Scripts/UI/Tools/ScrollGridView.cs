/// <summary>
/// Base on UGUI ScrollView and GridLayoutGroup. Scroll it as "grid style".
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class ScrollGridView : MonoBehaviour, IBeginDragHandler ,IEndDragHandler, IDragHandler
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

    public ScrollRect scrollRect;
    public GridLayoutGroup gridLayoutGroup;

    private TweenRectTransformMoveTo gridMover;

    private int gridIndex = 0;
    public int Index
    {
        set
        {
            gridIndex = value;
        }
        get
        {
            return gridIndex;
        }
    }
    private int gridIndexLast = 0;

    private ContentSizeFitter contentSizeFitter;

    private Vector2 beginFingerDragPoint;
    private Vector2 endFingerDragPoint;

    /// <summary>
    /// Raises the begin drag event.
    /// "Don't" call this function by your own, it depend on Unity's callback system
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnBeginDrag(PointerEventData data)
    {
        beginFingerDragPoint = data.position;
    }

    /// <summary>
    /// Raises the end drag event.
    /// "Don't" call this function by your own, it depend on Unity's callback system
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnEndDrag(PointerEventData data)
    {
        endFingerDragPoint = data.position;

        gridIndex = CalcGridIndex();

        if (statusNow == States.UserScrolling)
        {
            statusNow = States.TweenToPosition;
        }
    }

    /// <summary>
    /// Raises the drag event.
    /// "Don't" call this function by your own, it depend on Unity's callback system
    /// </summary>
    /// <param name="data">Data.</param>
    public void OnDrag(PointerEventData data)
    {
        // if it is scrolling by tween position, then igorne it, otherwise, it is scrolling by user's fingers
        if (statusNow != States.TweenToPosition)
        {
            statusNow = States.UserScrolling;
        }
    }

    /// <summary>
    /// Gets the grids count.
    /// It help you to know what gridIndex number you can set (0 ~ (gridsCount - 1))
    /// </summary>
    /// <returns>The grids count.</returns>
    public int GetGridsCount()
    {
        float oneGridLegnth = GetOneGridLength();
        if (oneGridLegnth > 0f)
        {
            int gridsCount = (int)(GetAllGridsLength() / oneGridLegnth);
            return gridsCount;
        }
        else
        {
            return 0;
        }
    }

    private void OnTweenComplete()
    {
        gridMover.Callback.onCompleteEvent -= this.OnTweenComplete;

        // tween position complete, back to Idle
        if (statusNow == States.TweenToPosition)
            statusNow = States.Idle;
    }

    private float GetAllGridsLength()
    {
        float allCellsLength = 0f;
        RectTransform rt = scrollRect.content.gameObject.GetComponent<RectTransform>();
        switch (gridLayoutGroup.constraint)
        {
            case GridLayoutGroup.Constraint.FixedColumnCount:
                allCellsLength = rt.sizeDelta.y;
                break;
            case GridLayoutGroup.Constraint.FixedRowCount:
                allCellsLength = rt.sizeDelta.x;
                break;
            case GridLayoutGroup.Constraint.Flexible:
                Debug.LogError("can not use GridLayoutGroup.Constraint.Flexible. Destroy itself.");
                this.enabled = false;
                GameObject.Destroy(this);
                break;
            default:
                break;
        }
        return allCellsLength;
    }

    private float GetOneGridLength()
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

    private float GetAnchoredPosition()
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

    private int CalcGridIndex()
    {
        int gridsCount = GetGridsCount();

        if (gridsCount > 0)
        {
            float oneGridLength = GetOneGridLength();

            float anchoredPosition = GetAnchoredPosition();

            float posFactor = 0f;
            Vector2 fingerVector = endFingerDragPoint - beginFingerDragPoint;

            switch (gridLayoutGroup.constraint)
            {
                case GridLayoutGroup.Constraint.FixedColumnCount:
                    {
                        if (fingerVector.y > 0)
                        {
                            posFactor = oneGridLength * 0.9f;
                        }
                        else if (fingerVector.y < 0)
                        {
                            posFactor = oneGridLength * 0.1f;
                        }
                        else
                        {
                            return gridIndex;
                        }
                    }break;
                case GridLayoutGroup.Constraint.FixedRowCount:
                    {
                        if (fingerVector.x > 0)
                        {
                            posFactor = oneGridLength * 0.1f;
                        }
                        else if (fingerVector.x < 0)
                        {
                            posFactor = oneGridLength * 0.9f;
                        }
                        else
                        {
                            return gridIndex;
                        }
                    }break;
            }

            float pos = Mathf.Abs(anchoredPosition) + posFactor;

            int index = 0;
            while (index < gridsCount)
            {
                float leftVal = oneGridLength * (float)index;
                float rightVal = oneGridLength * (float)(index + 1);

                if (pos >= leftVal && pos < rightVal)
                {
                    break;
                }
                else
                {
                    index++;
                    if (index >= gridsCount)
                    {
                        index = gridsCount - 1;
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }
            }
            return index;
        }
        else
        {
            return 0;
        }
    }

    private Vector2 CalcGridIndexToPosition(int toIndex)
    {
        int gridsCount = GetGridsCount();
        if (gridsCount > 0)
        {
            float oneGridLength = GetOneGridLength();
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
                    this.enabled = false;
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
                    if (gridIndexLast != gridIndex)
                    {
                        statusNow = States.TweenToPosition;
                        break;
                    }
                }
                break;
            case States.UserScrolling:
                {
                    // there is nothing need to do here
                }
                break;
            case States.TweenToPosition:
                {
                    if (isStatusChanged)
                    {
                        // not allow user scroll it
                        scrollRect.enabled = false;

                        // check grid index is valid or not
                        int gridsCount = GetGridsCount();
                        if (gridIndex >= gridsCount)
                            gridIndex = gridsCount - 1;
                        else if (gridIndex < 0)
                            gridIndex = 0;

                        // remember last gridIndex
                        gridIndexLast = gridIndex;

                        // calculate the position it should tween to
                        Vector2 tweenPosTo = CalcGridIndexToPosition(gridIndex);

                        // setup the tween and run it
                        gridMover.useNowAsFrom = true;
                        gridMover.vectorTo = tweenPosTo;
                        gridMover.Callback.onCompleteEvent += this.OnTweenComplete;
                        gridMover.Run();
                        break;
                    }

                    // lock gridIndex, it's not allow change gridIndex while tweening hasn't complete
                    gridIndex = gridIndexLast;

                }
                break;
        }
    }

    private void Start()
    {
        // setup ContentSizeFitter to minsize
        contentSizeFitter = gridLayoutGroup.gameObject.GetComponent<ContentSizeFitter>();
        if (contentSizeFitter == null)
            contentSizeFitter = gridLayoutGroup.gameObject.AddComponent<ContentSizeFitter>();
        contentSizeFitter.horizontalFit = ContentSizeFitter.FitMode.MinSize;
        contentSizeFitter.verticalFit = ContentSizeFitter.FitMode.MinSize;

        // setup ScrollRect
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
                this.enabled = false;
                GameObject.Destroy(this);
                break;
            default:
                break;
        }

        // setup GridLayoutGroup
        RectTransform rt = gridLayoutGroup.gameObject.GetComponent<RectTransform>();
        rt.anchorMin = new Vector2(0, 1);
        rt.anchorMax = new Vector2(0, 1);
        rt.pivot = new Vector2(0, 1);
        rt.anchoredPosition = Vector2.zero;

        // setup tween (mover)
        gridMover = this.gameObject.AddComponent<TweenRectTransformMoveTo>();
        gridMover.tweenTarget = scrollRect.content.gameObject;
        gridMover.autoStart = false;
        gridMover.Loop = iTween.LoopType.none;
        gridMover.Ease = iTween.EaseType.easeOutCubic;
        gridMover.delay = 0f;
        gridMover.duration = 0.25f;
    }

    private void Update()
    {
        if (gridLayoutGroup.constraintCount <= 0)
        {
            Debug.LogError("gridLayoutGroup.constraintCount <= 0; it must greater than 0, destroy itself.");
            this.enabled = false;
            GameObject.Destroy(this);
            return;
        }

        UpdateStatus();
    }
}
