using UnityEngine;
using System.Collections;

public class UISetParentToUIRoot : MonoBehaviour
{
    [System.Serializable]
    public class RectTransformArgs
    {
        public Vector3 anchoredPosition3D = Vector3.zero;
        public Vector2 anchoredPosition = Vector2.zero;
        public Vector2 offsetMin = Vector2.zero;
        public Vector2 offsetMax = Vector2.zero;
        public Vector2 sizeDelta = new Vector2(100, 100);
        public Vector2 anchorMin = new Vector2(0.5f, 0.5f);
        public Vector2 anchorMax = new Vector2(0.5f, 0.5f);
        public Vector2 pivot = new Vector2(0.5f, 0.5f);
    }
    public RectTransformArgs parentedArgs;

    public enum SortUnderUIRoot
    {
        None,
        First,
        Last,
    }
    public SortUnderUIRoot Sorting = SortUnderUIRoot.Last;

    private RectTransform thisRectTransform = null;

    void Start()
    {
        thisRectTransform = this.GetComponent<RectTransform>();
        UIRoot uiRoot = UIRoot.Instance;
        if (uiRoot == null)
        {
            Debug.LogError("UIRoot is null");
            GameObject.Destroy(this);
            return;
        }
        else if (thisRectTransform == null)
        {
            Debug.LogError("this is not a ugui object");
            GameObject.Destroy(this);
            return;
        }
        else
        {
            Vector3 origPos = this.transform.localPosition;
            Quaternion origRotation = this.transform.localRotation;
            Vector3 origScale = this.transform.localScale;

            this.transform.SetParent(uiRoot.transform);
            switch (Sorting)
            {
                case SortUnderUIRoot.First:
                    this.transform.SetAsFirstSibling();
                    break;
                case SortUnderUIRoot.Last:
                    this.transform.SetAsLastSibling();
                    break;
                case SortUnderUIRoot.None:
                    break;
            }

            this.transform.localPosition = origPos;
            this.transform.localRotation = origRotation;
            this.transform.localScale = origScale;

            thisRectTransform.pivot = parentedArgs.pivot;

            thisRectTransform.anchorMin = parentedArgs.anchorMin;
            thisRectTransform.anchorMax = parentedArgs.anchorMax;            

            thisRectTransform.offsetMin = parentedArgs.offsetMin;
            thisRectTransform.offsetMax = parentedArgs.offsetMax;

            thisRectTransform.anchoredPosition = parentedArgs.anchoredPosition;
            thisRectTransform.anchoredPosition3D = parentedArgs.anchoredPosition3D;

            thisRectTransform.sizeDelta = parentedArgs.sizeDelta;

            GameObject.Destroy(this);
        }
    }
}
