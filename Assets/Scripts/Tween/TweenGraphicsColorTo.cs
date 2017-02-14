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

    public override void Run()
    {
        graphics = this.tweenTarget.GetComponentsInChildren<Graphic>();
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
