using UnityEngine;

namespace UnityToolbox
{
    /// <summary>
    /// Simular to UGUI GridLayoutGroup, but it allows every cells size are different.
    /// It alaways adjust sizeDelta itself, recommand use it with ScrollView together (add on ScrollView.Content)
    /// Author: BoJue.
    /// </summary>
    [ExecuteInEditMode]
    public class LineLayoutGroup : MonoBehaviour
    {
        public enum Direction
        {
            Horizontal,
            Vertical
        }
        public Direction direction = Direction.Horizontal;

        public float spacing = 0f;

        private RectTransform thisRectTransform;
        private Vector2 allCellsSize = Vector2.zero;

        private void UpdateOwnRectTransform()
        {
            // set own anchor to left-top
            SetAnchorToLeftTop(thisRectTransform);

            thisRectTransform.sizeDelta = allCellsSize;
        }

        private void UpdateCellsRectTransform()
        {
            // set all child anchor to left-top
            for (int i = 0; i < thisRectTransform.childCount; i++)
            {
                Transform t = thisRectTransform.GetChild(i);
                RectTransform rt = t.GetComponent<RectTransform>();
                SetAnchorToLeftTop(rt);
            }

            switch (direction)
            {
                case Direction.Horizontal:
                    {
                        LineCellsByHorizontalAndCalcAllCellSize();
                    }
                    break;
                case Direction.Vertical:
                    {
                        LineCellsByVerticalAndCalcAllCellSize();
                    }
                    break;
            }
        }

        private void LineCellsByHorizontalAndCalcAllCellSize()
        {
            allCellsSize = Vector2.zero;
            Vector2 pos = Vector2.zero;
            for (int i = 0; i < thisRectTransform.childCount; i++)
            {
                Transform t = thisRectTransform.GetChild(i);
                RectTransform rt = t.GetComponent<RectTransform>();

                rt.anchoredPosition = pos;

                pos.x += rt.sizeDelta.x;
                pos.x += spacing;

                if (rt.sizeDelta.y > allCellsSize.y)
                    allCellsSize.y = rt.sizeDelta.y;
                allCellsSize.x = pos.x;
            }
        }

        private void LineCellsByVerticalAndCalcAllCellSize()
        {
            allCellsSize = Vector2.zero;
            Vector2 pos = Vector2.zero;
            for (int i = 0; i < thisRectTransform.childCount; i++)
            {
                Transform t = thisRectTransform.GetChild(i);
                RectTransform rt = t.GetComponent<RectTransform>();

                rt.anchoredPosition = pos;

                pos.y += -rt.sizeDelta.y;
                pos.y += -spacing;

                if (rt.sizeDelta.x > allCellsSize.x)
                    allCellsSize.x = rt.sizeDelta.x;
                allCellsSize.y = -pos.y;
            }
        }

        private void SetAnchorToLeftTop(RectTransform rt)
        {
            rt.anchorMin = new Vector2(0, 1);
            rt.anchorMax = new Vector2(0, 1);
            rt.pivot = new Vector2(0, 1);
        }

        private void Start()
        {
            thisRectTransform = this.gameObject.GetComponent<RectTransform>();
            SetAnchorToLeftTop(thisRectTransform);
            thisRectTransform.anchoredPosition = Vector2.zero;
        }

        private void Update()
        {
            UpdateCellsRectTransform();
            UpdateOwnRectTransform();
        }
    }
}
