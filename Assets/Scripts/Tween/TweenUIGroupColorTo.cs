/// <summary>
/// Tween a lot of TweenUIColorTo. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenUIGroupColorTo : TweenValueColor
{
    private Image[] images = null;

    protected override void Start()
    {
        images = this.tweenTarget.GetComponentsInChildren<Image>();
        base.Start();
    }

    protected override void OnComplete()
    {
        if (CanApplyColor())
            ApplyColor();
        
        base.OnComplete();
    }

    bool CanApplyColor()
    {
        if (images == null)
            return false;
        else if (images.Length <= 0)
            return false;

        return true;
    }

    void ApplyColor()
    {
        for (int i=0; i<images.Length; i++)
        {
            if (images[i] == null)
                continue;
            else
                images[i].color = this.ColorNow;
        }
    }

    void Update()
    {
        if (CanApplyColor() == false)
            return;
        
        if (this.GetComponent<iTween>() != null)
        {
            ApplyColor();
        }
    }
}
