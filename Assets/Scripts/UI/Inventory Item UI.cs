using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.EventSystems;

namespace Inventory.UI
{
    public class InventoryItemUI : MonoBehaviour
    {
        [SerializeField] private Image imageItem;
        [SerializeField] private TMP_Text quantityText;

        public event Action<InventoryItemUI>
            OnItemClicked,
            OnItemDroppedOn,
            OnItemBeginDrag,
            OnItemEndDrag,
            OnRightMouseBtnClicked
            ;

        private bool empty = true;

        public void Awake()
        {
            ResetData();
            DeSelect();

        }

        public void ResetData()
        {
            if (imageItem != null)
            {
                this.imageItem.gameObject.SetActive(false);
            }
            else
            {
                Debug.LogWarning("imageItem is null in InventoryItemUI. Skipping ResetData.");
            }

            if (quantityText != null)
            {
                this.quantityText.text = string.Empty;
            }
            else
            {
                Debug.LogWarning("quantityText is null in InventoryItemUI. Skipping ResetData.");
            }

            empty = true;
        }

        public void DeSelect()
        {
            this.imageItem.color = Color.white;
        }

        public void SetColor(Color color)
        {
            
            this.imageItem.color = color;
            
        }

        public void SetData(Sprite sprite, int quantity)
        {
            this.imageItem.gameObject.SetActive(true);
            this.imageItem.sprite = sprite;
            this.quantityText.text = quantity + "";
            empty = false;
        }

        public void Select()
        {
            this.imageItem.color = Color.yellow;
        }

        public void OnBeginDrag()
        {
            if (empty)
            {
                return;
            }
            OnItemBeginDrag?.Invoke(this);
        }

        public void OnDrop()
        {
            OnItemDroppedOn?.Invoke(this);
        }

        public void OnEndDrag()
        {
            OnItemEndDrag?.Invoke(this);
        }

        public void OnPointerClick(BaseEventData eventData)
        {
            PointerEventData pointerEventData = (PointerEventData)eventData;
            if (pointerEventData.button == PointerEventData.InputButton.Right)
            {
                OnRightMouseBtnClicked?.Invoke(this);
            }
            else
            {
                OnItemClicked?.Invoke(this);
            }
        }

    }
}

