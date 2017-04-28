/// <summary>
/// Tween scale to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenScaleTo : TweenBase
{
    public bool useNowAsFrom = false;
    public Vector3 scaleFrom = Vector3.one;
    public Vector3 scaleTo = Vector3.one * 2;

    private iTween iTweenInstance = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Run(Vector3 scaleFrom, Vector3 scaleTo)
    {
        this.scaleFrom = scaleFrom;
        this.scaleTo = scaleTo;

        this.Run();
    }

    public void Run(Vector3 scaleTo)
    {
        this.scaleTo = scaleTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        if (useNowAsFrom)
            scaleFrom = this.transform.localScale;
        else
            this.transform.localScale = scaleFrom;

        tweenName = "scaleto-" + UnityUtility.GenerateRandomString(8);
        iTween.ScaleTo(tweenTarget,
            iTween.Hash(
                "name", tweenName,
                "islocal", isLocal,
                "scale", scaleTo,
                "time", duration,
                "delay", delay,
                "easeType", ease.ToString(),
                "loopType", loop,
                "ignoretimescale", ignoreTimeScale,
                "oncomplete", recvCallback.OnCompleteFuncName,
                "oncompletetarget", recvCallback.gameObject
            )
        );
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
}
