/// <summary>
/// Tween graphic alpha to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicAlphaTo : TweenValueFloat
{
    public bool useNowAsFrom = false;

    private Graphic graphic;

    public override void Run()
    {
        graphic = tweenTarget.GetComponent<Graphic>();

        float nowAlpha = graphic.color.a;
        if (useNowAsFrom)
            floatFrom = nowAlpha;
        
        base.Run();

        FloatNow = nowAlpha; // avoid use the wrong value at first frame
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
        graphic.color = new Color(graphic.color.r, graphic.color.g, graphic.color.b, this.FloatNow);
    }
}
