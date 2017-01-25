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

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "value";
    }

    public void Run(Vector3 vecFrom, Vector3 vecTo)
    {
        this.vectorFrom = vecFrom;
        this.vectorTo = vecTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "from", this.vectorFrom,
                "to", this.vectorTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),

                "loopType", base.Loop,

                "onupdate", base.Callback.OnUpdateVector3FuncName,
                "onupdatetarget", base.Callback.gameObject,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject,

                "ignoretimescale", base.ignoreTimeScale
            )
        );

        base.Callback.OnUpdateVector3Event = this.OnUpdate;
    }

    private void OnUpdate(Vector3 vec3)
    {
        this.vectorNow = vec3;
    }
}
