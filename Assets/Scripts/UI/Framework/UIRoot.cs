using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

namespace UnityToolbox
{
    /// <summary>
    /// Unity UGUI based, use this class to manage UGUI elements.
    /// Author: BoJue.
    /// </summary>
    [RequireComponent(typeof(Canvas))]
    public class UIRoot : MonoBehaviour
    {
        private static UIRoot instance = null;
        public static UIRoot Instance { get { return instance; } }

        private Canvas canvasRef = null;
        public Canvas CanvasRef { get { return canvasRef; } }

        private CanvasScaler canvasScalerRef = null;
        public CanvasScaler CanvasScalerRef { get { return canvasScalerRef; } }

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

        /// <summary>
        /// Find the specified name and objTag. Call it after Monobehaviour Start()
        /// </summary>
        /// <returns>The find.</returns>
        /// <param name="name">Name.</param>
        /// <param name="objTag">Object tag.</param>
        public UIObject Find(string name, string objTag = "")
        {
            for (int i = 0; i < registeredObjs.Count; i++)
            {
                UIObject obj = registeredObjs[i];
                if (obj == null)
                    continue;
                if (string.IsNullOrEmpty(objTag))
                {
                    if (obj.name == name)
                        return obj;
                }
                else
                {
                    if (obj.name == name && obj.objectTag == objTag)
                        return obj;
                }
            }
            Debug.LogWarning("can not find uiobject. name: " + name + " tag: " + objTag);
            return null;
        }

        /// <summary>
        /// Find instance. Call it after Monobehaviour Start()
        /// </summary>
        /// <returns>The find.</returns>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Find<T>() where T : UIObject
        {
            for (int i = 0; i < registeredObjs.Count; i++)
            {
                UIObject obj = registeredObjs[i];
                if (obj is T)
                    return obj as T;
            }
            Debug.LogWarning("can not find uiobject by type: " + typeof(T).ToString());
            return null;
        }

        /// <summary>
        /// Find the specified name and objTag. Call it after Monobehaviour Start()
        /// </summary>
        /// <returns>The find.</returns>
        /// <param name="name">Name.</param>
        /// <param name="objTag">Object tag.</param>
        /// <typeparam name="T">The 1st type parameter.</typeparam>
        public T Find<T>(string name, string objTag = "") where T : UIObject
        {
            for (int i = 0; i < registeredObjs.Count; i++)
            {
                UIObject obj = registeredObjs[i];
                if (obj is T)
                {
                    if (string.IsNullOrEmpty(objTag))
                    {
                        if (obj.name == name)
                            return obj as T;
                    }
                    else
                    {
                        if (obj.name == name && obj.objectTag == objTag)
                            return obj as T;
                    }
                }
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
            canvasRef = this.GetComponent<Canvas>();
            canvasScalerRef = this.GetComponent<CanvasScaler>();
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
}
