/// <summary>
/// Let UGUI element can be draggable
/// Author: BoJue.
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Graphic))]
public class DraggableGraphic : MonoBehaviour, IPointerDownHandler, IDragHandler
{
    private RectTransform thisRectTransform;

    private Vector3 dragShift = Vector3.zero;

    void Start()
    {
        gameObject.GetComponent<Graphic>().raycastTarget = true;
        thisRectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Vector3 pointerDownPosition = eventData.position;
        Vector3 nowRectTransformPosition = thisRectTransform.position;

        dragShift = nowRectTransformPosition - pointerDownPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 globalMousePos = Vector3.zero;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            thisRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out globalMousePos)
        )
        {
            Vector2 finalPosition = globalMousePos + dragShift;
            thisRectTransform.position = finalPosition;
        }
    }
}
