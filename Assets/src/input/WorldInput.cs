using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldInput : MonoBehaviour
, IPointerClickHandler
, IPointerDownHandler
, IBeginDragHandler
, IDragHandler
{

    private float lastClick = 0;
    private Vector3 prevWorldPosition;

    private const float CLICK_TIME = 0.25f;



    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos;
        DragWorldPoint(eventData, out pos);
        // idk
        Camera.main.transform.position -= (pos - prevWorldPosition) / 50;
        prevWorldPosition = pos;
    }


    public void Update()
    {
        lastClick += Time.deltaTime;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (lastClick >= CLICK_TIME) return;

        Debug.Log("Select Unit");
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        DragWorldPoint(eventData, out prevWorldPosition);
    }

    private bool DragWorldPoint(PointerEventData eventData, out Vector3 worldPoint)
    {
        return RectTransformUtility.ScreenPointToWorldPointInRectangle(
            GetComponent<RectTransform>(),
            eventData.position,
            eventData.pressEventCamera,
            out worldPoint);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        lastClick = 0;
    }
}