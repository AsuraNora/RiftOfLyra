// using UnityEngine;

// public class PlayerSpawner : MonoBehaviour
// {
//     public GameObject warriorPrefab;
//     public GameObject wizardPrefab;
//     public Transform spawnPoint;

//     void Start()
//     {
//         string username = PlayerPrefs.GetString("LoggedInUser", "");
//         string characterClass = PlayerPrefs.GetString(username + "_CharacterClass", "Warrior");

//         GameObject prefabToSpawn = characterClass == "Warrior" ? warriorPrefab : wizardPrefab;
//         GameObject player = Instantiate(prefabToSpawn, spawnPoint.position, Quaternion.identity);

//         // Gắn Main Camera vào Player
//         Camera mainCam = Camera.main;
//         if (mainCam != null)
//         {
//             mainCam.transform.SetParent(player.transform);
//             mainCam.transform.localPosition = new Vector3(0, 0f, -4);
//             mainCam.transform.localRotation = Quaternion.identity;
//         }
//     }
// }
