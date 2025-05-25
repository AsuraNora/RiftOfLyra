using UnityEngine;

public class AttackZone : MonoBehaviour
{
    private Thongtin thôngtin; 

    void Start()
    {
        thôngtin = GetComponentInParent<Thongtin>(); 
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
