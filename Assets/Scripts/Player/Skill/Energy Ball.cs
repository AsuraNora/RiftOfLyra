using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float speed = 10f; 
    private float damage; 
    private Vector3 direction; 
    private float maxDistance; 
    private Vector3 startPosition; 

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        transform.position += direction * speed * Time.deltaTime;

        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); 
        }
    }

    
    public void SetDirection(Vector3 dir)
    {
        direction = dir.normalized;
    }

    public void SetMaxDistance(float distance)
    {
        maxDistance = distance;
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if(player != null)
            {
                Thongtin thongtin = player.GetComponent<Thongtin>();
                PlayerController fireDartInfo = player.GetComponent<PlayerController>();
                if (thongtin != null)
                {
                    damage = thongtin.attackDamage + fireDartInfo.fireDartDame; 
                }
            }
            EnemyAI enemy = other.GetComponent<EnemyAI>();
            BossAI boss = other.GetComponent<BossAI>();
            if (enemy != null)
            {
                enemy.TakeDamage(damage);
                Destroy(gameObject); 
            }
            if (boss != null)
            {
                boss.TakeDamage(damage); 
                Destroy(gameObject); 
            }
        }
    }
}
