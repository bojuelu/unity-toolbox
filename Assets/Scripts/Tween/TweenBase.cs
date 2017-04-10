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

    protected string tweenType = "";
    public string TweenType { get { return tweenType; } }

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

//        // remove the iTween with the same type
//        iTween[] tweens = this.tweenTarget.GetComponents<iTween>();
//        for (int i = 0; i < tweens.Length; i++)
//        {
//            if (tweens[i] == null)
//                continue;
//            
//            if (tweens[i]._name == tweenType)
//            {
//                Debug.LogWarning(string.Format("duplicate tween type: {0} ,id: {1} destroy it.", tweenType, tweens[i].id));
//                tweens[i].enabled = false;
//                GameObject.Destroy(tweens[i]);
//                tweens[i] = null;
//            }
//        }

        isTweening = true;
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
            recvObj.transform.localScale = Vector3.one;
            recvObj.transform.localRotation = Quaternion.identity;
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