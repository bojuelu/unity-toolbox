#if !UNITY_EDITOR
#define HIDE_LOG
#endif

// Category: VERBOSE, DEBUG, WARNING, ERROR, EXCEPTION

#if HIDE_LOG
public static class Debug
{
    public static void Log(object o)
    {
#if LOG_VERBOSE || LOG_DEBUG
        UnityEngine.Debug.Log(o);
#endif
    }

    public static void Log(object o, UnityEngine.Object uo)
    {
#if LOG_VERBOSE || LOG_DEBUG
        UnityEngine.Debug.Log(o, uo);
#endif
    }

    public static void LogWarning(object o)
    {
#if LOG_VERBOSE || LOG_WARNING
        UnityEngine.Debug.LogWarning(o);
#endif
    }

    public static void LogWarning(object o, UnityEngine.Object uo)
    {
#if LOG_VERBOSE || LOG_WARNING
        UnityEngine.Debug.LogWarning(o, uo);
#endif
    }

    public static void LogError(object o)
    {
#if LOG_VERBOSE || LOG_ERROR
        UnityEngine.Debug.LogError(o);
#endif
    }

    public static void LogError(object o, UnityEngine.Object uo)
    {
#if LOG_VERBOSE || LOG_ERROR
        UnityEngine.Debug.LogError(o, uo);
#endif
    }

    public static void LogException(System.Exception e)
    {
#if LOG_VERBOSE || LOG_EXCEPTION
        UnityEngine.Debug.LogException(e);
#endif
    }

    public static void LogException(System.Exception e, UnityEngine.Object uo)
    {
#if LOG_VERBOSE || LOG_EXCEPTION
        UnityEngine.Debug.LogException(e, uo);
#endif
    }
}
#endif
