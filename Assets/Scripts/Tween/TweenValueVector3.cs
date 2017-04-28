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

    private iTween iTweenInstance = null;

    protected override void Awake()
    {
        base.Awake();
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

        tweenName = "value-vector3-to-" + UnityUtility.GenerateRandomString(8);
        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenName,
                "from", vectorFrom,
                "to", vectorTo,
                "time", duration,
                "delay", delay,
                "easeType", ease.ToString(),
                "loopType", loop,
                "onupdate", recvCallback.OnUpdateVector3FuncName,
                "onupdatetarget", recvCallback.gameObject,
                "oncomplete", recvCallback.OnCompleteFuncName,
                "oncompletetarget", recvCallback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        recvCallback.onUpdateVector3Event -= this.OnUpdate;
        recvCallback.onUpdateVector3Event += this.OnUpdate;
    }

    public override void Pause()
    {
        if (iTweenInstance)
            iTweenInstance.enabled = false;
    }

    public override void Resume()
    {
        if (iTweenInstance)
            iTweenInstance.enabled = true;
    }

    private void OnUpdate(Vector3 vec3)
    {
        vectorNow = vec3;
        onUpdateInvokeTimes++;
    }
}
