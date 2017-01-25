/// <summary>
/// Run a lot of Tweens at once. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BatchTween : MonoBehaviour
{
    public bool autoStart = true;
    public GameObject objectWithTweens;
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

    void Awake()
    {
        tweens = objectWithTweens.GetComponents<TweenBase>();
        for (int i = 0; i < tweens.Length; i++)
        {
            tweens[i].autoStart = false;
        }
    }

    void Start()
    {
        if (autoStart)
        {
            this.Run();
        }
    }
}
