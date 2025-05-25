using System.Collections.Generic;
using UnityEngine;

public class RankUI : MonoBehaviour
{

    void Start()
    {
        ShowLeaderboard();
    }

    void ShowLeaderboard()
    {
        List<PlayerInfo> players = Thongtin.GetAllPlayersFromThongtin();
        players.Sort((a, b) => b.level.CompareTo(a.level)); // xếp theo level giảm dần

        foreach (var player in players)
        {
            Debug.Log($"Tên: {player.characterName}, Cấp: {player.level}");
        }
    }

}
