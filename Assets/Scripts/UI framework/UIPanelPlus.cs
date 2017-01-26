using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIPanelPlus : UIPanel
{
    public bool defaultIsShow = true;
    public TweenBase[] bringInTweens = null;
    public TweenBase[] dismissTweens = null;

    public UnityEvent onBringInEvent;
    public UnityEvent onDismissEvent;

    protected override void Start()
    {
        isShow = defaultIsShow;

        base.Start();
    }

    public override void BringIn()
    {
        if (isShow == true || IsTweening())
            return;

        for (int i = 0; i < bringInTweens.Length; i++)
        {
            bringInTweens[i].Run();
        }

        if (onBringInEvent != null)
            onBringInEvent.Invoke();
        
        base.BringIn();
    }

    public override void Dismiss()
    {
        if (isShow == false || IsTweening())
            return;

        for (int i = 0; i < dismissTweens.Length; i++)
        {
            dismissTweens[i].Run();
        }

        if (onDismissEvent != null)
            onDismissEvent.Invoke();
        
        base.Dismiss();
    }

    protected override void OnDestroy()
    {
        if (onBringInEvent != null)
            onBringInEvent.RemoveAllListeners();
        if (onDismissEvent != null)
            onDismissEvent.RemoveAllListeners();

        base.OnDestroy();
    }

    bool IsTweening()
    {
        if (bringInTweens != null)
        {
            for (int i = 0; i < bringInTweens.Length; i++)
            {
                if (bringInTweens[i].IsTweening)
                    return true;
            }
        }
        if (dismissTweens != null)
        {
            for (int i = 0; i < dismissTweens.Length; i++)
            {
                if (dismissTweens[i].IsTweening)
                    return true;
            }
        }

        return false;
    }
}
