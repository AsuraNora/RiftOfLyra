// using UnityEngine;
// using UnityEngine.UI;

// public class HealthBarUI : MonoBehaviour
// {
//     public Slider healthBarSlider; 
//     public Text healthText; 

//     void Start()
//     {
//         GameObject player = GameObject.FindGameObjectWithTag("Player");
//         if (player == null)
//         {
//             Debug.LogError("Player not found in the scene.");
//             return;
//         }
//         playerStats = player.GetComponent<Thongtin>();
        
//         UpdateHealthBar(); 
//     }

//     void Update()
//     {
//         UpdateHealthBar(); 
//     }

//     void UpdateHealthBar()
//     {
//         if (playerStats == null) return;

//         healthBarSlider.value = playerStats.currentHealth / playerStats.maxHealth; 
//         healthText.text = $"{playerStats.currentHealth}/{playerStats.maxHealth}"; 
//     }
// }
