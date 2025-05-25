using UnityEngine;

public class WarriorFireMeteorite : MonoBehaviour
{
    [SerializeField] public int fireMeteoriteLevel = 1;
    [SerializeField] private float fireMeteoriteRange = 5f;
    [SerializeField] public float fireMeteoriteDame = 10f;
    [SerializeField] private float meteoriteCoolDown = 5f;
    [SerializeField] private float meteoriteManaCost = 10f;
    [SerializeField] private GameObject meteoriteEffectPrefab;
    [SerializeField] private float meteoriteSpeed = 15f;
    [SerializeField] private float meteoriteSpawnHeight = 8f; // Có thể chỉnh trên Inspector
    private float lastCastTime = -999f;

    void Start()
    {
        // Load saved level and damage values
        LoadFireMeteoriteLevel();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (Time.time - lastCastTime < meteoriteCoolDown) return;

            // Lấy vị trí chuột trên thế giới
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            // Vị trí spawn: trên đầu nhân vật một khoảng meteoriteSpawnHeight
            Vector3 spawnPos = transform.position + Vector3.up * meteoriteSpawnHeight;

            // Tạo thiên thạch và bắt đầu di chuyển
            GameObject meteorite = Instantiate(meteoriteEffectPrefab, spawnPos, Quaternion.identity);

            // FlipX nếu chuột bên trái nhân vật
            if (meteorite.TryGetComponent<SpriteRenderer>(out var sr))
            {
                sr.flipX = mouseWorldPos.x < transform.position.x;
            }

            StartCoroutine(MoveMeteorite(meteorite, mouseWorldPos));

            lastCastTime = Time.time;
        }
    }

    private System.Collections.IEnumerator MoveMeteorite(GameObject meteorite, Vector3 targetPos)
    {
        while (meteorite != null && Vector3.Distance(meteorite.transform.position, targetPos) > 0.1f)
        {
            meteorite.transform.position = Vector3.MoveTowards(
                meteorite.transform.position,
                targetPos,
                meteoriteSpeed * Time.deltaTime
            );
            yield return null;
        }

        // Nổ tại vị trí targetPos
        if (meteorite != null)
        {
            // Gây sát thương vùng
            Collider2D[] hits = Physics2D.OverlapCircleAll(targetPos, fireMeteoriteRange);
            foreach (var hit in hits)
            {
                if (hit.CompareTag("Enemy"))
                {
                    EnemyAI enemy = hit.GetComponent<EnemyAI>();
                    if (enemy != null)
                        enemy.TakeDamage(fireMeteoriteDame);
                }
            }
            Destroy(meteorite);
        }
    }

    // Save và load level skill
    public void SaveFireMeteoriteLevel()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        PlayerPrefs.SetInt(loggedInUser + "_FireMeteoriteLevel", fireMeteoriteLevel);
        PlayerPrefs.SetFloat(loggedInUser + "_FireMeteoriteDame", fireMeteoriteDame);
        PlayerPrefs.Save();
    }

    public void LoadFireMeteoriteLevel()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        fireMeteoriteLevel = PlayerPrefs.GetInt(loggedInUser + "_FireMeteoriteLevel", fireMeteoriteLevel);
        fireMeteoriteDame = PlayerPrefs.GetFloat(loggedInUser + "_FireMeteoriteDame", fireMeteoriteDame);
    }

}

