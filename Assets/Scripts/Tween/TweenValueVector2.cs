/// <summary>
/// Tween value vector2. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenValueVector2 : TweenBase
{
    public Vector2 vectorFrom = Vector3.zero;
    public Vector2 vectorTo = Vector3.up * 10;
    private Vector2 vectorNow = Vector3.zero;
    public Vector2 VectorNow
    {
        get { return vectorNow; }
        set { vectorNow = value; }
    }

    protected int onUpdateInvokeTimes = 0;

    protected override void Awake()
    {
        base.Awake();
        tweenType = "value";
    }

    public void Run(Vector2 vectorFrom, Vector2 vectorTo)
    {
        this.vectorFrom = vectorFrom;
        this.vectorTo = vectorTo;

        Run();
    }

    public override void Run()
    {
        base.Run();

        onUpdateInvokeTimes = 0;

        vectorNow = vectorFrom;

        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenType,
                "from", vectorFrom,
                "to", vectorTo,
                "time", duration,
                "delay", delay,
                "easeType", ease.ToString(),
                "loopType", loop,
                "onupdate", Callback.OnUpdateVector2FuncName,
                "onupdatetarget", Callback.gameObject,
                "oncomplete", Callback.OnCompleteFuncName,
                "oncompletetarget", Callback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        Callback.onUpdateVector2Event = this.OnUpdate;
    }

    private void OnUpdate(Vector2 vec2)
    {
        vectorNow = vec2;
        onUpdateInvokeTimes++;
    }
}
