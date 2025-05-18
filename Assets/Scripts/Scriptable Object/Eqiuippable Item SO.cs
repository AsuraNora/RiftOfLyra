using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EqiuippableItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        public string ActionName => "Equip";

        public bool PerformAction( GameObject character, List<ItemParamater> itemState = null)
       
        {
            AgentWeaponScript weaponScript = character.GetComponent<AgentWeaponScript>();
            if (weaponScript != null)
            {
                weaponScript.SetWeapon(this, itemState == null ? defaultParamaterList : itemState);
                return true;
            }
            return false;
        }

    }

}

