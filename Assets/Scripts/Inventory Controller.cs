using Inventory.Model;
using Inventory.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace Inventory
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryPageUI inventoryPageUI;
        [SerializeField] private InventorySO inventorySO;
        public List<InventoryItem> initialItems = new List<InventoryItem>();

        private GameObject player; // Tham chiếu đến nhân vật (player)

        void Start()
        {
            PrepareUI();
            PrepareInventoryData();
        }

        public void PrepareUI()
        {
            inventoryPageUI.InitializeInventoryUI(inventorySO.Size);
            inventoryPageUI.OnDescrionRequested += HandleDescrionRequest;
            inventoryPageUI.OnSwapItems += HandleSwapItems;
            inventoryPageUI.OnStartDragging += HandleStartDragging;
            inventoryPageUI.OnItemActionRequested += HandleItemActionRequest;
        }

        public void PrepareInventoryData()
        {
            inventorySO.Initialize();
            inventorySO.OnInventoryupdated += UpdateInventoryUI;
            foreach (var item in initialItems)
            {
                if (item.isEmpty)
                {
                    continue;
                }
                inventorySO.AddItem(item);
            }
        }

        private void UpdateInventoryUI(Dictionary<int, InventoryItem> inventoryState)
        {
            inventoryPageUI.ResetAllItems();
            foreach (var item in inventoryState)
            {
                inventoryPageUI.UpdateData(item.Key,
                                            item.Value.item.itemImage,
                                            item.Value.quantity
                                            );
            }
        }

        private void HandleItemActionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                return;
            }

            IItemAction itemAction = inventoryItem.item as IItemAction;
            if (itemAction != null)
            {
                inventoryPageUI.ShowItemAction(itemIndex);
                inventoryPageUI.AddAction(itemAction.ActionName, () => PerformAction(itemIndex));
            }

            IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
            if (destroyableItem != null)
            {
                inventoryPageUI.AddAction("Drop", () => DropItem(itemIndex, inventoryItem.quantity));
            }
        }

        public void DropItem(int itemIndex, int quantity)
        {
            inventorySO.RemoveItem(itemIndex, quantity);
            inventoryPageUI.ResetSelection();
        }

        public void PerformAction(int itemIndex)
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                return;
            }

            // Kiểm tra và lấy tham chiếu đến nhân vật (player) nếu chưa có
            if (player == null)
            {
                player = GameObject.FindGameObjectWithTag("Player");
                if (player == null)
                {
                    Debug.LogError("Player not found in the scene. Cannot perform action.");
                    return;
                }
                else
                {
                    Debug.Log($"Player found: {player.name}");
                }
            }

            if (inventoryItem.item.canEquip == false)
            {
                IDestroyableItem destroyableItem = inventoryItem.item as IDestroyableItem;
                if (destroyableItem != null)
                {
                    inventorySO.RemoveItem(itemIndex, 1);
                }

                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    itemAction.PerformAction(player, inventoryItem.itemState);

                    if (inventorySO.GetItemAt(itemIndex).isEmpty)
                    {
                        inventoryPageUI.ResetSelection();
                    }
                }
            }

            // Nếu item có thể trang bị, thực hiện hành động trang bị
            if (inventoryItem.item.canEquip == true)
            {
                IItemAction itemAction = inventoryItem.item as IItemAction;
                if (itemAction != null)
                {
                    itemAction.PerformAction(player, inventoryItem.itemState);

                    if (inventorySO.GetItemAt(itemIndex).isEmpty)
                    {
                        inventoryPageUI.ResetSelection();
                    }
                }
            }
        }

        private void HandleStartDragging(int itemIndex)
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                return;
            }
            inventoryPageUI.CreateDraggedItem(inventoryItem.item.itemImage, inventoryItem.quantity);
        }

        private void HandleSwapItems(int itemIndex_1, int itemIndex_2)
        {
            inventorySO.SwapItems(itemIndex_1, itemIndex_2);
        }

        private void HandleDescrionRequest(int itemIndex)
        {
            InventoryItem inventoryItem = inventorySO.GetItemAt(itemIndex);
            if (inventoryItem.isEmpty)
            {
                inventoryPageUI.ResetSelection();
                return;
            }
            ItemSO item = inventoryItem.item;
            string description = PrepareDescription(inventoryItem);
            inventoryPageUI.UpdateDescrion(itemIndex,
                                            item.itemImage,
                                            item.itemName,
                                            description
                                            );
        }

        private string PrepareDescription(InventoryItem inventoryItem)
        {
            StringBuilder description = new StringBuilder();
            description.Append(inventoryItem.item.itemDescription);
            description.AppendLine();
            for (int i = 0; i < inventoryItem.itemState.Count; i++)
            {
                description.Append($"{inventoryItem.itemState[i].itemParamaterSO.ParamaterName}" +
                               $" : {inventoryItem.itemState[i].value} / {inventoryItem.item.defaultParamaterList[i].value}");
            }
            return description.ToString();
        }

        public void OnButtonBagClick()
        {
            if (inventoryPageUI.panelInventoryBackGround.anchoredPosition.y == 0)
                {
                    inventoryPageUI.Hide();
                }
                else
                {
                    inventoryPageUI.Show();
                    foreach (var item in inventorySO.GetCurrentInventoryState())
                    {
                        inventoryPageUI.UpdateData(
                            item.Key,
                            item.Value.item.itemImage,
                            item.Value.quantity
                        );
                    }
                }
        }

        void Update()
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                if (inventoryPageUI.panelInventoryBackGround.anchoredPosition.y == 0)
                {
                    inventoryPageUI.Hide();
                }
                else
                {
                    inventoryPageUI.Show();
                    foreach (var item in inventorySO.GetCurrentInventoryState())
                    {
                        inventoryPageUI.UpdateData(
                            item.Key,
                            item.Value.item.itemImage,
                            item.Value.quantity
                        );
                    }
                }
            }

        
        }
    }
}

