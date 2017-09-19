/// <summary>
/// Let UGUI element can be draggable
/// Author: BoJue.
/// </summary>
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Graphic))]
public class DraggableGraphic : MonoBehaviour, IDragHandler
{
    private RectTransform thisRectTransform;

    void Start()
    {
        gameObject.GetComponent<Graphic>().raycastTarget = true;
        thisRectTransform = gameObject.GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (thisRectTransform == null)
        {
            Debug.LogError("[DraggableGraphic] thisRectTransform == null");
            return;
        }
        Vector3 globalMousePos;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            thisRectTransform,
            eventData.position,
            eventData.pressEventCamera,
            out globalMousePos)
        )
        {
            thisRectTransform.position = globalMousePos;
        }
    }
}
