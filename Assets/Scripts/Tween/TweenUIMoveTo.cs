/// <summary>
/// Tween user interface move to, only for RectTransform. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenUIMoveTo : TweenValueVector3
{
    public bool useNowAsFrom = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Run()
    {
        if (useNowAsFrom)
            this.vectorFrom = this.tweenTarget.GetComponent<RectTransform>().anchoredPosition;

        base.Run();
    }

    protected override void OnComplete()
    {
        ApplyPosition();

        base.OnComplete();
    }

    void ApplyPosition()
    {
        this.tweenTarget.GetComponent<RectTransform>().anchoredPosition = this.VectorNow;
    }

    void Update()
    {
        if (isTweening == true)
        {
            ApplyPosition();
        }
    }
}
