using UnityEngine;

public class WizardFireBomb : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            EnemyAI enemy = collision.GetComponent<EnemyAI>();
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Thongtin playerInfo = player.GetComponent<Thongtin>();
            SkillPoisionSlashController fireBombInfo = player.GetComponent<SkillPoisionSlashController>();
            if(enemy != null)
            {
                enemy.TakeDamage(playerInfo.attackDamage + fireBombInfo.fireBombDame); 
            }

        }
    }

    private void OnEventEndAniMation()
    {
        Destroy(gameObject, 0.5f); 
    }
}
