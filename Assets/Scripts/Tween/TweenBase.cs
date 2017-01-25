/// <summary>
/// Tween base. Powered by iTween.
/// </summary>
using UnityEngine;
using System.Collections;

public abstract class TweenBase : MonoBehaviour
{
    public string id = "";

    public GameObject tweenTarget = null;

    public bool autoStart = true;
    public float duration = 2f;
    public float delay = 0f;
    public iTween.EaseType Ease = iTween.EaseType.linear;
    public iTween.LoopType Loop = iTween.LoopType.none;
    public bool pingPongOnlyOnce = false;
    public bool ignoreTimeScale = false;
    public bool isLocal = true;
    protected string tweenType = "";
    public string TweenType { get { return tweenType; } }

    private bool isInited = false;
    protected int onCompleteTimes = 0;
    public int OnCompleteTimes { get { return onCompleteTimes; } }

    protected TweenCallback recvCallback = null;
    public TweenCallback Callback { get { return recvCallback; } }

    public virtual void Run()
    {
        // init iTween
        if (!isInited)
        {
            iTween.Init(this.tweenTarget);
            isInited = true;
        }

        // reset oncomplete counter
        onCompleteTimes = 0;

        // remove the iTween with the same type
        iTween[] tweens = this.tweenTarget.GetComponents<iTween>();
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i] == null)
                continue;
            
            if (tweens[i]._name == tweenType)
            {
                Debug.LogWarning(string.Format("duplicate tween type: {0} ,id: {1} destroy it.", tweenType, tweens[i].id));
                tweens[i].enabled = false;
                GameObject.Destroy(tweens[i]);
                tweens[i] = null;
            }
        }
    }

    public virtual void Pause()
    {
        iTween.Pause(this.tweenTarget, tweenType);
    }

    public virtual void Resume()
    {
        iTween.Resume(this.tweenTarget, tweenType);
    }

    public virtual void Stop()
    {
        iTween.Stop(this.tweenTarget, tweenType);
    }

    protected virtual void Awake()
    {
        if (tweenTarget == null)
        {
            tweenTarget = this.gameObject;
        }

        // build a receive iTween callback event object
        if (recvCallback == null)
        {
            GameObject recvObj = new GameObject();
            recvObj.transform.SetParent(this.transform);
            recvObj.name = "_tween_callback";
            recvCallback = recvObj.AddComponent<TweenCallback>();
            recvCallback.OnCompleteEvent = this.OnComplete;
        }
    }

    protected virtual void Start()
    {
        if (autoStart)
        {
            this.Run();
        }
    }

    protected virtual void OnComplete()
    {
        onCompleteTimes++;

        if (pingPongOnlyOnce && onCompleteTimes >= 2)
        {
            Debug.Log("PingPongOnlyOnce && _onCompleteTimes >= 2 established, Stop this tween");
            this.Stop();
        }
    }
}