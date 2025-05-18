using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float damage;

    void Start()
    {
        Destroy(gameObject, 1.5f); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player"); 
            Thongtin playerThongTin = player.GetComponent<Thongtin>();
            if(playerThongTin != null )
            {
                playerThongTin.TakeDamage(damage);
            }
            Destroy(gameObject); 
        }      
    }
}
