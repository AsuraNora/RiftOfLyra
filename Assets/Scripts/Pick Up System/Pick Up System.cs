using UnityEngine;
using Inventory.Model;
using System;

public class PickUpSystem : MonoBehaviour
{
    [SerializeField] private InventorySO inventoryData;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        ItemPickUp itemPickUp = collision.GetComponent<ItemPickUp>();

        if( itemPickUp != null )
        {
            int reminder = inventoryData.addItem( itemPickUp.InventoryItem, itemPickUp.Quantity );

            if( reminder == 0 )
            {
                itemPickUp.DestroyItem();
            }
            else
            {
                Debug.Log("Inventory is full, cannot pick up the item.");
                itemPickUp.Quantity = reminder;
            }
        }
    }

}
