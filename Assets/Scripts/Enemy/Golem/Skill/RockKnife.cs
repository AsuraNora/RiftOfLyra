using UnityEngine;

public class RockKnife : MonoBehaviour
{
    public float damageRockKnife = 50f;
    public float maxDistance = 10f;

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        if (Vector3.Distance(startPosition, transform.position) >= maxDistance)
        {
            Destroy(gameObject); 
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Thongtin playerInfo = player.GetComponent<Thongtin>();
            EnemyAI enemyInfo = GameObject.FindGameObjectWithTag("Enemy").GetComponent<EnemyAI>();
            if (playerInfo != null)
            {
                playerInfo.TakeDamage(enemyInfo.enemyDame + damageRockKnife);
            }
            Destroy(gameObject);
        }
    }
}
