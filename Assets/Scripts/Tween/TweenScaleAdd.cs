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

        Space space = Space.Self;
        if (!base.isLocal)
            space = Space.World;

        iTween.ScaleAdd(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "space", space,
                "amount", this.scaleAmount,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),
                "loopType", base.Loop,
                "ignoretimescale", base.ignoreTimeScale,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject
            )
        );
    }
}
