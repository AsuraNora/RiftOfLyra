using UnityEngine;
using System;

[System.Serializable]
public class Quest
{
    public string questName;
    public string description;
    public EnemyAI.EnemyType targetEnemyType; // Loại quái cần tiêu diệt
    public int targetKillCount; // Số lượng quái cần tiêu diệt
    public int currentKillCount; // Số lượng đã tiêu diệt
    public bool isCompleted;
    public int rewardExp; // Phần thưởng EXP

    public event Action<Quest> OnQuestCompleted; // Sự kiện khi hoàn thành nhiệm vụ

    public Quest(string name, string desc, EnemyAI.EnemyType enemy, int target, int reward)
    {
        questName = name;
        description = desc;
        targetEnemyType = enemy;
        targetKillCount = target;
        currentKillCount = 0;
        isCompleted = false;
        rewardExp = reward;
    }

    public void IncrementKillCount(EnemyAI.EnemyType enemyType)
    {
        if (isCompleted || enemyType != targetEnemyType) return;

        currentKillCount++;
        Debug.Log($"Đã tiêu diệt {currentKillCount}/{targetKillCount} {targetEnemyType}");

        if (currentKillCount >= targetKillCount)
        {
            CompleteQuest();
        }
    }

    private void CompleteQuest()
    {
        if (isCompleted) return;

        isCompleted = true;
        Debug.Log($"Nhiệm vụ hoàn thành: Tiêu diệt {targetKillCount} {targetEnemyType}. Nhận {rewardExp} EXP!");

        OnQuestCompleted?.Invoke(this);
    }
}
