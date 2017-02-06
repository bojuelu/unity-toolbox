/// <summary>
/// Tween graphic alpha to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicAlphaTo : TweenValueFloat
{
    private Graphic graphic;

    public override void Run()
    {
        graphic = this.tweenTarget.GetComponent<Graphic>();

        float nowAlpha = graphic.color.a;

        this.FloatNow = nowAlpha;  // avoid tweening first frame use wrong value

        base.Run();
    }

    void Update()
    {
        if (isTweening == true)
            ApplyColor();
    }

    protected override void OnComplete()
    {
        ApplyColor();
        base.OnComplete();
    }

    void ApplyColor()
    {
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, this.FloatNow);
    }
}
