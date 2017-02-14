/// <summary>
/// Tween rect transform move to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenRectTransformMoveTo : TweenValueVector3
{
    public override void Run()
    {
        Vector3 nowPos = this.tweenTarget.GetComponent<RectTransform>().anchoredPosition;

        base.Run();
    }

    void Update()
    {
        if (isTweening && onUpdateInvokeTimes > 0)
            ApplyPosition();
    }

    protected override void OnComplete()
    {
        base.OnComplete();

        ApplyPosition();
    }

    void ApplyPosition()
    {
        this.tweenTarget.GetComponent<RectTransform>().anchoredPosition = this.VectorNow;
    }
}
