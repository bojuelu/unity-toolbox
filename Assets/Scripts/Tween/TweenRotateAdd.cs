﻿using UnityEngine;
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

        Space space = Space.Self;
        if (!base.isLocal)
            space = Space.World;

        iTween.RotateAdd(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "space", space,
                "amount", this.rotateAmount,
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
