using UnityEngine;

public class FireCircleController : MonoBehaviour
{
    public GameObject poisonSlashPrefab;
    public Transform firePoint;
    public float manaCost = 20f;
    public float cooldownTime = 5f;
    public float fireBombDame = 10f;

    private float lastSkillUseTime = -Mathf.Infinity;
    public int textLevelSkillFireCircle = 1;

    void Start()
    {
        LoadCircleData();
    }

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
            if (Time.time >= lastSkillUseTime + cooldownTime && thongtin.currentMana >= manaCost)
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
    
    public void SaveFireCircleData()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        PlayerPrefs.SetInt(loggedInUser + "_FireCircleLevel", textLevelSkillFireCircle);
        PlayerPrefs.SetFloat(loggedInUser + "_FireCircleDame", fireBombDame);
        PlayerPrefs.Save();
    }
    
    private void LoadCircleData()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        textLevelSkillFireCircle = PlayerPrefs.GetInt(loggedInUser + "_FireCircleLevel", textLevelSkillFireCircle);
        fireBombDame = PlayerPrefs.GetFloat(loggedInUser + "_FireMeteoriteDame", fireBombDame);
    }

}
