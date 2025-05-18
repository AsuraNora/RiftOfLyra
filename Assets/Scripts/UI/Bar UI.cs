using UnityEngine;

public class BarUI : MonoBehaviour
{
    [SerializeField] private RectTransform healthBar; // Panel thanh máu
    [SerializeField] private RectTransform manaBar;   // Panel thanh mana
    private Thongtin playerStats;
    private float newWidth;

    private float healthBarOriginalWidth; // Chiều rộng gốc của thanh máu
    private float manaBarOriginalWidth;   // Chiều rộng gốc của thanh mana

    void Start()
    {
        // Lấy component Thongtin từ chính GameObject này
        playerStats = GetComponent<Thongtin>();
        if (playerStats == null)
        {
            Debug.LogError("Thongtin component not found on the same GameObject as BarUI.");
        }

        // Lưu chiều rộng gốc của thanh máu và mana
        if (healthBar != null)
        {
            healthBarOriginalWidth = healthBar.sizeDelta.x;
        }
        if (manaBar != null)
        {
            manaBarOriginalWidth = manaBar.sizeDelta.x;
        }
    }

    void Update()
    {
        if (playerStats != null)
        {
            UpdateHealthBar();
            UpdateManaBar();
        }
    }

    void UpdateHealthBar()
    {
        if (healthBar != null)
        {
            // Tính toán chiều rộng mới dựa trên tỷ lệ máu hiện tại
            Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
            newWidth = (thongtin.currentHealth / thongtin.maxHealth) * healthBarOriginalWidth;
            healthBar.sizeDelta = new Vector2(newWidth, healthBar.sizeDelta.y);
        }
    }

    void UpdateManaBar()
    {
        if (manaBar != null)
        {
            newWidth = (playerStats.currentMana / playerStats.maxMana) * manaBarOriginalWidth;
            manaBar.sizeDelta = new Vector2(newWidth, manaBar.sizeDelta.y);
        }
    }
}
