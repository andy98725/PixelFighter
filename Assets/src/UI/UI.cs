using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
, IPointerClickHandler
, IDragHandler
{

    public ActionCard actionCardPrefab;

    private GameObject unitsPanel, actionsPanel, worldInput;

    public Unit selectedUnit;
    protected ActionCard selectedAction;

    const int SELECTION_NONE = -1, SELECTION_ACTION = 0, SELECTION_DIR = 1, SELECTION_CONFIRM = 2;
    private int selectionMode = SELECTION_ACTION;

    public void Awake()
    {
        unitsPanel = transform.Find("Units Panel").gameObject;
        actionsPanel = transform.Find("Actions Panel").gameObject;
        worldInput = transform.Find("World Input").gameObject;
    }
    public void Start()
    {
        selectUnit(selectedUnit);
    }

    private void openUI()
    {
        unitsPanel.SetActive(true);
        actionsPanel.SetActive(true);
        SizeCamera();
    }
    private void closeUI()
    {
        unitsPanel.SetActive(true);
        actionsPanel.SetActive(false);
        SizeCamera();

    }
    private void SizeCamera()
    {
        float height = 0;
        if (unitsPanel.activeSelf) height += unitsPanel.GetComponent<RectTransform>().rect.height;
        if (actionsPanel.activeSelf) height += actionsPanel.GetComponent<RectTransform>().rect.height;

        Rect screen = GetComponent<RectTransform>().rect;
        Camera.main.rect = new Rect(0, height / screen.height, 1, 1 - height / screen.height);

        worldInput.GetComponent<RectTransform>().sizeDelta = new Vector2(0, screen.height - height);
        worldInput.GetComponent<RectTransform>().position = new Vector3(screen.width / 2, screen.height / 2 + height / 2);
    }


    public void selectUnit(Unit unit)
    {
        selectedUnit = unit;
        if (selectedUnit != null)
        {
            selectionMode = SELECTION_ACTION;
            openUI();

            // Create & place actions
            List<ActionCard> cards = new List<ActionCard>();

        }
        else
        {
            selectionMode = SELECTION_NONE;
            closeUI();
        }
    }

    public void selectActionCard(ActionCard c)
    {
        selectedAction = c;
    }

    public void OnDrag(PointerEventData eventData)
    {
        // Debug.Log("UI drag");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        // Debug.Log("UI click");
    }
}
