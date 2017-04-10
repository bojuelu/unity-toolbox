/// <summary>
/// Tween value vector3. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenValueVector3 : TweenBase
{
    public Vector3 vectorFrom = Vector3.zero;
    public Vector3 vectorTo = Vector3.up * 10;
    private Vector3 vectorNow = Vector3.zero;
    public Vector3 VectorNow
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

    public void Run(Vector3 vectorFrom, Vector3 vectorTo)
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
                "onupdate", Callback.OnUpdateVector3FuncName,
                "onupdatetarget", Callback.gameObject,
                "oncomplete", Callback.OnCompleteFuncName,
                "oncompletetarget", Callback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        Callback.onUpdateVector3Event = this.OnUpdate;
    }

    private void OnUpdate(Vector3 vec3)
    {
        vectorNow = vec3;
        onUpdateInvokeTimes++;
    }
}
