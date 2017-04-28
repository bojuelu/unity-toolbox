/// <summary>
/// Tween scale add. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenScaleAdd : TweenBase
{
    public Vector3 scaleAmount = Vector3.one * 2f;

    private iTween iTweenInstance = null;

    protected override void Awake()
    {
        base.Awake();
    }

    public override void Run()
    {
        base.Run();

        Space space = Space.World;
        if (isLocal)
            space = Space.Self;

        tweenName = "scaleadd-" + UnityUtility.GenerateRandomString(8);
        iTween.ScaleAdd(tweenTarget,
            iTween.Hash(
                "name", tweenName,
                "space", space,
                "amount", scaleAmount,
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
