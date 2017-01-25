/// <summary>
/// Tween callback. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System;

public class TweenCallback : MonoBehaviour
{
    public delegate void iTweenCompleteDelegate();
    public iTweenCompleteDelegate OnCompleteEvent;
    public string OnCompleteFuncName { get { return "OnITweenComplete"; } }

    public delegate void iTweenUpdateFloatDelegate(float f);
    public iTweenUpdateFloatDelegate OnUpdateFloatEvent;
    public string OnUpdateFloatFuncName { get { return "OnITweenUpdateFloat"; } }

    public delegate void iTweenUpdateVector3(Vector3 vec3);
    public iTweenUpdateVector3 OnUpdateVector3Event;
    public string OnUpdateVector3FuncName { get { return "OnITweenUpdateVector3"; } }

    public delegate void iTweenUpdateRect(Rect rect);
    public iTweenUpdateRect OnUpdateRectEvent;
    public string OnUpdateRectFuncName { get { return "OnITweenUpdateRect"; } }

    public delegate void iTweenUpdateColor(Color rect);
    public iTweenUpdateColor OnUpdateColorEvent;
    public string OnUpdateColorFuncName { get { return "OnITweenUpdateColor"; } }

    private void OnITweenComplete()
    {
        if (OnCompleteEvent != null)
            OnCompleteEvent.Invoke();
    }

    private void OnITweenUpdateFloat(float f)
    {
        if (OnUpdateFloatEvent != null)
            OnUpdateFloatEvent.Invoke(f);
    }

    private void OnITweenUpdateVector3(Vector3 vec3)
    {
        if (OnUpdateVector3Event != null)
            OnUpdateVector3Event.Invoke(vec3);
    }

    private void OnITweenUpdateRect(Rect rect)
    {
        if (OnUpdateRectEvent != null)
            OnUpdateRectEvent.Invoke(rect);
    }

    private void OnITweenUpdateColor(Color c)
    {
        if (OnUpdateColorEvent != null)
            OnUpdateColorEvent.Invoke(c);
    }

    void OnDestroy()
    {
        OnCompleteEvent = null;
    }
}
