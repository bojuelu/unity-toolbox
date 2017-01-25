/// <summary>
/// Inherence from UIPanel, it use TweenBase as default BringIn and Dismiss behavior.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIPanelPlus : UIPanel
{
    public bool defaultIsShow = true;
    public TweenBase[] bringInTweens;
    public TweenBase[] dismissTweens;
    private bool isTweening = false;
    public bool IsTweening { get { return isTweening; } }
    private int totalTweenNum = 0;
    private int completeTweenNum = 0;

    public UnityEvent OnBringInEvent;
    public UnityEvent OnDismissEvent;

    void OnBringInTweenComplete()
    {
        completeTweenNum++;
    }

    void OnDismissTweenComplete()
    {
        completeTweenNum++;
    }

    protected override void Start()
    {
        base.isShow = defaultIsShow;

        for (int i = 0; i < bringInTweens.Length; i++)
        {
            bringInTweens[i].Callback.OnCompleteEvent += this.OnBringInTweenComplete;
        }
        for (int i = 0; i < dismissTweens.Length; i++)
        {
            dismissTweens[i].Callback.OnCompleteEvent += this.OnDismissTweenComplete;
        }

        base.Start();
    }

    public override void BringIn()
    {
        if (base.isShow || isTweening)
            return;

        totalTweenNum = bringInTweens.Length;
        completeTweenNum = 0;
        for (int i = 0; i < bringInTweens.Length; i++)
        {
            bringInTweens[i].Run();
        }
        isTweening = true;
        if (OnBringInEvent != null)
            OnBringInEvent.Invoke();
        
        base.BringIn();
    }

    public override void Dismiss()
    {
        if (!base.isShow || isTweening)
            return;

        totalTweenNum = dismissTweens.Length;
        completeTweenNum = 0;
        for (int i = 0; i < dismissTweens.Length; i++)
        {
            dismissTweens[i].Run();
        }
        isTweening = true;
        if (OnDismissEvent != null)
            OnDismissEvent.Invoke();
        
        base.Dismiss();
    }

    protected virtual void Update()
    {
        if (isTweening)
        {
            if (completeTweenNum >= totalTweenNum)
                isTweening = false;
        }
    }

    protected override void OnDestroy()
    {
        if (OnBringInEvent != null)
            OnBringInEvent.RemoveAllListeners();
        if (OnDismissEvent != null)
            OnDismissEvent.RemoveAllListeners();

        for (int i = 0; i < bringInTweens.Length; i++)
        {
            bringInTweens[i].Callback.OnCompleteEvent -= this.OnBringInTweenComplete;
        }
        for (int i = 0; i < dismissTweens.Length; i++)
        {
            dismissTweens[i].Callback.OnCompleteEvent -= this.OnDismissTweenComplete;
        }
        
        base.OnDestroy();
    }
}
