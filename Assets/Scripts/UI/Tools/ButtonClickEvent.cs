﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace UnityToolbox
{
    /// <summary>
    /// Button click event. UGUI Button.onClick has no args let callback function know what button is clicked. This is the solution.
    /// Author: BoJue.
    /// </summary>
    public class ButtonClickEvent : MonoBehaviour
    {
        public delegate void ClickHandler(ButtonClickEvent b);
        public event ClickHandler onClickEvent;
        public string strID = "";
        public int intID = 0;
        public float floatID = 0f;

        private Button button;
        public Button ButtonRef { get { return button; } }

        void Start()
        {
            button = this.gameObject.GetComponent<Button>();
            if (button == null)
            {
                Debug.LogError("no commponent 'Button' at this game object. destory itself");
                onClickEvent = null;
                GameObject.Destroy(this);
            }
            else
            {
                button.onClick.AddListener(this.OnClick);
            }
        }

        void OnDestroy()
        {
            if (button != null)
            {
                button.onClick.RemoveListener(this.OnClick);
            }
        }

        void OnClick()
        {
            if (onClickEvent != null)
            {
                onClickEvent.Invoke(this);
            }
        }
    }
}
