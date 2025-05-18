using UnityEngine;
using System.Collections;

public class BossAI : MonoBehaviour
{
    [Header("Boss Information")]
    [SerializeField] private string bossName = "Golem Rock";
    [SerializeField] private float bossMaxHP = 10000f;
    [SerializeField, Tooltip("Current HP (do not modify)")] private float currentHealth;
    [SerializeField] private float bossSpeed = 5f;
    [SerializeField] public float bossAtk = 200f;

    [Header("Skill Settings")]
    [SerializeField] private int attackCount = 0;
    [SerializeField] private GameObject RockKnifePrefab;
    [SerializeField] private GameObject RockKnifeSpawnPoint;
    [SerializeField] private GameObject LazerPrefab;
    [SerializeField] private GameObject LazerSpawnPoint;

    private Animator animator;
    private Rigidbody2D rb;
    private GameObject player;

    private bool isAttacking = false;
    private bool isDead = false;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        currentHealth = bossMaxHP;
        
        rb.linearVelocity = Vector2.zero;
        animator.SetTrigger("Idle");
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        currentHealth -= damage;
        Debug.Log($"Boss bị tổn thương: {damage}, máu còn lại: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
        if(currentHealth > 0)
        {
            BossAttack(); 
        }
    }

    private void BossAttack()
    {
        if (attackCount < 3)
        {
            attackCount++;
            animator.SetTrigger("NormalAttack");
        }
        else
        {
            attackCount = 0;
            animator.SetTrigger("LazerAttack"); 
        }
    }

    public void OnPerformNormalAttack()
    {
        if (player == null || RockKnifePrefab == null || RockKnifeSpawnPoint == null)
        {
            Debug.LogWarning("Thiếu prefab hoặc spawn point.");
            isAttacking = false;
            return;
        }

        Vector2 direction = (player.transform.position - RockKnifeSpawnPoint.transform.position).normalized;
        if (direction == Vector2.zero)
        {
            Debug.LogWarning("Hướng bằng zero, không bắn.");
            isAttacking = false;
            return;
        }

        GameObject rockKnife = Instantiate(RockKnifePrefab, RockKnifeSpawnPoint.transform.position, Quaternion.identity);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rockKnife.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rbRock = rockKnife.GetComponent<Rigidbody2D>();
        if (rbRock != null)
        {
            rbRock.linearVelocity = direction * 10f; 
        }

        StartCoroutine(ResumeAttackStateAfterDelay(0.5f)); 
    }

    public void OnPerformLazerAttack()
    {
        if (LazerPrefab == null || LazerSpawnPoint == null) return;

        Instantiate(LazerPrefab, LazerSpawnPoint.transform.position, Quaternion.identity);

        StartCoroutine(ResumeAttackStateAfterDelay(1f)); // Thời gian chờ trước khi có thể tấn công lại
    }

    private IEnumerator ResumeAttackStateAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAttacking = false;
    }

    private void Die()
    {
        isDead = true;
        animator.SetTrigger("Die");
        rb.linearVelocity = Vector2.zero;
        gameObject.SetActive(false);
    }
}
