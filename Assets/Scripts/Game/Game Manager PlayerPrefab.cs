// using System;
// using UnityEngine;
// using Inventory.Model;

// public class GameManagerPlayerPrefab : MonoBehaviour
// {
//     public static event Action<InventorySO> OnPlayerCreated;

//     [SerializeField] private GameObject playerPrefab;

//     private void Start()
//     {
//         // Tạo PlayerPrefab
//         GameObject player = Instantiate(playerPrefab);

//         // Lấy InventorySO từ PlayerPrefab
//         InventorySO playerInventory = player.GetComponent<PlayerController>().GetInventory();

//         // Gọi sự kiện để thông báo cho PickUpSystem
//         OnPlayerCreated?.Invoke(playerInventory);
//     }
// }
