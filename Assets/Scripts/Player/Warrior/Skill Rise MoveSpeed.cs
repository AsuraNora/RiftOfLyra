using UnityEngine;

public class SkillRiseMoveSpeed : MonoBehaviour
{
    public float manaCost = 20f; // Lượng mana tiêu tốn
    public float cooldownTime = 5f; // Thời gian hồi chiêu
    public float boostDuration = 3f; // Thời gian tăng tốc
    public float speedMultiplier = 2f; 
    [SerializeField] private GameObject skillEffect;

    private float lastSkillUseTime = -Mathf.Infinity; 
    private bool isBoostActive = false; 
    private float boostEndTime; 
    public int levelSkillRiseSpeed = 1; // Cấp độ kỹ năng

    void Start()
        {
            if (skillEffect != null)
            {
                skillEffect.SetActive(false); // Tắt hiệu ứng kỹ năng ban đầu
            }
        }
    void Update()
    {
        Thongtin thongtin = GetComponent<Thongtin>();
        Dichuyển dichuyen = GetComponent<Dichuyển>();

        if (Input.GetKeyUp(KeyCode.Space))
        {
            if (thongtin != null && dichuyen != null)
            {
                // Kiểm tra mana và thời gian hồi chiêu
                if (Time.time >= lastSkillUseTime + cooldownTime && thongtin.currentMana >= manaCost)
                {
                    // Tiêu tốn mana
                    thongtin.currentMana -= manaCost;

                    // Kích hoạt tăng tốc
                    isBoostActive = true;
                    boostEndTime = Time.time + boostDuration;
                    lastSkillUseTime = Time.time;

                    // Tăng tốc độ di chuyển
                    dichuyen.moveSpeed *= speedMultiplier;
                    skillEffect.SetActive(true); // Kích hoạt hiệu ứng kỹ năng
                    Debug.Log("Tăng tốc độ di chuyển: " + dichuyen.moveSpeed);
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
                Debug.LogError("Thongtin hoặc Dichuyển component không được tìm thấy trên " + gameObject.name);
            }
        }

        // Kiểm tra nếu thời gian tăng tốc kết thúc
        if (isBoostActive && Time.time >= boostEndTime)
        {
            isBoostActive = false;
            skillEffect.SetActive(false); // Tắt hiệu ứng kỹ năng
            dichuyen.moveSpeed /= speedMultiplier; // Trả tốc độ di chuyển về bình thường
            Debug.Log("Tăng tốc độ di chuyển đã kết thúc.");
        }
    }
}
