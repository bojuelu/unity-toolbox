/// <summary>
/// Tween a lot of images color to. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenImagesColorTo : TweenValueColor
{
    private Image[] images = null;

    private bool isTweeningLast = false;

    protected override void Start()
    {
        images = this.tweenTarget.GetComponentsInChildren<Image>();
        base.Start();
    }

    public override void Run()
    {
        base.Run();
    }

    void Update()
    {
        if (isTweeningLast != isTweening)
        {
            isTweeningLast = isTweening;
            return;  // skip the first frame, bcz at the first frame, ColorNow is not correct value
        }
        else if (isTweeningLast == true)  
        {
            ApplyColor();
        }
    }

    protected override void OnComplete()
    {
        ApplyColor();
        base.OnComplete();
    }

    void ApplyColor()
    {
        if (images == null)
            return;

        for (int i=0; i<images.Length; i++)
        {
            if (images[i] == null)
                continue;
            else
                images[i].color = this.ColorNow;
        }
    }
}
