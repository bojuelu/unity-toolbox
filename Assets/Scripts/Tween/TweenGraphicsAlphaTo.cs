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

    private bool skipFirstFrame = true;

    public override void Run()
    {
        graphics = this.tweenTarget.GetComponentsInChildren<Graphic>();

        base.Run();
    }

    void Update()
    {
        if (skipFirstFrame)  // skip the first frame, bcz at the first frame, this.FloatNow is not correct value
        {
            skipFirstFrame = false;
            return;
        }
        else if (isTweening)
        {
            ApplyAlpha();
        }
    }

    protected override void OnComplete()
    {
        ApplyAlpha();
        skipFirstFrame = true;

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
