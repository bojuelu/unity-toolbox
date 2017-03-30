/// <summary>
/// Tween graphic color to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicColorTo : TweenValueColor
{
    public bool useNowAsFrom = false;

    private Graphic graphic;

    public override void Run()
    {
        graphic = tweenTarget.GetComponent<Graphic>();

        Color nowColor = graphic.color;
        if (useNowAsFrom)
            colorFrom = nowColor;

        base.Run();

        ColorNow = nowColor;  // avoid use the wrong value at first frame
    }

    void Update()
    {
        if (isTweening && onUpdateInvokeTimes > 0)
            ApplyColor();
    }

    protected override void OnComplete()
    {
        base.OnComplete();

        ApplyColor();
    }

    void ApplyColor()
    {
        graphic.color = this.ColorNow;
    }
}
