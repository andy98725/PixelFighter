using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class WorldInput : MonoBehaviour
, IPointerClickHandler
, IPointerDownHandler
, IBeginDragHandler
, IDragHandler
{

    private float lastClick = 0;
    private Vector3 startDrag;

    private UI ui;

    private const float CLICK_TIME = 0.25f;


    public void Awake()
    {
        ui = transform.parent.GetComponent<UI>();
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        startDrag = WorldPoint(eventData);
    }
    public void OnDrag(PointerEventData eventData)
    {
        Vector3 pos = WorldPoint(eventData);
        Vector3 delta = pos - startDrag;
        Camera.main.transform.position -= delta;
    }


    public void Update()
    {
        lastClick += Time.deltaTime;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        lastClick = 0;
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (lastClick >= CLICK_TIME) return;

        Vector3 mousePos = WorldPoint(eventData);

        RaycastHit hit;
        mousePos.z = -1;
        Physics.Raycast(mousePos, Camera.main.transform.forward, out hit, 100.0f, LayerMask.GetMask("Units"));

        Unit selected = null;
        if (hit.collider != null) selected = hit.collider.gameObject.GetComponent<Unit>();

        ui.selectUnit(selected);

    }

    private Vector3 WorldPoint(PointerEventData eventData)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(eventData.position);
        point.z = 0;
        return point;
    }
}