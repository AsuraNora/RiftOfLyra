using System;
using System.Collections.Generic;
using Inventory.Model;
using UnityEngine;

public class AgentWeaponScript : MonoBehaviour
{
    [SerializeField] private EqiuippableItemSO weapon;
    [SerializeField] private InventorySO inventoryData;
    [SerializeField] private List<ItemParamater> paramaterToModify;
    [SerializeField] private List<ItemParamater> itemCurrentState;

    public void SetWeapon(EqiuippableItemSO weapon, List<ItemParamater> itemState) 
    {
        if(weapon != null )
        {
            inventoryData.addItem(weapon, 1, itemCurrentState);
        }
        this.weapon = weapon;
        this.itemCurrentState = new List<ItemParamater>(itemState);
        ModifyParameters();
    }

    private void ModifyParameters()
    {
        foreach (var paramater in paramaterToModify)
        {
            if( itemCurrentState.Contains(paramater))
            {
                int index = itemCurrentState.IndexOf(paramater);
                float newValue = itemCurrentState[index].value + paramater.value;
                itemCurrentState[index] = new ItemParamater
                {
                    itemParamaterSO = paramater.itemParamaterSO,
                    value = newValue
                };
            }
        }
    }
}
