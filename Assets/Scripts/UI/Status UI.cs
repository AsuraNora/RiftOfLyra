using UnityEngine;

public class StatusUI : MonoBehaviour
{
    public TextMesh playerNameText;
    public TextMesh levelText;
    public TextMesh healthText;
    public TextMesh manaText;
    public TextMesh attackText;
    public TextMesh expText;
    [SerializeField] public GameObject ItemSword;
    [SerializeField] public GameObject ItemArmor;
    [SerializeField] public GameObject ItemShoe;
    //public Image imageAvatar;
    public TextMesh skillPointsText;

    private Thongtin playerThongtin;

    void Start()
    {
        Thongtin playerThongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (playerThongtin != null)
        {
            playerThongtin.LoadPlayerData();
            LoadItemSword();
        }

    }

    public void LoadItemSword()
    {
        string spriteName = PlayerPrefs.GetString("ItemSwordSprite", "");
        if (!string.IsNullOrEmpty(spriteName))
        {
            Sprite loadedSprite = Resources.Load<Sprite>(spriteName);
            if (loadedSprite != null)
            {
                SpriteRenderer sr = ItemSword.GetComponent<SpriteRenderer>();
                if (sr != null)
                {
                    sr.sprite = loadedSprite;
                }
                else
                {
                    Debug.LogWarning("ItemSword does not have a SpriteRenderer component.");
                }
            }
            else
            {
                Debug.LogWarning("Không tìm thấy sprite trong Resources với tên: " + spriteName);
            }
        }
    }

    public void SaveItemSword(Sprite newSprite)
    {
        SpriteRenderer img = ItemSword.GetComponent<SpriteRenderer>();
        img.sprite = newSprite;
        // Lưu sprite vào PlayerPrefs
        if (img.sprite != null)
        {
            PlayerPrefs.SetString("ItemSwordSprite", img.sprite.name);
            PlayerPrefs.Save();
        }
    }

    void Update()
    {
        playerThongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (playerThongtin != null)
        {
            skillPointsText.text = playerThongtin.skillPoints.ToString();
            playerNameText.text = playerThongtin.characterName;
            GameObject.FindWithTag("Avatar").GetComponent<SpriteRenderer>().sprite = playerThongtin.avatar;
            levelText.text = "Level: " + playerThongtin.level;
            healthText.text = "Health: " + playerThongtin.currentHealth + "/" + playerThongtin.maxHealth;
            manaText.text = "Mana: " + playerThongtin.currentMana + "/" + playerThongtin.maxMana;
            attackText.text = "Attack: " + playerThongtin.attackDamage;
            expText.text = "EXP: " + playerThongtin.currentExp + "/" + playerThongtin.expToLevelUp;
        }
    }

    public void OnButtonHealthBonusClick()
    {
        playerThongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (playerThongtin.skillPoints > 0)
        {
            playerThongtin.maxHealth += 10;
            playerThongtin.currentHealth += 10;
            playerThongtin.skillPoints--;
            playerThongtin.SavePlayerData();
        }
    }

    public void OnButtonManaBonusClick()
    {
        playerThongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (playerThongtin.skillPoints > 0)
        {
            playerThongtin.maxMana += 10;
            playerThongtin.currentMana += 10;
            playerThongtin.skillPoints--;
            playerThongtin.SavePlayerData();
        }
    }

    public void OnButtonAttackBonusClick()
    {
        playerThongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (playerThongtin.skillPoints > 0)
        {
            playerThongtin.attackDamage += 5; ;
            playerThongtin.skillPoints--;
            playerThongtin.SavePlayerData();
        }
    }

    public void UseMana(int mana)
    {
        playerThongtin.currentMana -= mana;
        if (playerThongtin.currentMana < 0)
        {
            playerThongtin.currentMana = 0;
        }
        playerThongtin.SavePlayerData();
    }

    public void TurnOnItemSord(Sprite newSprite)
    {
        SpriteRenderer img = ItemSword.GetComponent<SpriteRenderer>();
        img.sprite = newSprite;
    }
    public void TurnOnItemArmor(Sprite newSprite)
    {
        SpriteRenderer img = ItemArmor.GetComponent<SpriteRenderer>();
        img.sprite = newSprite;
    }
    public void TurnOnItemShoe(Sprite newSprite)
    {
        SpriteRenderer img = ItemShoe.GetComponent<SpriteRenderer>();
        img.sprite = newSprite;
    }
}
