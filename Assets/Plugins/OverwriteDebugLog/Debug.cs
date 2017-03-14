/// <summary>
/// Hide Debug log when product release.
/// Use Unity: Build Settings / Scripting Define Symbols (or define in your code), to decide what kind of log you want to show.
/// Define Symbols: LOG_VERBOSE, LOG_DEBUG, LOG_WARNING, LOG_ERROR, LOG_EXCEPTION .
/// LOG_VERBOSE will show all kind of logs.
/// Author: BoJue.
/// </summary>


#if !UNITY_EDITOR
#define HIDE_LOG
#endif

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
