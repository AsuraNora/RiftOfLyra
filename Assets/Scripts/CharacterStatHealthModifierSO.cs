using System.Collections;
using Inventory.Model;
using Inventory.UI;
using UnityEngine;

[CreateAssetMenu]
public class CharacterStatHealthModifierSO : CharacterModifierSO
{
    private ItemSO itemSO;
    private EdibleItemSO edibleItemSO;
    public override void AffectCharacter(GameObject character, float val)
    {
        if (character == null)
        {
            Debug.LogError("Character is null. Cannot apply health modifier.");
            return;
        }

        // Lấy component Thongtin từ đối tượng character
        Thongtin thongtin = character.GetComponent<Thongtin>();
        if (thongtin != null)
        {
            thongtin.AddHealth(val);
        }
        else
        {
            Debug.LogError($"Thongtin component not found on {character.name}. Cannot apply health modifier.");
        }
    }

    public override void AffectManaCharacter(GameObject character, float valMana)
    {
        if (character == null)
        {
            Debug.LogError("Character is null. Cannot apply mana modifier.");
            return;
        }

        // Lấy component Thongtin từ đối tượng character
        Thongtin thongtin = character.GetComponent<Thongtin>();
        if (thongtin != null)
        {
            thongtin.AddMana(valMana);
        }
        else
        {
            Debug.LogError($"Thongtin component not found on {character.name}. Cannot apply mana modifier.");
        }
    }

    public override void AffectDamageCharacter(GameObject character, float valDamage, int time)
    {
        if (character == null)
        {
            Debug.LogError("Character is null. Cannot apply damage modifier.");
            return;
        }

        // Lấy component Thongtin từ đối tượng character
        Thongtin thongtin = character.GetComponent<Thongtin>();
        if (thongtin != null)
        {
            thongtin.AddDamage(valDamage);
            character.GetComponent<Thongtin>().StartCoroutine(DelayEndAddDamage(thongtin, valDamage, time));
        }
        else
        {
            Debug.LogError($"Thongtin component not found on {character.name}. Cannot apply damage modifier.");
        }


    }

    private IEnumerator DelayEndAddDamage(Thongtin thongtin, float valDamage, int time)
    {
        yield return new WaitForSeconds(time);
        thongtin.EndAddDamage(valDamage);
    }

    public override void AffectExpCharacter(GameObject character, int valExp)
    {
        if (character == null)
        {
            Debug.LogError("Character is null. Cannot apply experience modifier.");
            return;
        }

        // Lấy component Thongtin từ đối tượng character
        Thongtin thongtin = character.GetComponent<Thongtin>();
        if (thongtin != null)
        {
            thongtin.AddExp(valExp);
        }
        else
        {
            Debug.LogError($"Thongtin component not found on {character.name}. Cannot apply experience modifier.");
        }
    }

    public override bool EquipItem(GameObject character, bool isEquip, float valMaxHP, float valMaxMana, float valDamage, Sprite itemImage, int itemType)
    {
        if (character == null)
        {
            Debug.LogError("Character is null. Cannot apply equip item modifier.");
            return false;
        }
        Thongtin thongtin = character.GetComponent<Thongtin>();
        StatusUI statusUI = GameObject.FindAnyObjectByType<StatusUI>();

        if (!isEquip)
        {
            if (thongtin != null)
            {
                if (itemType == 0 && thongtin.playerType == Thongtin.PlayerType.Wizard && statusUI.ItemSword.GetComponent<SpriteRenderer>().sprite == null)
                {
                    thongtin.AddDamage(valDamage);
                    statusUI.TurnOnItemSord(itemImage);
                    return true;
                }
                if (itemType == 1 && thongtin.playerType == Thongtin.PlayerType.Warrior && statusUI.ItemSword.GetComponent<SpriteRenderer>().sprite == null)
                {
                    thongtin.AddDamage(valDamage);
                    statusUI.TurnOnItemSord(itemImage);
                    return true;
                }
                if (itemType == 2 && statusUI.ItemArmor.GetComponent<SpriteRenderer>().sprite == null)
                {
                    thongtin.AddMaxHP(valMaxHP);
                    statusUI.TurnOnItemArmor(itemImage);
                    return true;
                }
                if (itemType == 3 && statusUI.ItemShoe.GetComponent<SpriteRenderer>().sprite == null)
                {
                    thongtin.AddMaxMP(valMaxMana);
                    statusUI.TurnOnItemShoe(itemImage);
                    return true;
                }
            }
            return false;
        }
        else
        {
            if (thongtin != null)
            {
                thongtin.AddMaxHP(-valMaxHP);
                thongtin.AddMaxMP(-valMaxMana);
                thongtin.AddDamage(-valDamage);
                if (itemType == 0 || itemType == 1)
                {
                    statusUI.TurnOnItemSord(null);
                    return true;
                }
                if (itemType == 2)
                {
                    statusUI.TurnOnItemArmor(null);
                    return true;
                }
                if (itemType == 3)
                {
                    statusUI.TurnOnItemShoe(null);
                    return true;
                }
            }
            return false;
        }
    }

}
