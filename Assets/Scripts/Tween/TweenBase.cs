/// <summary>
/// Tween base. Powered by iTween.
/// Author: BoJue.
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
    public iTween.EaseType ease = iTween.EaseType.linear;
    public iTween.LoopType loop = iTween.LoopType.none;
    public bool pingPongOnlyOnce = false;
    public bool ignoreTimeScale = false;
    public bool isLocal = true;

    protected string tweenName = "";
    public string TweenName { get { return tweenName; } }

    protected bool isInited = false;
    public bool IsInited { get { return isInited; } }

    protected bool isTweening = false;
    public bool IsTweening { get { return isTweening; } }

    protected int completeTimes = 0;
    public int CompleteTimes { get { return completeTimes; } }

    protected TweenCallback recvCallback = null;
    public TweenCallback Callback { get { return recvCallback; } }

    public virtual void Run()
    {
        // reset oncomplete counter
        completeTimes = 0;

        isTweening = true;
    }

    public virtual void Pause()
    {
    }

    public virtual void Resume()
    {
    }

    public virtual void Stop()
    {
        iTween.StopByName(tweenTarget, tweenName);

        isTweening = false;
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
            recvObj.transform.localPosition = Vector3.zero;
            recvObj.transform.localRotation = Quaternion.identity;
            recvObj.transform.localScale = Vector3.one;
            recvObj.name = this.name + "_tween_callback";
            recvCallback = recvObj.AddComponent<TweenCallback>();
            recvCallback.onCompleteEvent += this.OnComplete;
        }
    }

    protected virtual void Start()
    {
        // init iTween
        if (!isInited)
        {
            iTween.Init(this.tweenTarget);
            isInited = true;
        }

        if (autoStart)
        {
            this.Run();
        }
    }

    protected virtual void OnDestroy()
    {
        if (recvCallback != null)
        {
            recvCallback.onCompleteEvent -= this.OnComplete;
        }
    }

    protected virtual void OnComplete()
    {
        completeTimes++;

        switch (loop)
        {
            case iTween.LoopType.loop:
                {
                }
                break;
            case iTween.LoopType.none:
                {
                    isTweening = false;
                }
                break;
            case iTween.LoopType.pingPong:
                {
                    if (pingPongOnlyOnce && completeTimes >= 2)
                    {
                        Debug.Log("PingPongOnlyOnce && _onCompleteTimes >= 2 established, Stop this tween");
                        this.Stop();
                    }
                }
                break;
        }
    }
}