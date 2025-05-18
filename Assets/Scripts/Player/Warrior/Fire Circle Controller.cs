using UnityEngine;

public class FireCircleController : MonoBehaviour
{
    public GameObject poisonSlashPrefab; // Prefab của kỹ năng
    public Transform firePoint; // Vị trí bắn kỹ năng
    public float manaCost = 20f; // Lượng mana tiêu tốn
    public float cooldownTime = 5f; // Thời gian hồi chiêu
    public float fireBombDame = 10f; // Sát thương của kỹ năng

    private float lastSkillUseTime = -Mathf.Infinity; // Thời gian sử dụng kỹ năng lần cuối
    public int textLevelSkillFireCircle = 1; // Cấp độ kỹ năng

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CastPoisonSlash();
        }
    }

    void CastPoisonSlash()
    {
        Thongtin thongtin = GetComponent<Thongtin>();
        if (thongtin != null)
        {
            // Kiểm tra mana và thời gian hồi chiêu
            if (Time.time >= lastSkillUseTime + cooldownTime && thongtin.currentMana >= manaCost && thongtin.level >= 3 )
            {
                // Tiêu tốn mana
                thongtin.currentMana -= manaCost;

                // Tạo kỹ năng
                Instantiate(poisonSlashPrefab, firePoint.position, firePoint.rotation);

                // Cập nhật thời gian sử dụng kỹ năng
                lastSkillUseTime = Time.time;

                Debug.Log("Kỹ năng Poison Slash được kích hoạt!");
            }
            else if (thongtin.currentMana < manaCost)
            {
                Debug.Log("Không đủ mana để sử dụng kỹ năng!");
            }
            else
            {
                Debug.Log("Kỹ năng đang trong thời gian hồi chiêu!");
            }
        }
        else
        {
            Debug.LogError("Không tìm thấy component Thongtin trên nhân vật!");
        }
    }
}
