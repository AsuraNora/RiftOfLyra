using System.Runtime.InteropServices;
using UnityEngine;

public abstract class CharacterModifierSO : ScriptableObject
{
    public abstract void AffectCharacter(GameObject character, float val);
    public abstract void AffectManaCharacter(GameObject character, float valMana);
    public abstract void AffectDamageCharacter(GameObject character, float valDamage, int time);
    public abstract void AffectExpCharacter(GameObject character, int valExp);
    public abstract void EquipItem(GameObject character, bool isEquip, float val, float valMana, float valDamage, Sprite itemImage, int itemType);
}
