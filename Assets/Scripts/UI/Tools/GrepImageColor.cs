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

            grepPixelPos.x *= (image.mainTexture.width / imageRectTransform.sizeDelta.x) * imageRectTransform.localScale.x;
            grepPixelPos.y *= (image.mainTexture.height / imageRectTransform.sizeDelta.y) * imageRectTransform.localScale.y;
            Debug.Log("result grepPixelPos: " + grepPixelPos.ToString());


            Texture2D tex2d = image.mainTexture as Texture2D;
            Color grepColor = tex2d.GetPixel((int)grepPixelPos.x, (int)grepPixelPos.y);

            Debug.Log("grepColor: " + grepColor.ToString());

            grepOutColor = grepColor;
        }
    }    
}
