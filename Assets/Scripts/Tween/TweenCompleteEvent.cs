using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class TweenCompleteEvent : MonoBehaviour
{
    public TweenBase tween;
    public UnityEvent onTweenCompleteEvent;

	void Start ()
	{
        if (tween == null)
        {
            tween = this.GetComponent<TweenBase>();
        }

        tween.Callback.onCompleteEvent += OnTweenComplete;
	}

    void OnDestroy()
    {
        tween.Callback.onCompleteEvent -= OnTweenComplete;
    }

    void OnTweenComplete()
    {
        if (onTweenCompleteEvent != null)
        {
            onTweenCompleteEvent.Invoke();
        }
    }
}
