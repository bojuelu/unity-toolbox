/// <summary>
/// Tween move add. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenMoveAdd : TweenBase
{
    public Vector3 moveAmount = new Vector3(10, 10, 10);

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

        tweenName = "moveadd-" + UnityUtility.GenerateRandomString(8);
        iTweenInstance = iTween.MoveAdd(tweenTarget,
            iTween.Hash(
                "name", tweenName,
                "space", space,
                "amount", moveAmount,
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
