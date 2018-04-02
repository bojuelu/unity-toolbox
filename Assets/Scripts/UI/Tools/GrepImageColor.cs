using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace UnityToolbox
{
    public class GrepImageColor : MonoBehaviour, IPointerClickHandler
    {
        public Canvas canvas;
        public Color grepOutColor;

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
            Vector2 clickPos = pointerEventData.position;  // new Vector2(Input.mousePosition.x, Input.mousePosition.y); //pointerEventData.position;

            Debug.Log("orig clickPos: " + clickPos.ToString());

            RectTransform canvasRT = canvas.GetComponent<RectTransform>();
            clickPos.x *= (canvasRT.sizeDelta.x * canvasRT.localScale.x / Screen.width);
            clickPos.y *= (canvasRT.sizeDelta.y * canvasRT.localScale.y / Screen.height);
            Debug.Log("result clickPos: " + clickPos.ToString());

            Vector2 imagePos = imageRectTransform.anchoredPosition;
            Debug.Log("imagePos: " + imagePos.ToString());

            Vector2 grepPixelPos = clickPos - imagePos;

            grepPixelPos.x *= (imageRectTransform.sizeDelta.x / image.mainTexture.width) * imageRectTransform.localScale.x;
            grepPixelPos.y *= (imageRectTransform.sizeDelta.y / image.mainTexture.height) * imageRectTransform.localScale.y;

            Debug.Log("grepPixelPos: " + grepPixelPos.ToString());

            Texture2D tex2d = image.mainTexture as Texture2D;
            Color grepColor = tex2d.GetPixel((int)grepPixelPos.x, (int)grepPixelPos.y);

            Debug.Log("grepColor: " + grepColor.ToString());

            grepOutColor = grepColor;
        }
    }    
}
