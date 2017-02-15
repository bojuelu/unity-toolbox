/// <summary>
/// A panel use TweenBase behavior as it's BringIn and Dismiss.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class UIPanelPlus : UIPanel
{
    public bool defaultIsShow = true;
    public TweenBase[] bringInTweens = null;
    public TweenBase[] dismissTweens = null;

    public bool DestroyItselfWhenDismiss = false;

    public UnityEvent onStartEvent;
    public UnityEvent onFirstUpdateEvent;
    public UnityEvent onDestroyEvent;
    public UnityEvent onEnableEvent;
    public UnityEvent onDisableEvent;

    private bool isFirstUpdate = true;

    public bool IsTweening()
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

    protected virtual void Awake()
    {
        isShow = defaultIsShow;
    }

    protected virtual void OnEnable()
    {
        if (onEnableEvent != null)
            onEnableEvent.Invoke();
    }

    protected override void Start()
    {
        if (onStartEvent != null)
            onStartEvent.Invoke();

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

        if (DestroyItselfWhenDismiss)
            this.StartCoroutine(this.WaitTweensDoneThenDestroyItself());
        
        base.Dismiss();
    }

    private IEnumerator WaitTweensDoneThenDestroyItself()
    {
        yield return new WaitForEndOfFrame();

        while (IsTweening())
            yield return null;

        GameObject.Destroy(this.gameObject);
    }

    protected virtual void OnDisable()
    {
        if (onDisableEvent != null)
            onDisableEvent.Invoke();
    }

    protected override void OnDestroy()
    {
        if (onDestroyEvent != null)
        {
            onDestroyEvent.Invoke();
            onDestroyEvent.RemoveAllListeners();
        }

        if (onStartEvent != null)
            onStartEvent.RemoveAllListeners();

        if (onFirstUpdateEvent != null)
            onFirstUpdateEvent.RemoveAllListeners();
        
        if (onEnableEvent != null)
            onEnableEvent.RemoveAllListeners();
        
        if (onDisableEvent != null)
            onDisableEvent.RemoveAllListeners();

        base.OnDestroy();
    }

    protected virtual void Update()
    {
        if (isFirstUpdate)
        {
            if (onFirstUpdateEvent != null)
                onFirstUpdateEvent.Invoke();
            isFirstUpdate = false;
        }
    }
}
