using System.Collections.Generic;
using UnityEngine;

public class RankManager : MonoBehaviour
{
    void Start()
    {
        ShowLeaderboard();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ShowLeaderboard();
            Debug.Log("Danh sach nè");
        }
        
    }

    void ShowLeaderboard()
    {
        Debug.Log("đã gọi");
        List<PlayerInfo> players = Thongtin.GetAllPlayersFromThongtin();
        Debug.Log("Số lượng player: " + (players == null ? "null" : players.Count.ToString()));
        players.Sort((a, b) => b.level.CompareTo(a.level)); 

        foreach (var player in players)
        {
            Debug.Log($"Tên: {player.characterName}, Cấp: {player.level}");
        }
    }

}
