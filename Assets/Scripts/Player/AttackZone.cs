using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private Thongtin thôngtin; // Tham chiếu đến PlayerStats

    void Start()
    {
        thôngtin = GetComponentInParent<Thongtin>(); // Lấy tham chiếu đến PlayerStats từ đối tượng cha
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            float damage = thôngtin.attackDamage;
            collision.GetComponent<EnemyAI>().TakeDamage(damage);
            Debug.Log("Gây sát thương: " + damage);
        }
    }
}
