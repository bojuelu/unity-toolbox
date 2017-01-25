using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent(typeof(Canvas))]
public class UIRoot : MonoBehaviour
{
    private static UIRoot instance = null;
    public static UIRoot Instance { get { return instance; } }

    private Canvas mainCanvas;
    public Canvas MainCanvas { get { return mainCanvas; } }
    private List<UIObject> registeredObjs = null;

    public bool Register(UIObject u)
    {
        if (u == null)
            return false;
        
        if (registeredObjs.Contains(u))
        {
            Debug.LogError(u.name + " has registered");
            return false;
        }
        else
        {
            for (int i = 0; i < registeredObjs.Count; i++)
            {
                if (registeredObjs[i].name == u.name)
                {
                    Debug.LogWarning("gameObject name duplicate: " + u.name + " , when using Find() method remember give input \"tag\" ");
                }
            }
        }

        registeredObjs.Add(u);
        return true;
    }

    public bool Unregister(UIObject u)
    {
        if (u == null)
            return false;

        if (registeredObjs.Contains(u))
        {
            registeredObjs.Remove(u);
            return true;
        }
        return false;
    }

    public UIObject Find(string name, string tag="")
    {
        for (int i = 0; i < registeredObjs.Count; i++)
        {
            UIObject obj = registeredObjs[i];
            //Debug.Log(string.Format("{0} {1} {1}", obj.name, name, tag));
            if (obj == null)
                continue;
            if (string.IsNullOrEmpty(tag))
            {
                if (obj.name == name)
                    return obj;
            }
            else
            {
                if (obj.name == name && obj.objectTag == tag)
                    return obj;
            }
        }
        Debug.LogWarning("can not find uiobject. name: " + name + " tag: " + tag);
        return null;
    }

    public T Find<T>() where T : UIObject
    {
        for (int i = 0; i < registeredObjs.Count; i++)
        {
            if (registeredObjs[i] is T)
                return registeredObjs[i] as T;
        }
        Debug.LogWarning("can not find uiobject by type: " + typeof(T).ToString());
        return null;
    }

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("UIRoot duplcate, destroy myself");
            GameObject.Destroy(this);
            return;
        }
        instance = this;
        mainCanvas = this.GetComponent<Canvas>();
        registeredObjs = new List<UIObject>();
    }

    void Update()
    {
        // remove null object
        for (int i = 0; i < registeredObjs.Count; i++)
        {
            if (registeredObjs[i] == null)
            {
                registeredObjs.Remove(registeredObjs[i]);
                break;
            }
        }
    }

    void OnDestroy()
    {
        registeredObjs.Clear();
        instance = null;
    }
}
