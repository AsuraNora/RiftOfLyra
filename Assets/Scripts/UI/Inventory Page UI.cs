using System;
using System.Collections.Generic;

using UnityEngine;

namespace Inventory.UI
{
    public class InventoryPageUI : MonoBehaviour
    {
        [SerializeField] private InventoryItemUI itemPrefab;
        [SerializeField] private RectTransform panelContent;
        [SerializeField] public RectTransform panelInventoryBackGround;
        [SerializeField] public InventoryDescriptionUI inventoryDescriptionUI;
        [SerializeField] private MouseFollowerUI mouseFollowerUI;

        private Canvas canvas;
        private InventoryItemUI inventoryItemUI;

        private void Awake()
        {
            //Hide();
            mouseFollowerUI.Toggle(false);
            inventoryDescriptionUI.ResetDescription();

            canvas = transform.root.GetComponent<Canvas>();

            // Kiểm tra và gán InventoryItemUI
            inventoryItemUI = GetComponent<InventoryItemUI>();

        }

        List<InventoryItemUI> listOfInventoryItemUI = new List<InventoryItemUI>();

        private int currentlyDraggedItemIndex = -1;

        public event Action<int> OnDescrionRequested,
                                 OnItemActionRequested,
                                 OnStartDragging
                                 ;

        public event Action<int, int> OnSwapItems;

        [SerializeField] private ItemActionUI itemActionUI;

        public void InitializeInventoryUI(int inventorySize)
        {
            for (int i = 0; i < inventorySize; i++)
            {
                InventoryItemUI itemUI = Instantiate(itemPrefab, panelContent);

                itemUI.transform.localPosition = Vector3.zero;
                itemUI.transform.localRotation = Quaternion.identity;

                listOfInventoryItemUI.Add(itemUI);
                itemUI.OnItemClicked += HandleItemSelection;
                itemUI.OnItemBeginDrag += HandleBeginDrag;
                itemUI.OnItemDroppedOn += HandleSwap;
                itemUI.OnItemEndDrag += HandleEndDrag;
                itemUI.OnRightMouseBtnClicked += HandleShowItemAction;
            }
        }

        private void HandleShowItemAction(InventoryItemUI uI)
        {
            int index = listOfInventoryItemUI.IndexOf(uI);
            if (index == -1)
            {
                return;
            }
            OnItemActionRequested?.Invoke(index);
        }

        private void HandleEndDrag(InventoryItemUI uI)
        {
            ResetDraggedItem();
        }

        private void HandleSwap(InventoryItemUI uI)
        {
            int index = listOfInventoryItemUI.IndexOf(uI);
            if (index == -1)
            {
                return;
            }
            OnSwapItems?.Invoke(currentlyDraggedItemIndex, index);
            HandleItemSelection(uI);
        }

        public void ResetDraggedItem()
        {
            mouseFollowerUI.Toggle(false);
            currentlyDraggedItemIndex = -1;
        }

        private void HandleBeginDrag(InventoryItemUI uI)
        {
            int index = listOfInventoryItemUI.IndexOf(uI);
            if (index == -1)
                return;
            currentlyDraggedItemIndex = index;
            HandleItemSelection(uI);
            OnStartDragging?.Invoke(index);

        }

        public void CreateDraggedItem(Sprite sprite, int quantity)
        {
            mouseFollowerUI.Toggle(true);
            mouseFollowerUI.SetData(sprite, quantity);
        }

        private void HandleItemSelection(InventoryItemUI uI)
        {
            int index = listOfInventoryItemUI.IndexOf(uI);
            if (index == -1)
                return;
            OnDescrionRequested?.Invoke(index);
        }

        public void UpdateData(int itemIndex,
                                Sprite itemImage,
                                int itemQuantity
                                )
        {
            if (listOfInventoryItemUI.Count > itemIndex)
            {
                listOfInventoryItemUI[itemIndex].SetData(itemImage, itemQuantity);
            }
        }


        public void Show()
        {
            panelInventoryBackGround.anchoredPosition = new Vector2(panelInventoryBackGround.anchoredPosition.x, panelInventoryBackGround.anchoredPosition.y - 2040);
            if (listOfInventoryItemUI == null || listOfInventoryItemUI.Count == 0)
            {
                Debug.LogError("Danh sách InventoryItemUI rỗng hoặc chưa được khởi tạo!");
                return;
            }

            ResetSelection();
        }

        public void ResetSelection()
        {
            inventoryDescriptionUI.ResetDescription();
            DeSelectAllItems();
        }

        public void SetItemColor(int itemIndex, Color color)
        {
            if (listOfInventoryItemUI.Count > itemIndex)
            {
                listOfInventoryItemUI[itemIndex].SetColor(color);
            }
        }

        public void AddAction(string actionName, Action performAction)
        {
            itemActionUI.AddButton(actionName, performAction);
        }

        public void ShowItemAction(int index)
        {
            itemActionUI.Toggle(true);
            itemActionUI.transform.position = listOfInventoryItemUI[index].transform.position;
        }

        public void DeSelectAllItems()
        {
            foreach (InventoryItemUI item in listOfInventoryItemUI)
            {
                item.DeSelect();
            }
            itemActionUI.Toggle(false);
        }

        public void Hide()
        {
            itemActionUI.Toggle(false);
            panelInventoryBackGround.anchoredPosition = new Vector2(panelInventoryBackGround.anchoredPosition.x, panelInventoryBackGround.anchoredPosition.y + 2040);
            ResetDraggedItem();
        }

        internal void UpdateDescrion(int itemIndex, Sprite itemImage, string itemName, string itemDescription)
        {
            inventoryDescriptionUI.SetDescription(itemImage, itemName, itemDescription);
            DeSelectAllItems();
            listOfInventoryItemUI[itemIndex].Select();
        }

        internal void ResetAllItems()
        {
            foreach (var item in listOfInventoryItemUI)
            {
                item.ResetData();
                item.DeSelect();
            }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                DeSelectAllItems();
            }
            float scroll = Input.GetAxis("Mouse ScrollWheel");
            if (scroll > 0f)
            {
                DeSelectAllItems();
            }
            else if (scroll < 0f)
            {
                DeSelectAllItems();
            }

        }

    }
}
