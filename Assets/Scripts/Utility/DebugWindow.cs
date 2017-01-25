/// <summary>
/// A draggable window on the game screen, it is useful for debugging :)
/// Power by UnityEngine old school style GUI.
/// Author: BoJue.
/// </summary>
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class DebugWindow : MonoBehaviour
{
    public Rect windowRect = new Rect(0, 0, 640, 480);
    public int fontSize = 16;
    public bool canDrag = true;

    private int id = 0;
    public int ID { get { return id; } }

    private Vector2 scrollPosition = Vector2.zero;
    private Vector2 logContentSize = Vector2.zero;
    private List<GUIContent> logList = new List<GUIContent>();

    private string[] delimiterStrings = { "\\n", "\\r", "\\r\\n", System.Environment.NewLine };

    public void Log(string log, bool printConsole=false)
    {
        if (printConsole)
            Debug.Log(log);

        string[] logs = SplitBy(log, delimiterStrings);
        for (int i = 0; i < logs.Length; i++)
        {
            GUIContent c = new GUIContent(logs[i]);
            logList.Add(c);
        }
        ScrollToBottom();
    }

    public void LogWarning(string log, bool printConsole=false)
    {
        if (printConsole)
            Debug.LogWarning(log);
        
        string[] logs = SplitBy(log, delimiterStrings);
        for (int i = 0; i < logs.Length; i++)
        {
            GUIContent c = new GUIContent(logs[i]);
            logList.Add(c);
        }
        ScrollToBottom();
    }

    public void LogError(string log, bool printConsole=false)
    {
        if (printConsole)
            Debug.LogError(log);
        
        string[] logs = SplitBy(log, delimiterStrings);
        for (int i = 0; i < logs.Length; i++)
        {
            GUIContent c = new GUIContent(logs[i]);
            logList.Add(c);
        }
        ScrollToBottom();
    }

    public void LogWithTag(string tag, string log, bool printConsole=false)
    {
        if (printConsole)
            Debug.Log(tag + log);
        
        string[] logs = SplitBy(log, delimiterStrings);
        for (int i = 0; i < logs.Length; i++)
        {
            GUIContent c = new GUIContent(logs[i]);
            logList.Add(c);
        }
        ScrollToBottom();
    }

    public void ClearLog()
    {
        logList.Clear();
    }

    public string[] GetLog()
    {
        string[] log = new string[logList.Count];
        for (int i = 0; i < log.Length; i++)
        {
            log[i] = logList[i].text;
        }
        return log;
    }

    private string[] SplitBy(string origString, string[] delimiterStrs)
    {
        bool needSplit = false;
        for (int i = 0; i < delimiterStrs.Length; i++)
        {
            if (origString.Contains(delimiterStrs[i]))
            {
                needSplit = true;
                break;
            }
        }

        if (needSplit)
        {
            string[] result = origString.Split(delimiterStrs, System.StringSplitOptions.None);
            return result;
        }
        else
        {
            string[] result = { origString };
            return result;
        }
    }

    private void ScrollToBottom()
    {
        if (this == null)
            return;
        
        this.StopCoroutine("LateScrollToBottom");
        this.StartCoroutine(this.LateScrollToBottom());
    }

    private IEnumerator LateScrollToBottom()
    {
        yield return new WaitForEndOfFrame();
        yield return new WaitForEndOfFrame();
        if (logContentSize.y >= windowRect.height)
        {
            scrollPosition.y = float.MaxValue;
        }
    }

    private void DoWindow(int windowID)
    {
        // define log label style
        GUIStyle labelStyle = GUI.skin.label;
        labelStyle.alignment = TextAnchor.UpperLeft;
        labelStyle.fontSize = fontSize;

        // update log content size
        logContentSize = Vector2.zero;
        for (int i = 0; i < logList.Count; i++)
        {
            Vector2 size = labelStyle.CalcSize(logList[i]);

            if (logContentSize.x < size.x)
                logContentSize.x = size.x;

            logContentSize.y += fontSize;  //size.y;
        }

        // draw log content in the scroll view
        Rect scrollPos = new Rect(0, 0, windowRect.width, windowRect.height);
        Rect scrollView = new Rect(0, 0, logContentSize.x, logContentSize.y + 10f);
        scrollPosition = GUI.BeginScrollView(scrollPos, scrollPosition, scrollView);
        {
            for (int i = 0; i < logList.Count; i++)
            {
                GUI.Label(
                    new Rect(
                        0,
                        0 + (i * fontSize),
                        labelStyle.CalcSize(logList[i]).x,
                        labelStyle.CalcSize(logList[i]).y
                    ),
                    logList[i],
                    labelStyle
                );
            }
        }
        GUI.EndScrollView();

        // make the window can drag
        if (canDrag)
            GUI.DragWindow();
    }

    private void Awake()
    {
        // It must use unique id for GUI.Window(), otherwise, some of GUI.Window() will disappear.
        // Use GUIUtility.GetControlID() to get un-collision id.
        id = GUIUtility.GetControlID(FocusType.Passive);
        Debug.Log("DebugWindow id: " + id.ToString());
    }

    private void Start()
    {
    }

    private void OnGUI()
    {
        GUIStyle wndStyle = GUI.skin.box;
        wndStyle.alignment = TextAnchor.UpperLeft;
        windowRect = GUI.Window(id, windowRect, DoWindow, "", wndStyle);
    }
}
