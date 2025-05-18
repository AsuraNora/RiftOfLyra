using System;
using System.Collections;
using UnityEngine;

public class Thongtin : MonoBehaviour
{
    public enum PlayerType
    {
        Warrior,
        Wizard
    }

    public string characterName;
    public int level = 1;
    public float maxHealth = 100;
    public float currentHealth;
    public float healthBonus = 20;
    public float attackDamage = 10;
    public float attackBonus = 5;
    public float maxMana = 50;
    public float currentMana;
    public float manaBonus = 10;
    public int currentExp = 0;
    public int expToLevelUp = 100;
    public float recoverHPAmount = 5f;
    public float recoverMPAmount = 50f;
    public PlayerType playerType;
    public Sprite avatar;

    public int skillPoints = 0;
    public int upgradeSkillPoint = 0;

    void Start()
    {
        LoadPlayerData();
        StartCoroutine(RecoverHP());
        StartCoroutine(RecoveMP());


    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        SavePlayerData();
        Debug.Log("Player took damage: " + damage + ", Current Health: " + currentHealth);
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("Player died");
        var attackNormal = GetComponent<AttackNormal>();
        if (attackNormal != null) attackNormal.enabled = false;

        var dichuyen = GetComponent<Dichuyển>();
        if (dichuyen != null) dichuyen.enabled = false;

        var wizardMovement = GetComponent<WizardMovement>();
        if (wizardMovement != null) wizardMovement.enabled = false;

        var playerController = GetComponent<PlayerController>();
        if (playerController != null) playerController.enabled = false;
    }

    public void GainExp(int exp)
    {
        currentExp += exp;
        Debug.Log("Gained " + exp + " EXP");
        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
        SavePlayerData();
    }

    public void LevelUp()
    {
        level++;
        currentExp -= expToLevelUp;
        expToLevelUp = Mathf.RoundToInt(expToLevelUp * 1.5f);

        maxHealth += healthBonus;
        currentHealth = maxHealth;
        attackDamage += attackBonus;
        maxMana += manaBonus;
        currentMana = maxMana;
        skillPoints += 1;
        upgradeSkillPoint += 1;

        Debug.Log("Level Up! New Level: " + level);
        SavePlayerData();
    }

    private void OnButtonHealthBonusClick()
    {
        if (skillPoints > 0)
        {
            maxHealth += 10;
            currentHealth += 10;
            skillPoints--;
            SavePlayerData();
        }
    }

    IEnumerator RecoveMP()
    {
        while (true)
        {
            yield return new WaitForSeconds(10);
            if (currentMana < maxMana)
            {
                currentMana = Mathf.Min(currentMana + recoverMPAmount, maxMana);
            }
        }
    }

    IEnumerator RecoverHP()
    {
        while (true)
        {
            yield return new WaitForSeconds(30);
            if (currentHealth < maxHealth)
            {
                currentHealth = Mathf.Min(currentHealth + recoverHPAmount, maxHealth);
            }
        }
    }


    public void SavePlayerData()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        PlayerPrefs.SetString(loggedInUser + "_CharacterName", characterName);
        PlayerPrefs.SetInt(loggedInUser + "_Level", level);
        PlayerPrefs.SetFloat(loggedInUser + "_MaxHealth", maxHealth);
        PlayerPrefs.SetFloat(loggedInUser + "_CurrentHealth", currentHealth);
        PlayerPrefs.SetFloat(loggedInUser + "_HealthBonus", healthBonus);
        PlayerPrefs.SetFloat(loggedInUser + "_AttackDamage", attackDamage);
        PlayerPrefs.SetFloat(loggedInUser + "_AttackBonus", attackBonus);
        PlayerPrefs.SetFloat(loggedInUser + "_MaxMana", maxMana);
        PlayerPrefs.SetFloat(loggedInUser + "_CurrentMana", currentMana);
        PlayerPrefs.SetFloat(loggedInUser + "_ManaBonus", manaBonus);
        PlayerPrefs.SetInt(loggedInUser + "_CurrentExp", currentExp);
        PlayerPrefs.SetInt(loggedInUser + "_ExpToLevelUp", expToLevelUp);
        PlayerPrefs.SetInt(loggedInUser  + "_PlayerType", (int)playerType);
        PlayerPrefs.SetInt(loggedInUser + "_SkillPoints", skillPoints);
        PlayerPrefs.SetInt(loggedInUser + "_UpgradeSkillPoint", upgradeSkillPoint);
        PlayerPrefs.Save();
        Debug.Log("Dữ liệu người chơi đã được lưu trữ");
    }

    public void LoadPlayerData()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        if (PlayerPrefs.HasKey(loggedInUser + "_Level"))
        {
            characterName = PlayerPrefs.GetString(loggedInUser + "_CharacterName");
            level = PlayerPrefs.GetInt(loggedInUser + "_Level");
            maxHealth = PlayerPrefs.GetFloat(loggedInUser + "_MaxHealth");
            currentHealth = PlayerPrefs.GetFloat(loggedInUser + "_MaxHealth");
            healthBonus = PlayerPrefs.GetFloat(loggedInUser + "_HealthBonus");
            attackDamage = PlayerPrefs.GetFloat(loggedInUser + "_AttackDamage");
            attackBonus = PlayerPrefs.GetFloat(loggedInUser + "_AttackBonus");
            maxMana = PlayerPrefs.GetFloat(loggedInUser + "_MaxMana");
            currentMana = PlayerPrefs.GetFloat(loggedInUser + "_MaxMana");
            manaBonus = PlayerPrefs.GetFloat(loggedInUser + "_ManaBonus");
            currentExp = PlayerPrefs.GetInt(loggedInUser + "_CurrentExp");
            expToLevelUp = PlayerPrefs.GetInt(loggedInUser + "_ExpToLevelUp");
            playerType = (PlayerType)PlayerPrefs.GetInt(loggedInUser + "_PlayerType");
            skillPoints = PlayerPrefs.GetInt(loggedInUser + "_SkillPoints");
            upgradeSkillPoint = PlayerPrefs.GetInt(loggedInUser + "_UpgradeSkillPoint");
            Debug.Log("Dữ liệu người chơi đã được tải");
        }
        else
        {
            currentHealth = maxHealth;
            currentMana = maxMana;
        }
    }

    internal void AddHealth(float val)
    {
        currentHealth += val;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    internal void AddMana(float val)
    {
        currentMana += val;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }

    internal void AddDamage(float val)
    {
        attackDamage += val;
    }

    internal void EndAddDamage(float val)
    {
        attackDamage -= val;
    }

    internal void AddExp(int val)
    {
        currentExp += val;
        if (currentExp >= expToLevelUp)
        {
            LevelUp();
        }
    }

    internal void AddMaxHP(float valMaxHP)
    {
        maxHealth += valMaxHP;
        currentHealth += valMaxHP;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    internal void AddMaxMP(float valMaxMP)
    {
        maxMana += valMaxMP;
        currentMana += valMaxMP;
        if (currentMana > maxMana)
        {
            currentMana = maxMana;
        }
    }
}
