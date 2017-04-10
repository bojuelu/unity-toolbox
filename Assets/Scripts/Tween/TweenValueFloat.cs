﻿/// <summary>
/// Tween value float. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenValueFloat : TweenBase
{
    public float floatFrom = 0f;
    public float floatTo = 1f;
    private float floatNow = 0f;
    public float FloatNow
    {
        get { return floatNow; }
        set { floatNow = value; }
    }

    protected int onUpdateInvokeTimes = 0;

    protected override void Awake()
    {
        base.Awake();
        tweenType = "value";
    }

    public void Run(float floatFrom, float floatTo)
    {
        this.floatFrom = floatFrom;
        this.floatTo = floatTo;

        Run();
    }

    public override void Run()
    {
        base.Run();

        onUpdateInvokeTimes = 0;

        floatNow = floatFrom;

        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenType,
                "from", floatFrom,
                "to", floatTo,
                "time", duration,
                "delay", delay,
                "easeType", ease.ToString(),
                "loopType", loop,
                "onupdate", Callback.OnUpdateFloatFuncName,
                "onupdatetarget", Callback.gameObject,
                "oncomplete", Callback.OnCompleteFuncName,
                "oncompletetarget", Callback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        Callback.onUpdateFloatEvent = this.OnUpdate;
    }

    private void OnUpdate(float f)
    {
        floatNow = f;
        onUpdateInvokeTimes++;
    }
}
