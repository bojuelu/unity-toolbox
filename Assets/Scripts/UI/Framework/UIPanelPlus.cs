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
    public BatchTweens bringInTweens = null;
    public BatchTweens dismissTweens = null;

    public bool DestroyItselfWhenDismiss = false;

    public UnityEvent onStartEvent;
    public UnityEvent onFirstUpdateEvent;
    public UnityEvent onDestroyEvent;
    public UnityEvent onEnableEvent;
    public UnityEvent onDisableEvent;

    private bool isFirstUpdate = true;

    public float BringInNeedTime()
    {
        return CalcTweeningSpendTime(bringInTweens);
    }

    public float DismissNeedTime()
    {
        return CalcTweeningSpendTime(dismissTweens);
    }

    public bool IsTweening()
    {
        return bringInTweens.IsRunning() || dismissTweens.IsRunning();
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

        bringInTweens.Run();

        base.BringIn();
    }

    public override void Dismiss()
    {
        if (isShow == false || IsTweening())
            return;

        dismissTweens.Run();

        if (DestroyItselfWhenDismiss)
            this.StartCoroutine(this.WaitTweensDoneThenDestroyItself());
        
        base.Dismiss();
    }

    private float CalcTweeningSpendTime(BatchTweens batchTween)
    {
        float t = 0f;
        TweenBase[] allTweens = batchTween.gameObject.GetComponents<TweenBase>();
        for (int i = 0; i < allTweens.Length; i++)
        {
            if (allTweens[i] == null)
                continue;
            else if (allTweens[i].enabled == false)
                continue;
            else
            {
                float thisTweenNeedTime = allTweens[i].delay + allTweens[i].duration;
                if (thisTweenNeedTime > t)
                    t = thisTweenNeedTime;
            }
        }
        return t;
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
