using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{
    [CreateAssetMenu]
    public class EdibleItemSO : ItemSO, IDestroyableItem, IItemAction
    {
        [SerializeField] private List<ModifierData> modifierData = new List<ModifierData>();
        public string ActionName => "Use";
        // [field : SerializeField] public AudioClip actionSFX { get; set; }

        public bool PerformAction(GameObject character, List<ItemParamater> itemState = null)
        {
            foreach (ModifierData data in modifierData)
            {
                if (data.statModifierSO == null)
                {
                    Debug.LogWarning("statModifierSO is null in ModifierData. Skipping this modifier.");
                    continue;
                }
                data.statModifierSO.AffectCharacter(character, data.value);
                data.statModifierSO.AffectManaCharacter(character, data.valueMana);
                data.statModifierSO.AffectDamageCharacter(character, data.valueDamage, data.time);
                data.statModifierSO.AffectExpCharacter(character, data.valueExp);

                if (!data.isEquip)
                {
                    bool equipped = data.statModifierSO.EquipItem(character, data.isEquip, data.valMaxHP, data.valMaxMP, data.valueDamage, data.itemImage, data.itemType);
                    if (equipped)
                        data.isEquip = true;
                }
                else
                {
                    bool unequipped = data.statModifierSO.EquipItem(character, data.isEquip, data.valMaxHP, data.valMaxMP, data.valueDamage, data.itemImage, data.itemType);
                    if (unequipped)
                        data.isEquip = false;
                }
            }
            return true;
        }
    }

    internal interface IItemAction
    {
        public string ActionName { get; }
        bool PerformAction(GameObject character, List<ItemParamater> itemState);
    }

    internal interface IDestroyableItem
    {
    }

    [Serializable]
    public class ModifierData
    {
        public CharacterModifierSO statModifierSO;
        public float value;
        public float valueMana;
        public float valueDamage;
        public int time;
        public int valueExp;
        public bool isEquip;
        public float valMaxHP;
        public float valMaxMP;
        public Sprite itemImage;
        public int itemType;
    }
}

