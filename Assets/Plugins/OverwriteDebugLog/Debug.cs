/// <summary>
/// Hide Debug log when product release.
/// Log tags: LOG_VERBOSE, LOG_DEBUG, LOG_WARNING, LOG_ERROR, LOG_EXCEPTION .
/// LOG_VERBOSE will show all kind of logs.
/// Author: BoJue.
/// </summary>

#if UNITY_EDITOR
// In Unity editor, this script is no use, print log as usual.
#else
    #if UNITY_IOS
        // At iOS, use GetLogTagClass.mm to setup log tag
        #define HIDE_LOG_FROM_XCODE
    #else
        // At the other OS, goto: Build Settings -> Player Settings -> Other Settings -> Scripting Define Symbols. to setup log tag
        #define HIDE_LOG
    #endif
#endif

#if HIDE_LOG_FROM_XCODE
using System;
using System.Runtime.InteropServices;

public static class Debug
{
    [DllImport("__Internal")]
    private static extern string GetLogTag();

    public static void Log(object o)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_DEBUG")
            UnityEngine.Debug.Log(o);
        
        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void Log(object o, UnityEngine.Object uo)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_DEBUG")
            UnityEngine.Debug.Log(o, uo);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogFormat(string format, params object[] args)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_DEBUG")
            UnityEngine.Debug.LogFormat(format, args);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogWarning(object o)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_WARNING")
            UnityEngine.Debug.LogWarning(o);
        
        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogWarning(object o, UnityEngine.Object uo)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_WARNING")
            UnityEngine.Debug.LogWarning(o, uo);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogWarningFormat(string format, params object[] args)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_WARNING")
            UnityEngine.Debug.LogWarningFormat(format, args);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogError(object o)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_ERROR")
            UnityEngine.Debug.LogError(o);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogError(object o, UnityEngine.Object uo)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_ERROR")
            UnityEngine.Debug.LogError(o, uo);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogErrorFormat(string format, params object[] args)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_ERROR")
            UnityEngine.Debug.LogErrorFormat(format, args);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogException(System.Exception e)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_EXCEPTION")
            UnityEngine.Debug.LogException(e);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void LogException(System.Exception e, UnityEngine.Object uo)
    {
        string tag = GetLogTag();
        if (tag == "LOG_VERBOSE" || tag == "LOG_EXCEPTION")
            UnityEngine.Debug.LogException(e, uo);

        // free memory, bcz this string is come from malloc via objective-c
        IntPtr stringPointer = (IntPtr)Marshal.StringToHGlobalAnsi(tag);
        Marshal.FreeHGlobal(stringPointer);
    }

    public static void Assert(bool b)
    {
        //        UnityEngine.Debug.Assert(b);
    }

    public static void Assert(bool b, UnityEngine.Object o)
    {
        //        UnityEngine.Debug.Assert(b, o);
    }

    public static void Assert(bool b, object o)
    {
        //        UnityEngine.Debug.Assert(b, o);
    }

    public static void Assert(bool b, string s)
    {
        //        UnityEngine.Debug.Assert(b, s);
    }

    public static void Assert(bool b, object o, UnityEngine.Object c)
    {
        //        UnityEngine.Debug.Assert(b, o, c);
    }

    public static void Assert(bool b, string s, UnityEngine.Object c)
    {
        //        UnityEngine.Debug.Assert(b, s, c);
    }

    public static void Assert(bool b, string s, params object[] args)
    {
    }

    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 dir)
    {
        UnityEngine.Debug.DrawRay(start, dir);
    }

    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end)
    {
        UnityEngine.Debug.DrawLine(start, end);            
    }

    public static void DebugBreak()
    {
        UnityEngine.Debug.DebugBreak();
    }
}
#elif HIDE_LOG
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

    public static void LogFormat(string format, params object[] args)
    {
#if LOG_VERBOSE || LOG_DEBUG
        UnityEngine.Debug.LogFormat(format, args);
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

    public static void LogWarningFormat(string format, params object[] args)
    {
#if LOG_VERBOSE || LOG_WARNING
        UnityEngine.Debug.LogWarningFormat(format, args);
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

    public static void LogErrorFormat(string format, params object[] args)
    {
#if LOG_VERBOSE || LOG_ERROR
        UnityEngine.Debug.LogErrorFormat(format, args);
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

    public static void Assert(bool b)
    {
//        UnityEngine.Debug.Assert(b);
    }

    public static void Assert(bool b, UnityEngine.Object o)
    {
//        UnityEngine.Debug.Assert(b, o);
    }

    public static void Assert(bool b, object o)
    {
//        UnityEngine.Debug.Assert(b, o);
    }

    public static void Assert(bool b, string s)
    {
//        UnityEngine.Debug.Assert(b, s);
    }

    public static void Assert(bool b, object o, UnityEngine.Object c)
    {
//        UnityEngine.Debug.Assert(b, o, c);
    }

    public static void Assert(bool b, string s, UnityEngine.Object c)
    {
//        UnityEngine.Debug.Assert(b, s, c);
    }

    public static void Assert(bool b, string s, params object[] args)
    {
    }

    public static void DrawRay(UnityEngine.Vector3 start, UnityEngine.Vector3 dir)
    {
        UnityEngine.Debug.DrawRay(start, dir);
    }

    public static void DrawLine(UnityEngine.Vector3 start, UnityEngine.Vector3 end)
    {
        UnityEngine.Debug.DrawLine(start, end);            
    }

    public static void DebugBreak()
    {
        UnityEngine.Debug.DebugBreak();
    }
}
#endif
