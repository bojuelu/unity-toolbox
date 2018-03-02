using UnityEngine;
using UnityEngine.UI;

namespace UnityToolbox
{
    /// <summary>
    /// Sharp the fucking UGUI Text
    /// Refrence: http://answers.unity3d.com/questions/1226551/ui-text-is-blurred-unity-535f.html
    /// Use the way "Lame hack 1"
    /// Author: U_Ku_Shu (http://answers.unity3d.com/users/490552/u-ku-shu.html)
    /// </summary>
    [ExecuteInEditMode]
    public class TextSharperer : MonoBehaviour
    {
        private const int scaleValue = 10;

        private Text thisText;

        void Start()
        {
            thisText = gameObject.GetComponent<Text>();

            thisText.fontSize = thisText.fontSize * scaleValue;

            thisText.transform.localScale = thisText.transform.localScale / scaleValue;

            thisText.horizontalOverflow = HorizontalWrapMode.Overflow;
            thisText.verticalOverflow = VerticalWrapMode.Overflow;
        }
    }
}
