/// <summary>
/// Tween value rect. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenValueRect : TweenBase
{
    public Rect rectFrom = new Rect(0, 0, 0, 0);
    public Rect rectTo = new Rect(1, 1, 1, 1);
    private Rect rectNow = new Rect(0, 0, 0, 0);
    public Rect RectNow
    {
        get { return rectNow; }
        set { rectNow = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        tweenType = "value";
    }

    public void Run(Rect rectFrom, Rect rectTo)
    {
        this.rectNow = rectFrom;
        this.rectFrom = rectFrom;
        this.rectTo = rectTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenType,
                "from", rectFrom,
                "to", rectTo,
                "time", duration,
                "delay", delay,
                "easeType", Ease.ToString(),
                "loopType", Loop,
                "onupdate", Callback.OnUpdateRectFuncName,
                "onupdatetarget", Callback.gameObject,
                "oncomplete", Callback.OnCompleteFuncName,
                "oncompletetarget", Callback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        Callback.onUpdateRectEvent = this.OnUpdate;
    }

    private void OnUpdate(Rect rect)
    {
        rectNow = rect;
    }
}
