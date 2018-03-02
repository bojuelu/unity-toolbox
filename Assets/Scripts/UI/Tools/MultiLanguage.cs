﻿using UnityEngine;

namespace UnityToolbox
{
    public class MultiLanguage : MonoBehaviour
    {
        public UnityEngine.UI.Text[] texts;

        public string[] english;
        public string[] chineseTraditional;
        public string[] chineseSimplified;
        public string[] japanese;
        public string[] korean;
        public string[] thai;

        void Start()
        {
            // select string content via SystemLanguage
            SystemLanguage lang = Application.systemLanguage;
            string[] content = null;
            switch (lang)
            {
                case SystemLanguage.English:
                    content = english;
                    break;
                case SystemLanguage.Chinese:
                case SystemLanguage.ChineseTraditional:
                    content = chineseTraditional;
                    break;
                case SystemLanguage.ChineseSimplified:
                    content = chineseSimplified;
                    break;
                case SystemLanguage.Japanese:
                    content = japanese;
                    break;
                case SystemLanguage.Korean:
                    content = korean;
                    break;
                case SystemLanguage.Thai:
                    content = thai;
                    break;

                default:
                    content = english;
                    break;
            }

            // apply string content to texts
            for (int i = 0; i < content.Length; i++)
            {
                if (content[i] == null)
                    continue;
                if (texts[i] == null)
                    continue;
                if (i >= texts.Length)
                    break;
                string theContent = content[i].Replace("\\n", System.Environment.NewLine);
                texts[i].text = theContent;
            }
        }

    }
}
