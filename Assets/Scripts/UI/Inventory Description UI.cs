
using UnityEngine;
using UnityEngine.UI;

namespace Inventory.UI 
{
    public class InventoryDescriptionUI : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private TMPro.TMP_Text titleName;
        [SerializeField] private TMPro.TMP_Text description;

        private void Awake()
        {
            ResetDescription();
        }

        public void ResetDescription() {
            this.image.gameObject.SetActive(false);
            this.titleName.text = "";
            this.description.text = "";

        }

        public void SetDescription(
                Sprite sprite, 
                string titleName,
                string description
                ) {
            this.image.gameObject.SetActive(true);
            this.image.sprite = sprite;
            this.titleName.text = titleName;
            this.description.text = description;
        }
        
    }
}

