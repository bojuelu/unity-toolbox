/// <summary>
/// Tween image alpha to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenImageAlphaTo : TweenValueFloat
{
    public override void Run()
    {
        float nowAlpha = this.tweenTarget.GetComponent<Image>().color.a;

        this.FloatNow = nowAlpha;  // avoid tweening first frame use wrong value

        base.Run();
    }

    void Update()
    {
        if (isTweening == true)
            ApplyAlpha();
    }

    protected override void OnComplete()
    {
        ApplyAlpha();
        base.OnComplete();
    }

    void ApplyAlpha()
    {
        Image image = this.tweenTarget.GetComponent<Image>();
        image.color = new Color(image.color.r, image.color.g, image.color.b, this.FloatNow);
    }
}
