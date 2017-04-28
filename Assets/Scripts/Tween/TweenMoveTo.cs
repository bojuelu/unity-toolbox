/// <summary>
/// Tween move to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenMoveTo : TweenBase
{
    public bool useNowAsFrom = false;
    public Vector3 moveFrom = Vector3.zero;
    public Vector3 moveTo = Vector3.up * 10;

    private iTween iTweenInstance = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public void Run(Vector3 moveFrom, Vector3 moveTo, bool isLocal=true)
    {
        this.moveFrom = moveFrom;
        this.moveTo = moveTo;
        this.isLocal = isLocal;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        if (useNowAsFrom)
            moveFrom = this.transform.localPosition;
        else
            this.transform.localPosition = moveFrom;

        tweenName = "moveto-" + UnityUtility.GenerateRandomString(8);
        iTween.MoveTo(tweenTarget,
            iTween.Hash(
                "name", tweenName,
                "islocal", isLocal,
                "position", moveTo,
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
