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

        this.VectorNow = nowPos;  // avoid tweening first frame use wrong value

        base.Run();
    }

    void Update()
    {
        if (isTweening == true)
            ApplyPosition();
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
}
