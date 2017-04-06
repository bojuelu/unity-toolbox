/// <summary>
/// Tween move add. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenMoveAdd : TweenBase
{
    public Vector3 moveAmount = new Vector3(10, 10, 10);

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "moveadd";
    }

    public override void Run()
    {
        base.Run();

        Space space = Space.World;
        if (isLocal)
            space = Space.Self;

        iTween.MoveAdd(base.tweenTarget,
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
