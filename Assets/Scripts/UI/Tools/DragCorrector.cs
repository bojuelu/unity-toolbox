using UnityEngine;
using UnityEngine.EventSystems;

namespace UnityToolbox
{
    /// <summary>
    /// Fix issue: High resolution device is too sensitive on drag, so let click event being ignored.
    /// https://answers.unity.com/questions/1149417/ui-button-onclick-sensitivity-for-high-dpi-devices.html
    /// Author: FireOApache (https://answers.unity.com/users/323653/fireoapache.html)
    /// </summary>
    public class DragCorrector : MonoBehaviour
    {
        public int baseTH = 6;
        public int basePPI = 210;
        private int dragTH = 0;

        void Start()
        {
            dragTH = baseTH * (int)Screen.dpi / basePPI;

            EventSystem es = GetComponent<EventSystem>();

            if (es) es.pixelDragThreshold = dragTH;

            Debug.Log("[DragCorrector] dragTH: " + dragTH);
        }
    }
}
