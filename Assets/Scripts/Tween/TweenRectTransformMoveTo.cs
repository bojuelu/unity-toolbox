﻿/// <summary>
/// Tween rect transform move to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenRectTransformMoveTo : TweenValueVector3
{
    public bool useNowAsFrom = false;

    public override void Run()
    {
        if (useNowAsFrom)
        {
            Vector3 nowPos = this.tweenTarget.GetComponent<RectTransform>().anchoredPosition3D;
            vectorFrom = nowPos;
        }

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
        tweenTarget.GetComponent<RectTransform>().anchoredPosition3D = this.VectorNow;
    }
}
