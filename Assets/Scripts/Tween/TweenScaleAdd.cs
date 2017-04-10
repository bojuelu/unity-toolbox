/// <summary>
/// Tween scale add. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenScaleAdd : TweenBase
{
    public Vector3 scaleAmount = Vector3.one * 2f;

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "scaleadd";
    }

    public override void Run()
    {
        base.Run();

        Space space = Space.World;
        if (isLocal)
            space = Space.Self;

        iTween.ScaleAdd(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "space", space,
                "amount", this.scaleAmount,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.ease.ToString(),
                "loopType", base.loop,
                "ignoretimescale", base.ignoreTimeScale,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject
            )
        );
    }
}
