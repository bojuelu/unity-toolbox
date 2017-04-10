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

    protected int onUpdateInvokeTimes = 0;

    protected override void Awake()
    {
        base.Awake();
        tweenType = "value";
    }

    public void Run(Rect rectFrom, Rect rectTo)
    {
        this.rectFrom = rectFrom;
        this.rectTo = rectTo;

        Run();
    }

    public override void Run()
    {
        base.Run();

        onUpdateInvokeTimes = 0;

        rectNow = rectFrom;

        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenType,
                "from", rectFrom,
                "to", rectTo,
                "time", duration,
                "delay", delay,
                "easeType", ease.ToString(),
                "loopType", loop,
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
        onUpdateInvokeTimes++;
    }
}
