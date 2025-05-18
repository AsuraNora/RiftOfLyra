using UnityEngine;
using Inventory.UI;

public class MouseFollowerUI : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    [SerializeField] private InventoryItemUI inventoryItemUI;

    public void Awake()
    {
        canvas = transform.root.GetComponent<Canvas>();
        inventoryItemUI = GetComponentInChildren<InventoryItemUI>();
    }

    public void SetData(Sprite sprite, int quantity)
    {
        inventoryItemUI.SetData(sprite, quantity);
    }

    void Update() {
        // Vector3 position;
        // RectTransformUtility.ScreenPointToWorldPointInRectangle(
        //     (RectTransform)canvas.transform,
        //     Input.mousePosition,
        //     canvas.worldCamera,
        //     out position
        // );
        // transform.position = canvas.transform.TransformPoint(position);
        transform.position = Input.mousePosition;
    }

    public void Toggle(bool val) {
        gameObject.SetActive(val);
    }
}
