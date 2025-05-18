using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Item
{
    public string itemName;
    public int itemID;
    public bool isStackable;  // Có thể cộng dồn?
    public bool isSplittable; // Có thể tách không?
    public int maxStack; // Số lượng tối đa trong 1 ô
}
