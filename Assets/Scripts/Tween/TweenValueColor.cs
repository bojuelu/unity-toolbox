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
        base.tweenType = "value";
    }

    public void Run(Color colorFrom, Color colorTo)
    {
        this.colorFrom = colorFrom;
        this.colorTo = colorTo;

        this.Run();
    }

    public override void Run()
    {
        base.Run();

        iTween.ValueTo(base.tweenTarget,
            iTween.Hash(
                "name", base.tweenType,
                "from", this.colorFrom,
                "to", this.colorTo,
                "time", base.duration,
                "delay", base.delay,
                "easeType", base.Ease.ToString(),

                "loopType", base.Loop,

                "onupdate", base.Callback.OnUpdateColorFuncName,
                "onupdatetarget", base.Callback.gameObject,
                "oncomplete", base.Callback.OnCompleteFuncName,
                "oncompletetarget", base.Callback.gameObject,

                "ignoretimescale", base.ignoreTimeScale
            )
        );

        base.Callback.OnUpdateColorEvent = this.OnUpdate;
    }

    private void OnUpdate(Color c)
    {
        this.colorNow = c;
    }
}
