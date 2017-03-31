/// <summary>
/// Fold / Un-fold a RectTransform
/// You can fold a rect transform by UGUI Toggle, or assign 'fold' flag in your script directly.
/// Author: BoJue.
/// </summary>
using UnityEngine;
using UnityEngine.UI;

public class FoldRect : MonoBehaviour
{
    public bool fold = true;
    private bool foldLast = true;
    public float foldDuration = 0.25f;
    private float foldingPassedTime = 0f;

    public Toggle toggle;

    public Vector2 foldupSize = new Vector2(100, 100);
    public Vector2 unFoldupSize = new Vector2(100, 200);

    private RectTransform rect;

    public enum States
    {
        Idle,
        Folding,
    }
    private States statusNow = States.Idle;
    public States Status { get { return statusNow; } }
    private States statusLast = States.Idle;

    void Start()
    {
        rect = this.gameObject.GetComponent<RectTransform>();
    }

    void Update()
    {
        if (toggle != null)
            fold = toggle.isOn;

        UpdateStatus();
    }

    void UpdateStatus()
    {
        bool isStatusChanged = false;
        if (statusNow != statusLast)
        {
            isStatusChanged = true;
            statusLast = statusNow;
        }

        switch (statusNow)
        {
            case States.Idle:
                {
                    if (fold != foldLast)
                    {
                        statusNow = States.Folding;
                        foldLast = fold;
                    }
                }
                break;
            case States.Folding:
                {
                    if (isStatusChanged)
                    {
                        foldingPassedTime = 0f;
                        break;
                    }

                    if (foldDuration <= 0f)
                    {
                        FoldDirectly();
                        statusNow = States.Idle;
                        break;
                    }

                    if (foldingPassedTime <= foldDuration)
                    {
                        Vector2 v = rect.sizeDelta;
                        if (fold)
                        {
                            v = Vector2.Lerp(unFoldupSize, foldupSize, (foldingPassedTime / foldDuration));
                        }
                        else
                        {
                            v = Vector2.Lerp(foldupSize, unFoldupSize, (foldingPassedTime / foldDuration));
                        }
                        SetRect(v);
                        foldingPassedTime += Time.deltaTime;
                    }
                    else
                    {
                        FoldDirectly();
                        statusNow = States.Idle;
                        break;
                    }

                    fold = foldLast;  // can't change foldup at Folding state
                }
                break;
        }
    }

    void FoldDirectly()
    {
        if (fold)
            FoldupDirectly();
        else
            UnfoldupDirectly();
    }

    void FoldupDirectly()
    {
        SetRect(foldupSize);
    }

    void UnfoldupDirectly()
    {
        SetRect(unFoldupSize);
    }

    void SetRect(Vector2 v)
    {
        rect.sizeDelta = v;
    }
}
