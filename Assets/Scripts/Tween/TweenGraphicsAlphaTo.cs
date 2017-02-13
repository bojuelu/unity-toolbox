/// <summary>
/// Tween lots of graphics alpha to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicsAlphaTo : TweenValueFloat
{
    private Graphic[] graphics;

    public override void Run()
    {
        graphics = this.tweenTarget.GetComponentsInChildren<Graphic>();
        base.Run();
    }

    void Update()
    {
        if (isTweening)
        {
            ApplyAlpha();
        }
    }

    protected override void OnComplete()
    {
        ApplyAlpha();

        base.OnComplete();
    }

    void ApplyAlpha()
    {
        if (graphics == null)
            return;

        for (int i = 0; i < graphics.Length; i++)
        {
            if (graphics[i] == null)
            {
                continue;
            }
            else
            {
                graphics[i].color = new Color(
                    graphics[i].color.r,
                    graphics[i].color.g,
                    graphics[i].color.b,
                    this.FloatNow
                );
            }            
        }
    }
}
