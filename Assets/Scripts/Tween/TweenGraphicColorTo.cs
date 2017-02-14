/// <summary>
/// Tween graphic color to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicColorTo : TweenValueColor
{
    private Graphic graphic;

    public override void Run()
    {
        graphic = this.tweenTarget.GetComponent<Graphic>();

        Color nowColor = graphic.color;

        this.ColorNow = nowColor;  // avoid tweening first frame use wrong value

        base.Run();
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
