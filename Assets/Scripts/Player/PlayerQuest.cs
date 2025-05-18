using UnityEngine;
using System;

public class PlayerQuest : MonoBehaviour
{
    private string playerCharacter;
    public Quest CurrentQuest { get; private set; } 

    void Start()
    {
        playerCharacter = PlayerPrefs.GetString("LoggedInCharacter", "Unknown");
        LoadQuestFromPlayerPrefs();
    }

    public void EnemyKilled(EnemyAI.EnemyType enemyType)
    {
        if (CurrentQuest == null || CurrentQuest.isCompleted) return;

        CurrentQuest.IncrementKillCount(enemyType);
        SaveQuestProgress();

        if (CurrentQuest.isCompleted)
        {
            CompleteQuest();
        }
    }

    private void SaveQuestProgress()
    {
        if (CurrentQuest != null)
        {
            PlayerPrefs.SetInt(playerCharacter + "_QuestProgress", CurrentQuest.currentKillCount);
            PlayerPrefs.Save();
        }
    }

    private void LoadQuestFromPlayerPrefs()
    {
        if (PlayerPrefs.HasKey(playerCharacter + "_QuestStatus"))
        {
            int savedProgress = PlayerPrefs.GetInt(playerCharacter + "_QuestProgress", 0);
            int targetKills = PlayerPrefs.GetInt(playerCharacter + "_QuestTarget", 5);
            string savedEnemyType = PlayerPrefs.GetString(playerCharacter + "_QuestEnemy", "Slime");
            int rewardExp = PlayerPrefs.GetInt(playerCharacter + "_QuestRewardExp", 0);

            EnemyAI.EnemyType enemyType = (EnemyAI.EnemyType)Enum.Parse(typeof(EnemyAI.EnemyType), savedEnemyType);

            CurrentQuest = new Quest(
                $"Tiêu diệt {enemyType}",
                $"Hãy tiêu diệt {targetKills} {enemyType} để nhận {rewardExp} EXP.",
                enemyType,
                targetKills,
                rewardExp
            );
            CurrentQuest.currentKillCount = savedProgress;
        }
    }

    private void CompleteQuest()
    {
        if (CurrentQuest == null || !CurrentQuest.isCompleted) return;

        Debug.Log($"✅ Nhiệm vụ hoàn thành: {CurrentQuest.questName}. Nhận {CurrentQuest.rewardExp} EXP!");

        // Xóa nhiệm vụ khỏi PlayerPrefs
        PlayerPrefs.DeleteKey(playerCharacter + "_QuestStatus");
        PlayerPrefs.DeleteKey(playerCharacter + "_QuestProgress");
        PlayerPrefs.DeleteKey(playerCharacter + "_QuestTarget");
        PlayerPrefs.DeleteKey(playerCharacter + "_QuestEnemy");
        PlayerPrefs.DeleteKey(playerCharacter + "_QuestRewardExp");
        PlayerPrefs.Save();

        // Đặt CurrentQuest về null
        CurrentQuest = null;
    }
}
