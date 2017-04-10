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

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "moveto";
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

        iTween.MoveTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "islocal", base.isLocal,
                "position", this.moveTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.ease.ToString(),
                "loopType", base.loop,
                "ignoretimescale", base.ignoreTimeScale,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject
            )
        );
    }
}
