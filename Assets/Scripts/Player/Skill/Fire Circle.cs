using UnityEngine;

public class FireCircle : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 15f;

    private Vector3 startPosition;
    private Vector3 direction;
    private Transform playerTransform;
    private bool returning = false;

    void Start()
    {
        startPosition = transform.position;
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Tính toán hướng từ người chơi đến con trỏ chuột
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0f;
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Thongtin thongtin = player.GetComponent<Thongtin>();
            FireCircleController fireCircleController = player.GetComponent<FireCircleController>();
            if (boss != null)
            {
                boss.TakeDamage(thongtin.attackDamage + fireCircleController.fireBombDame);
            }
            if (enemy != null)
            {
                enemy.TakeDamage(thongtin.attackDamage + fireCircleController.fireBombDame);
            }
        }
    }

}
