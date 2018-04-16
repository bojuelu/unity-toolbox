using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityToolbox
{
    public class GrepImageColor : MonoBehaviour, IPointerClickHandler
    {
        public Canvas canvas;
        public Color grepColor;

        public delegate void OnGrepColorHandler(Color color);
        public event OnGrepColorHandler onGrepColorHandler;

        Image image;
        RectTransform imageRectTransform;

        void Start()
        {
            image = gameObject.GetComponent<Image>();
            imageRectTransform = image.GetComponent<RectTransform>();

            imageRectTransform.pivot = Vector2.zero;
            imageRectTransform.anchorMin = Vector2.zero;
            imageRectTransform.anchorMax = Vector2.zero;
        }

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            Vector2 clickPos = pointerEventData.position;
            Debug.Log("orig clickPos: " + clickPos.ToString());

            RectTransform canvasRT = canvas.GetComponent<RectTransform>();
            clickPos.x *= (canvasRT.sizeDelta.x / Screen.width);
            clickPos.y *= (canvasRT.sizeDelta.y / Screen.height);
            Debug.Log("result clickPos: " + clickPos.ToString());

            Vector2 imagePos = imageRectTransform.position;
            Debug.Log("orig imagePos: " + imagePos.ToString());

            imagePos.x *= (canvasRT.sizeDelta.x / Screen.width);
            imagePos.y *= (canvasRT.sizeDelta.y / Screen.height);
            Debug.Log("result imagePos: " + imagePos.ToString());

            Vector2 grepPixelPos = clickPos - imagePos;
            Debug.Log("orig grepPixelPos: " + grepPixelPos.ToString());

            grepPixelPos.x *= (image.mainTexture.width / ((imageRectTransform.sizeDelta.x > 0) ? imageRectTransform.sizeDelta.x : 1)) / ((imageRectTransform.localScale.x > 0) ? imageRectTransform.localScale.x : 1);
            grepPixelPos.y *= (image.mainTexture.height / ((imageRectTransform.sizeDelta.y > 0) ? imageRectTransform.sizeDelta.y : 1)) / ((imageRectTransform.localScale.y > 0) ? imageRectTransform.localScale.y : 1);
            Debug.Log("result grepPixelPos: " + grepPixelPos.ToString());


            Texture2D tex2d = image.mainTexture as Texture2D;
            grepColor = tex2d.GetPixel((int)grepPixelPos.x, (int)grepPixelPos.y);

            Debug.Log("grepColor: " + grepColor.ToString());

            if (onGrepColorHandler != null)
                onGrepColorHandler(grepColor);
        }
    }    
}
