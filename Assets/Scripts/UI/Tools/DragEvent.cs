﻿/// <summary>
/// Drag event.
/// Author: BoJue.
/// </summary>
using UnityEngine;
using UnityEngine.EventSystems;

public class DragEvent : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public delegate void DragHandler(PointerEventData data);
    public event DragHandler onBeginDrag;
    public event DragHandler onDrag;
    public event DragHandler onEndDrag;

    public void OnBeginDrag(PointerEventData data)
    {
        if (onBeginDrag != null)
            onBeginDrag(data);
    }

    public void OnDrag(PointerEventData data)
    {
        if (onDrag != null)
            onDrag(data);
    }

    public void OnEndDrag(PointerEventData data)
    {
        if (onEndDrag != null)
            onEndDrag(data);
    }

    void OnDestroy()
    {
        onBeginDrag = null;
        onDrag = null;
        onEndDrag = null;
    }
}
