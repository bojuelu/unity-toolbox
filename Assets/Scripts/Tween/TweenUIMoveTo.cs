using UnityEngine;
using System.Collections;

public class TweenUIMoveTo : TweenValueVector3
{
    public bool useNowAsFrom = false;

    protected override void Start()
    {
        base.Start();
    }

    public override void Run()
    {
        if (useNowAsFrom)
            this.vectorFrom = this.tweenTarget.GetComponent<RectTransform>().anchoredPosition;

        base.Run();
    }

    protected override void OnComplete()
    {
        ApplyPosition();

        base.OnComplete();
    }

    void ApplyPosition()
    {
        this.tweenTarget.GetComponent<RectTransform>().anchoredPosition = this.VectorNow;
    }

    void Update()
    {
        if (this.GetComponent<iTween>() != null)
        {
            ApplyPosition();
        }
    }
}
