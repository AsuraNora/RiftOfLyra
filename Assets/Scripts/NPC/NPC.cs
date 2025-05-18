using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    public Quest quest;
    public float interactionRange = 2f; 
    public GameObject canvasCommunication; 
    public TMP_Text questText;
    [SerializeField] private GameObject SoilderThink;
    private bool playerInRange = false;
    private string playerCharacter;

    void Start()
    {
        canvasCommunication.SetActive(false);
        playerCharacter = PlayerPrefs.GetString("LoggedInCharacter", "Unknown");
        LoadQuestFromPlayerPrefs();
        SoilderThink.SetActive(false);
    }

    void Update()
    {
        CheckPlayerInRange();

        if (Input.GetKeyDown(KeyCode.C) && playerInRange)
        {
            if (!PlayerPrefs.HasKey(playerCharacter + "_QuestStatus"))
            {
                AssignNewQuest();
            }
            else
            {
                Debug.Log("Bạn chưa hoàn thành nhiệm vụ hiện tại!");
            }
            canvasCommunication.SetActive(true);
            questText.text = GetQuestInfo();
        }

        if (canvasCommunication.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return)) ProcessQuest();
            else if (Input.GetKeyDown(KeyCode.Escape)) canvasCommunication.SetActive(false);
        }
    }

    void CheckPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        playerInRange = false;
        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                playerInRange = true;
                SoilderThink.SetActive(true);
                break;
            }
            else
            {
                SoilderThink.SetActive(false);
            }
        }
    }

    void AssignNewQuest()
    {
        // Loại quái được chọn
        EnemyAI.EnemyType randomEnemy = (EnemyAI.EnemyType)Random.Range(0, System.Enum.GetValues(typeof(EnemyAI.EnemyType)).Length);

        // Số lượng quái ngẫu nhiên
        int randomTarget = Random.Range(3, 10);

        // Phần thưởng EXP ngẫu nhiên
        int randomRewardExp = Random.Range(50, 200);

        // Lưu thông tin nhiệm vụ vào PlayerPrefs
        PlayerPrefs.SetInt(playerCharacter + "_QuestStatus", 1);
        PlayerPrefs.SetInt(playerCharacter + "_QuestProgress", 0);
        PlayerPrefs.SetInt(playerCharacter + "_QuestTarget", randomTarget);
        PlayerPrefs.SetString(playerCharacter + "_QuestEnemy", randomEnemy.ToString());
        PlayerPrefs.SetInt(playerCharacter + "_QuestRewardExp", randomRewardExp);
        PlayerPrefs.Save();

        // Tạo nhiệm vụ
        quest = new Quest(
            $"Tiêu diệt {randomEnemy}",
            $"Hãy tiêu diệt {randomTarget} {randomEnemy} để nhận {randomRewardExp} EXP.",
            randomEnemy,
            randomTarget,
            randomRewardExp
        );
    }

    void ProcessQuest()
    {
        if (!PlayerPrefs.HasKey(playerCharacter + "_QuestStatus"))
        {
            AssignNewQuest();
        }
        else
        {
            int progress = PlayerPrefs.GetInt(playerCharacter + "_QuestProgress", 0);
            int target = PlayerPrefs.GetInt(playerCharacter + "_QuestTarget", 5);
            string enemyType = PlayerPrefs.GetString(playerCharacter + "_QuestEnemy", "Unknown");
            int rewardExp = PlayerPrefs.GetInt(playerCharacter + "_QuestRewardExp", 0);

            if (progress >= target)
            {
                Debug.Log($"✅ Nhiệm vụ hoàn thành! Nhận {rewardExp} EXP!");
                PlayerPrefs.DeleteKey(playerCharacter + "_QuestStatus");
                PlayerPrefs.DeleteKey(playerCharacter + "_QuestProgress");
                PlayerPrefs.DeleteKey(playerCharacter + "_QuestTarget");
                PlayerPrefs.DeleteKey(playerCharacter + "_QuestEnemy");
                PlayerPrefs.DeleteKey(playerCharacter + "_QuestRewardExp");
                PlayerPrefs.Save();
            }
            else
            {
                Debug.Log($"Nhiệm vụ chưa hoàn thành! ({progress}/{target} {enemyType})");
            }
        }

        questText.text = GetQuestInfo();
        canvasCommunication.SetActive(false);
    }

    void LoadQuestFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(playerCharacter + "_QuestStatus"))
        {
            int savedProgress = PlayerPrefs.GetInt(playerCharacter + "_QuestProgress", 0);
            int targetKills = PlayerPrefs.GetInt(playerCharacter + "_QuestTarget");
            string savedEnemyType = PlayerPrefs.GetString(playerCharacter + "_QuestEnemy", "Slime");
            int rewardExp = PlayerPrefs.GetInt(playerCharacter + "_QuestRewardExp", 0);

            EnemyAI.EnemyType enemyType = (EnemyAI.EnemyType)System.Enum.Parse(typeof(EnemyAI.EnemyType), savedEnemyType);
            quest = new Quest(
                $"Tiêu diệt {enemyType}",
                $"Hãy tiêu diệt {targetKills} {enemyType} để nhận {rewardExp} EXP.",
                enemyType,
                targetKills,
                rewardExp
            );
        }
        else
        {
            quest = new Quest("Chưa có nhiệm vụ", "Hãy nhận nhiệm vụ từ NPC.", EnemyAI.EnemyType.SlimeLv1, 0, 0);
        }
    }

    string GetQuestInfo()
    {
        if (!PlayerPrefs.HasKey(playerCharacter + "_QuestStatus"))
        {
            return $"Quest: {quest.questName}\n➡ Yêu cầu: {quest.description}\nChấp nhận: Enter - Từ chối: ESC";
        }
        else
        {
            int progress = PlayerPrefs.GetInt(playerCharacter + "_QuestProgress", 0);
            int target = PlayerPrefs.GetInt(playerCharacter + "_QuestTarget");
            string enemyType = PlayerPrefs.GetString(playerCharacter + "_QuestEnemy", "Slime");
            int rewardExp = PlayerPrefs.GetInt(playerCharacter + "_QuestRewardExp", 0);

            return $"Quest: Tiêu diệt {enemyType}\nTiến độ: {progress}/{target} {enemyType}\nPhần thưởng: {rewardExp} EXP\nTiếp tục: Enter - Đóng: ESC";
        }
    }
}
