using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TweenUIColorTo : TweenValueColor
{
    public bool useNowAsFrom = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Run()
    {
        if (useNowAsFrom)
            this.colorFrom = this.tweenTarget.GetComponent<Image>().color;
        
        base.Run();
    }

    void Update()
    {
        if (this.GetComponent<iTween>() != null)
        {
            this.tweenTarget.GetComponent<Image>().color = this.ColorNow;
        }
    }
}
