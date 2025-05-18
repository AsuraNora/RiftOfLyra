// using System.Collections.Generic;
// using UnityEngine;

// public class Target : MonoBehaviour
// {
//     public float targetRange = 5f;
//     public GameObject arrowPrefab; // Prefab của mũi tên
//     public float arrowOffset; // Khoảng cách từ mũi tên đến quái vật
//     private GameObject arrowInstance; // Instance của mũi tên
//     private List<Transform> targetsInRange = new List<Transform>();
//     private int currentTargetIndex = -1;
//     public Transform currentTarget;
//     private AttackNormal playerAttack; // Tham chiếu đến script AttackNormal của người chơi

//     void Start()
//     {
//         if (arrowPrefab != null)
//         {
//             arrowInstance = Instantiate(arrowPrefab);
//             arrowInstance.SetActive(false);
//         }
//         else
//         {
//             Debug.LogError("arrowPrefab chưa được gán!");
//         }

//         playerAttack = GetComponent<AttackNormal>();

//         // TEST: thử chọn target ngay lúc bắt đầu
//         Invoke(nameof(SwitchTarget), 1f);
//     }


//     void Update()
//     {
//         Debug.Log("Target Update is running");
//         if (currentTarget == null || !currentTarget.gameObject.activeInHierarchy)
//         {
//             ClearTarget();
//             return;
//         }

//         // Thử thêm log kiểm tra phím
//         if (Input.GetKeyDown(KeyCode.T))
//         {
//             Debug.Log("Key T pressed");
//             SwitchTarget();
//         }
//         else
//         {
//             Debug.Log("Key T not pressed");
//         }

//         if (arrowInstance != null && currentTarget != null)
//         {
//             arrowInstance.transform.position = currentTarget.position + Vector3.up * arrowOffset;
//         }
//     }




//     void SwitchTarget()
//     {
//         // Lấy danh sách mục tiêu gần nhất
//         FindTargetsInRange();

//         if (targetsInRange.Count == 0) return;

//         // Chuyển qua mục tiêu tiếp theo
//         currentTargetIndex = (currentTargetIndex + 1) % targetsInRange.Count;
//         currentTarget = targetsInRange[currentTargetIndex];

//         // Đánh dấu mục tiêu
//         HighlightTarget(currentTarget);

//         // Thiết lập mục tiêu cho người chơi
//         playerAttack.SetTarget(currentTarget);
//     }

//     void FindTargetsInRange()
//     {
//         targetsInRange.Clear();
//         Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, targetRange);
//         foreach (var col in colliders)
//         {
//             EnemyAI enemy = col.GetComponent<EnemyAI>();
//             NPC npc = col.GetComponent<NPC>();

//             if (enemy != null && enemy.gameObject.activeInHierarchy)
//             {
//                 targetsInRange.Add(enemy.transform);
//             }
//             else if (npc != null && npc.gameObject.activeInHierarchy)
//             {
//                 targetsInRange.Add(npc.transform);
//             }
//         }

//         Debug.Log("Số lượng target trong phạm vi: " + targetsInRange.Count);

//         targetsInRange.Sort((a, b) =>
//             Vector2.Distance(transform.position, a.position)
//             .CompareTo(Vector2.Distance(transform.position, b.position)));
//     }



//     void HighlightTarget(Transform target)
//     {
//         if (arrowInstance == null || target == null)
//         {
//             Debug.LogWarning("arrowInstance hoặc target bị null.");
//             return;
//         }

//         Debug.Log("Highlight target: " + target.name);
//         arrowInstance.SetActive(true);
//         arrowInstance.transform.position = target.position + Vector3.up * arrowOffset;
//     }


//     public void ClearTarget()
//     {
//         currentTarget = null;

//         if (arrowInstance != null && !ReferenceEquals(arrowInstance, null))
//         {
//             arrowInstance.SetActive(false); // Ẩn mũi tên nếu còn tồn tại
//         }
//     }

// }
