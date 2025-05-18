using UnityEngine;

public class SkillPoisionSlashController : MonoBehaviour
{
    [Header("Fire Bomb Info")]
    [SerializeField] private GameObject fireBombPrefab;
    [SerializeField] public float fireBombDame;
    [SerializeField] private float cooldownTime;
    [SerializeField] private float manaCost;
    [SerializeField] private int levelRequest;
    [SerializeField] public int levelSkill = 1;

    [Header("Distance")]
    [SerializeField] private float maxDistance = 15f;
    [SerializeField] private GameObject rangeIndicatorPrefab; 

    private float lastFireBombTime = -Mathf.Infinity;
    private GameObject rangeIndicator; 

    void Start()
    {
        // Tạo vòng tròn nhưng ẩn đi ban đầu
        if (rangeIndicatorPrefab != null)
        {
            rangeIndicator = Instantiate(rangeIndicatorPrefab, transform.position, Quaternion.identity);
            rangeIndicator.transform.localScale = new Vector3(maxDistance * 2, maxDistance * 2, 1); // Đặt kích thước vòng tròn
            rangeIndicator.SetActive(false); // Ẩn vòng tròn
        }
    }

    void Update()
    {
        HandleRangeIndicator();
    }



    private void HandleRangeIndicator()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
             FireBomb();
        }
    }

    private void FireBomb()
    {
        if (Time.time >= lastFireBombTime + cooldownTime)
        {
            Debug.Log("Fire Bomb");
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Thongtin playerInfo = player.GetComponent<Thongtin>();

            if (playerInfo != null && playerInfo.currentMana >= manaCost && playerInfo.level >= levelRequest )
            {
                Debug.Log("Fire Bomb Done");

                // Tiêu tốn mana
                playerInfo.currentMana -= manaCost;

                // Lấy vị trí chuột trong thế giới
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                // Tạo kỹ năng tại vị trí chuột
                GameObject fireBomb = Instantiate(fireBombPrefab, mousePosition, Quaternion.identity);

                // Cập nhật thời gian sử dụng kỹ năng
                lastFireBombTime = Time.time;

                Debug.Log("Fire Bomb được tạo tại vị trí: " + mousePosition);
            }
            else
            {
                Debug.Log("Không đủ mana hoặc cấp độ không đủ để sử dụng kỹ năng!");
            }
        }
    }
}
