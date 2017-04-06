/// <summary>
/// Run a lot of Tweens at once. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatchTweens : MonoBehaviour
{
    public bool autoStart = true;
    private TweenBase[] tweens = null;

    public void Run()
    {
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i].enabled)
            {
                tweens[i].Run();
            }
        }
    }

    public void Pause()
    {
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i].enabled)
            {
                tweens[i].Pause();
            }
        }
    }

    public void Resume()
    {
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i].enabled)
            {
                tweens[i].Resume();
            }
        }
    }

    public void Stop()
    {
        for (int i = 0; i < tweens.Length; i++)
        {
            if (tweens[i].enabled)
            {
                tweens[i].Stop();
            }
        }
    }

    public bool IsRunning()
    {
        bool isRunning = false;
        for (int i = 0; i < tweens.Length; i++)
        {
            isRunning |= (tweens[i].IsTweening && tweens[i].enabled);
        }
        return isRunning;
    }
        
    public void ReloadTweens()
    {
        tweens = this.gameObject.GetComponents<TweenBase>();
        for (int i = 0; i < tweens.Length; i++)
        {
            tweens[i].autoStart = false;
        }
    }

    void Awake()
    {
        ReloadTweens();
    }

    void Start()
    {
        if (autoStart)
        {
            this.Run();
        }
    }
}
