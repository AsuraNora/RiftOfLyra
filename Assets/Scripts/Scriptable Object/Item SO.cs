using System;
using System.Collections.Generic;
using UnityEngine;

namespace Inventory.Model
{

    public abstract class ItemSO : ScriptableObject
    {
        [field: SerializeField] public bool isStackable { get; set; }
        public int ID => GetInstanceID();
        [field: SerializeField] public int maxStack { get; set; }
        [field: SerializeField] public bool canEquip { get; set; }
        [field: SerializeField] public string itemName { get; set; }
        [field: SerializeField] [field: TextArea] public string itemDescription { get; set; }
        [field: SerializeField] public Sprite itemImage { get; set;}
        [field: SerializeField] public List<ItemParamater> defaultParamaterList { get; set; }
    }
  
    [Serializable]
    public struct ItemParamater : IEquatable<ItemParamater>
    {
        public ItemParamaterSO itemParamaterSO;
        public float value;
        public bool Equals(ItemParamater other)
        {
            return other.itemParamaterSO == itemParamaterSO;
        }
    }
}

