using UnityEngine;
using System.Collections;

public class TweenScaleTo : TweenBase
{
    public bool useNowAsFrom = false;
    public Vector3 scaleFrom = Vector3.one;
    public Vector3 scaleTo = Vector3.one * 2;

    protected override void Awake()
    {
        base.Awake();
        base.tweenType = "scaleto";
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

        iTween.ScaleTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "islocal", base.isLocal,
                "scale", this.scaleTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),
                "loopType", base.Loop,
                "ignoretimescale", base.ignoreTimeScale,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject
            )
        );
    }

}
