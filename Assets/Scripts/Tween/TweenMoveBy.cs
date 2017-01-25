using UnityEngine;
using System.Collections;

public class TweenMoveBy : TweenBase
{
    public Vector3 moveAmount = new Vector3(10, 10, 10);

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "moveby";
    }

    public override void Run()
    {
        base.Run();

        Space space = Space.Self;
        if (!base.isLocal)
            space = Space.World;

        iTween.MoveBy(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "space", space,
                "amount", this.moveAmount,
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
