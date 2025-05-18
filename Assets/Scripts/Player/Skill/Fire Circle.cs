using UnityEngine;

public class FireCircle : MonoBehaviour
{
    public float damage = 50f; // Lượng sát thương gây ra
    public float speed = 10f; // Tốc độ bay của kỹ năng
    public float maxDistance = 15f; // Khoảng cách bay tối đa

    private Vector3 startPosition; // Vị trí bắt đầu của kỹ năng
    private Vector3 direction; // Hướng bay của kỹ năng
    private Transform playerTransform; // Tham chiếu đến vị trí người chơi
    private bool returning = false; // Trạng thái quay lại người chơi

    void Start()
    {
        startPosition = transform.position; // Lưu vị trí bắt đầu
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform; // Lấy vị trí người chơi

        // Tính toán hướng từ người chơi đến con trỏ chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f; // Đặt z = 0 vì đây là game 2D
        direction = (mousePosition - transform.position).normalized; // Hướng bay
    }

    void Update()
    {
        if (!returning)
        {
            // Kỹ năng bay theo hướng con trỏ chuột
            transform.Translate(direction * speed * Time.deltaTime, Space.World);

            // Kiểm tra nếu vượt quá khoảng cách bay tối đa
            if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
            {
                returning = true; // Kích hoạt trạng thái quay lại
            }
        }
        else
        {
            // Kỹ năng quay lại người chơi
            Vector3 directionToPlayer = (playerTransform.position - transform.position).normalized;
            transform.Translate(directionToPlayer * speed * Time.deltaTime, Space.World);

            // Kiểm tra nếu kỹ năng đã quay lại gần người chơi
            if (Vector3.Distance(transform.position, playerTransform.position) < 0.5f)
            {
                Destroy(gameObject); // Hủy kỹ năng
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kiểm tra va chạm với kẻ địch
        if (collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            BossAI boss = collision.GetComponent<BossAI>();
            if (boss != null)
            {
                boss.TakeDamage(damage);
            }
            if (enemy != null )
            {
                enemy.TakeDamage(damage); 
            }
        }
    }
}
