/// <summary>
/// Tween rotate add. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenRotateAdd : TweenBase
{
    public Vector3 rotateAmount = new Vector3(90, 90, 90);

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "rotateadd";
    }

    public override void Run()
    {
        base.Run();

        Space space = Space.World;
        if (isLocal)
            space = Space.Self;

        iTween.RotateAdd(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "space", space,
                "amount", this.rotateAmount,
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
