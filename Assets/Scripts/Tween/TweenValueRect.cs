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
        base.tweenType = "value";
    }

    public void Run(Rect rectFrom, Rect rectTo)
    {
        this.rectFrom = rectFrom;
        this.rectTo = rectTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "from", this.rectFrom,
                "to", this.rectTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),

                "loopType", base.Loop,

                "onupdate", base.Callback.OnUpdateRectFuncName,
                "onupdatetarget", base.Callback.gameObject,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject,

                "ignoretimescale", base.ignoreTimeScale
            )
        );

        base.Callback.onUpdateRectEvent = this.OnUpdate;
    }

    private void OnUpdate(Rect rect)
    {
        this.rectNow = rect;
    }
}
