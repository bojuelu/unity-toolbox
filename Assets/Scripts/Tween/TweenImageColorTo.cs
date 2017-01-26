/// <summary>
/// Tween image color to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenImageColorTo : TweenValueColor
{

    public override void Run()
    {
        Color nowColor = this.tweenTarget.GetComponent<Image>().color;

        this.ColorNow = nowColor;  // avoid tweening first frame use wrong value

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
        this.tweenTarget.GetComponent<Image>().color = this.ColorNow;
    }
}
