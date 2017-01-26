/// <summary>
/// Tween callback. Powered by iTween.
/// Author: BoJue.
/// </summary>

using UnityEngine;
using System;

public class TweenCallback : MonoBehaviour
{
    public delegate void iTweenCompleteDelegate();
    public iTweenCompleteDelegate onCompleteEvent;
    public string OnCompleteFuncName { get { return "OnITweenComplete"; } }

    public delegate void iTweenUpdateFloatDelegate(float f);
    public iTweenUpdateFloatDelegate onUpdateFloatEvent;
    public string OnUpdateFloatFuncName { get { return "OnITweenUpdateFloat"; } }

    public delegate void iTweenUpdateVector3(Vector3 vec3);
    public iTweenUpdateVector3 onUpdateVector3Event;
    public string OnUpdateVector3FuncName { get { return "OnITweenUpdateVector3"; } }

    public delegate void iTweenUpdateRect(Rect rect);
    public iTweenUpdateRect onUpdateRectEvent;
    public string OnUpdateRectFuncName { get { return "OnITweenUpdateRect"; } }

    public delegate void iTweenUpdateColor(Color rect);
    public iTweenUpdateColor onUpdateColorEvent;
    public string OnUpdateColorFuncName { get { return "OnITweenUpdateColor"; } }

    private void OnITweenComplete()
    {
        if (onCompleteEvent != null)
            onCompleteEvent.Invoke();
    }

    private void OnITweenUpdateFloat(float f)
    {
        if (onUpdateFloatEvent != null)
            onUpdateFloatEvent.Invoke(f);
    }

    private void OnITweenUpdateVector3(Vector3 vec3)
    {
        if (onUpdateVector3Event != null)
            onUpdateVector3Event.Invoke(vec3);
    }

    private void OnITweenUpdateRect(Rect rect)
    {
        if (onUpdateRectEvent != null)
            onUpdateRectEvent.Invoke(rect);
    }

    private void OnITweenUpdateColor(Color c)
    {
        if (onUpdateColorEvent != null)
            onUpdateColorEvent.Invoke(c);
    }

    void OnDestroy()
    {
        onCompleteEvent = null;
    }
}
