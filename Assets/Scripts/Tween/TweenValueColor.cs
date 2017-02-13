/// <summary>
/// Tween value color. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;

public class TweenValueColor : TweenBase
{
    public Color colorFrom = Color.black;
    public Color colorTo = Color.white;
    private Color colorNow = Color.black;
    public Color ColorNow
    {
        get { return colorNow; }
        set { colorNow = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        tweenType = "value";
    }

    public void Run(Color colorFrom, Color colorTo)
    {
        this.colorNow = colorFrom;
        this.colorFrom = colorFrom;
        this.colorTo = colorTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(tweenTarget,
            iTween.Hash(
                "name", tweenType,
                "from", colorFrom,
                "to", colorTo,
                "time", duration,
                "delay", delay,
                "easeType", Ease.ToString(),
                "loopType", Loop,
                "onupdate", Callback.OnUpdateColorFuncName,
                "onupdatetarget", Callback.gameObject,
                "oncomplete", Callback.OnCompleteFuncName,
                "oncompletetarget", Callback.gameObject,
                "ignoretimescale", ignoreTimeScale
            )
        );

        Callback.onUpdateColorEvent = this.OnUpdate;
    }

    private void OnUpdate(Color c)
    {
        colorNow = c;
    }
}
