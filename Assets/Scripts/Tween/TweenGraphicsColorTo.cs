/// <summary>
/// Tween lots of graphics color to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenGraphicsColorTo : TweenValueColor
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
        if (skipFirstFrame)  // skip the first frame, bcz at the first frame, this.ColorNow is not correct value
        {
            skipFirstFrame = false;
            return;
        }
        else if (isTweening)
        {
            ApplyColor();
        }
    }

    protected override void OnComplete()
    {
        ApplyColor();
        skipFirstFrame = true;

        base.OnComplete();
    }

    void ApplyColor()
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
                graphics[i].color = this.ColorNow;
            }            
        }
    }
}
