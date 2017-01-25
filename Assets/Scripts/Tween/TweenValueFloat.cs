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

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "value";
    }

    public void Run(float floatFrom, float floatTo)
    {
        this.floatFrom = floatFrom;
        this.floatTo = floatTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "from", this.floatFrom,
                "to", this.floatTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),

                "loopType", base.Loop,

                "onupdate", base.Callback.OnUpdateFloatFuncName,
                "onupdatetarget", base.Callback.gameObject,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject,

                "ignoretimescale", base.ignoreTimeScale
            )
        );

        base.Callback.OnUpdateFloatEvent = this.OnUpdate;
    }

    private void OnUpdate(float f)
    {
        this.floatNow = f;
    }
}
