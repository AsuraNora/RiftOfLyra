using UnityEngine;
using System.Collections;
using Inventory.Model;
using Inventory;

public class GameManager : MonoBehaviour
{
    public GameObject warriorPrefab;
    public GameObject wizardPrefab;
    public Transform spawnPoint;
    [SerializeField] private GameObject canvasCommunication;
    [SerializeField] private TMPro.TMP_Text notEnoughGoldText;
    [SerializeField] private InventorySO inventorySO; // Tham chiếu đến InventorySO
    [SerializeField] private InventoryController inventoryController; // Tham chiếu đến InventorySO trong GameManager

    void Start()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        string characterName = PlayerPrefs.GetString(loggedInUser + "_CharacterName", "DefaultName");
        string characterClass = PlayerPrefs.GetString(loggedInUser + "_CharacterClass", "Warrior");

        Debug.Log("Character Name: " + characterName);
        Debug.Log("Character Class: " + characterClass);

        // Chọn prefab tương ứng với class
        GameObject prefabToUse = characterClass == "Wizard" ? wizardPrefab : warriorPrefab;

        GameObject character = Instantiate(prefabToUse, spawnPoint.position, spawnPoint.rotation);
        character.name = characterName;

        Camera mainCam = Camera.main;
        if (mainCam != null)
        {
            mainCam.transform.SetParent(character.transform);
            mainCam.transform.localPosition = new Vector3(0, 0f, -4);
            mainCam.transform.localRotation = Quaternion.identity;
        }

        Thongtin characterScript = character.GetComponent<Thongtin>();
        if (characterScript != null)
        {
            characterScript.characterName = characterName;
            characterScript.LoadPlayerData();
        }
        canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);
        notEnoughGoldText.gameObject.SetActive(false); // Ẩn thông báo không đủ vàng khi bắt đầu
    }

    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin playerStats = player.GetComponent<Thongtin>();
            if (playerStats != null && playerStats.currentHealth <= 0)
            {
                canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            }
        }
    }

    public void OnReviveButtonClicked()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin playerStats = player.GetComponent<Thongtin>();
            if (playerStats != null)
            {
                // Kiểm tra trong túi đồ
                int goldCoinIndex = -1;
                foreach (var item in inventorySO.GetCurrentInventoryState())
                {
                    if (item.Value.item.itemName == "Gold Coin")
                    {
                        goldCoinIndex = item.Key; // Lưu lại vị trí của Gold Coin
                        break;
                    }
                }

                if (goldCoinIndex != -1) // Nếu tìm thấy Gold Coin
                {
                    // Gọi hàm DropItem để giảm số lượng Gold Coin
                    inventoryController.DropItem(goldCoinIndex, 1);

                    // Hồi sinh nhân vật
                    playerStats.currentHealth = playerStats.maxHealth;
                    canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);

                   ActivePlayer(); // Kích hoạt lại các thành phần của nhân vật

                    Debug.Log("Player revived using Gold Coin!");
                }
                else
                {
                    Debug.LogWarning("No Gold Coin found in inventory. Cannot revive.");
                    notEnoughGoldText.gameObject.SetActive(true);
                }
            }
        }
    }

    public void OnExitButtonClicked()
    {
        notEnoughGoldText.gameObject.SetActive(false); // Ẩn thông báo không đủ vàng khi nhấn nút thoát
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin playerStats = player.GetComponent<Thongtin>();
            if (playerStats != null)
            {
                playerStats.currentHealth = playerStats.maxHealth;
                player.transform.position = spawnPoint.position;
                canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);
                ActivePlayer();
            }
        }
    }

    private void ActivePlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin playerStats = player.GetComponent<Thongtin>();
            if (playerStats != null)
            {
                var attackNormal = playerStats.GetComponent<AttackNormal>();
                if (attackNormal != null) attackNormal.enabled = true;

                var dichuyen = playerStats.GetComponent<Dichuyển>();
                if (dichuyen != null) dichuyen.enabled = true;

                var wizardMovement = playerStats.GetComponent<WizardMovement>();
                if (wizardMovement != null) wizardMovement.enabled = true;

                var playerController = playerStats.GetComponent<PlayerController>();
                if (playerController != null) playerController.enabled = true;
            }
        }
    }

    public void RespawnEnemy(GameObject enemy, float respawnTime)
    {
        StartCoroutine(RespawnCoroutine(enemy, respawnTime));
    }

    IEnumerator RespawnCoroutine(GameObject enemy, float respawnTime)
    {
        yield return new WaitForSeconds(respawnTime);
        enemy.SetActive(true);
    }
}
