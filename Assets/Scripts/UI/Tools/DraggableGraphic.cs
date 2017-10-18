/// <summary>
/// Let UGUI element can be draggable
/// Author: BoJue.
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Graphic))]
public class DraggableGraphic : MonoBehaviour, IPointerDownHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public delegate void EventHandler(DraggableGraphic obj, PointerEventData eventData);
    public event EventHandler onPointDownEvent;
    public event EventHandler onBeginDragEvent;
    public event EventHandler onDragEvent;
    public event EventHandler onEndDragEvent;

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

        if (onPointDownEvent != null)
            onPointDownEvent(this, eventData);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (onBeginDragEvent != null)
            onBeginDragEvent(this, eventData);
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

        if (onDragEvent != null)
            onDragEvent(this, eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (onEndDragEvent != null)
            onEndDragEvent(this, eventData);
    }

    void OnDestroy()
    {
        onPointDownEvent = null;
        onBeginDragEvent = null;
        onDragEvent = null;
        onEndDragEvent = null;
    }
}
