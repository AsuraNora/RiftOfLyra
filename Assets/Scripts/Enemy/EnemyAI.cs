using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class EnemyAI : MonoBehaviour
{
    public enum EnemyType
    {
        SlimeLv1,
        GreenSlimeLv3,
        OrcLv7,
        SkeletonLv5,
        CrabbyLv9,
        PinkStarLv11,
        FierceToothLv13,
        GolemRockLv10,
        FlyingDemonLv17,
        MinoTaurLv15,
        GhostLv19,
    }

    public EnemyType enemyType;
    public float enemyHealth = 100f;
    public float enemyDame = 10f;
    public float moveSpeed = 2f;
    private Rigidbody2D rb;
    public float respawnTime = 3f;
    public int expReward = 50;
    [SerializeField] private Transform CurrentHPBar;
    private Vector3 initialScale;
    private Vector3 initialPosition;
    [SerializeField] TextMesh textDeclineHP;
    [Header("Attack Settings")]
    [SerializeField] private int attackCount = 1;
    [SerializeField] private GameObject arrowPrefab;
    [SerializeField] private GameObject rockKnifePrefab;
    [SerializeField] private GameObject spawmRockKnifePrefab;

    private GameManager gameManager;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    public float currentHealth;

    [SerializeField] private bool isCharging = false;
    [SerializeField] private bool isAttacking = false;
    private Transform playerTransform;

    [Header("Drop Settings")]
    public List<DropItem> dropItems = new List<DropItem>();
    public float dropRadius = 2f;

    private Coroutine moveCoroutine;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gameManager = FindObjectOfType<GameManager>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentHealth = enemyHealth;
        UpdateHPBar();
        textDeclineHP.text = string.Empty;
    }

    void Update() 
    {
        
     }

    private void UpdateHPBar( )
    {
        float HPPercentage = currentHealth / enemyHealth;

        CurrentHPBar.localScale = new Vector3(HPPercentage, 0.2f, 1f);

    }

    protected void OnEnable()
    {
        currentHealth = enemyHealth;
        UpdateHPBar();

        if (rb == null) rb = GetComponent<Rigidbody2D>();
        if (animator == null) animator = GetComponent<Animator>();
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();

        animator.ResetTrigger("Die");
        animator.Play("Idle");

        moveCoroutine = StartCoroutine(MoveRandomly());
    }

    IEnumerator MoveRandomly()
    {
        while (gameObject.activeSelf && !isCharging)
        {
            Vector2 randomDirection = Random.insideUnitCircle.normalized;
            rb.linearVelocity = randomDirection * moveSpeed;
            UpdateSpriteDirection(randomDirection);
            yield return new WaitForSeconds(1f);
        }
    }

    private IEnumerator ClearDameText()
    {
        yield return new WaitForSeconds(0.5f);
        textDeclineHP.text = string.Empty;
    }

    public void TakeDamage(float damage)
    {
        currentHealth -= damage;
        textDeclineHP.text = "- " + damage.ToString();
        UpdateHPBar();
        Debug.Log($"Quái vật mất {damage} máu, còn lại: {currentHealth}");
        StartCoroutine(ChangeColorOnDamage());
        StartCoroutine(ClearDameText());

        if (currentHealth > 0 && arrowPrefab != null && enemyType != EnemyType.GolemRockLv10)
        {
            Debug.Log("Quái vật bắn tên");
            AttackPlayer();
        }
        else if (currentHealth > 0 && arrowPrefab == null && enemyType != EnemyType.GolemRockLv10)
        {
            Debug.Log("Quái vật tấn công người chơi");
            AttackPlayerV2();
        }
        else if (currentHealth > 0 && rockKnifePrefab != null && attackCount <= 3 && enemyType == EnemyType.GolemRockLv10)
        {
            attackCount++;
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Vector2 direction = (player.transform.position - spawmRockKnifePrefab.transform.position).normalized;
            UpdateSpriteDirection(direction);
            animator.SetTrigger("NormalAttack");
        }
        else if (currentHealth > 0 && rockKnifePrefab != null && attackCount > 3 && enemyType == EnemyType.GolemRockLv10)
        {
            attackCount = 1;
            animator.SetTrigger("LazerAttack");
        }

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            UpdateHPBar();
            Die();
        }
    }

    void Die()
    {
        animator.SetTrigger("Die");
        StopCoroutine(moveCoroutine);
        rb.linearVelocity = Vector2.zero;

        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin playerThongTin = player.GetComponent<Thongtin>();
            if (playerThongTin != null)
                playerThongTin.GainExp(expReward);

            PlayerQuest playerQuest = player.GetComponent<PlayerQuest>();
            if (playerQuest != null)
                playerQuest.EnemyKilled(enemyType);
        }

        DropItems();
        StartCoroutine(DisableAfterAnimation());
    }

    void DropItems()
    {
        foreach (DropItem dropItem in dropItems)
        {
            if (Random.value <= dropItem.dropChance)
            {
                Vector2 randomOffset = Random.insideUnitCircle * dropRadius;
                Vector3 dropPosition = transform.position + new Vector3(randomOffset.x, randomOffset.y, 0);
                Instantiate(dropItem.itemPrefab, dropPosition, Quaternion.identity);
            }
        }
    }

    IEnumerator DisableAfterAnimation()
    {
        yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        gameObject.SetActive(false);
        if (gameManager != null)
        {
            gameManager.RespawnEnemy(gameObject, respawnTime);
        }
    }

    private void AttackPlayer()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || arrowPrefab == null) return;

        Vector2 direction = (player.transform.position - transform.position).normalized;
        GameObject arrow = Instantiate(arrowPrefab, transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        arrow.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rbArrow = arrow.GetComponent<Rigidbody2D>();
        if (rbArrow != null)
        {
            rbArrow.linearVelocity = direction * 10f;
        }
    }

    private void AttackPlayerV2()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) return;

        playerTransform = player.transform;
        if (!isCharging)
        {
            isCharging = true;
            if (moveCoroutine != null)
                StopCoroutine(moveCoroutine);
            StartCoroutine(ChargeToPlayer());
        }
    }

    public void AttackPlayerV3()
    {
        StopCoroutine(moveCoroutine);
        animator.SetTrigger("Idle");
        Debug.Log("Quái vật ném dao đá in");
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null || rockKnifePrefab == null) return;

        Vector2 direction = (player.transform.position - spawmRockKnifePrefab.transform.position).normalized;
        GameObject rockKnife = Instantiate(rockKnifePrefab, spawmRockKnifePrefab.transform.position, Quaternion.identity);

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rockKnife.transform.rotation = Quaternion.Euler(0, 0, angle);

        Rigidbody2D rbRockKnife = rockKnife.GetComponent<Rigidbody2D>();
        if (rockKnife != null)
        {
            rbRockKnife.linearVelocity = direction * 10f;
        }
        StartCoroutine(MoveRandomly());
    }

    // Đuổi theo người chơi để tấn công
    private IEnumerator ChargeToPlayer()
    {
        float originalSpeed = moveSpeed;
        moveSpeed *= 2;

        while (isCharging && !isAttacking)
        {
            if (playerTransform == null) break;

            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.linearVelocity = direction * moveSpeed;
            UpdateSpriteDirection(direction);

            yield return null;
        }

        moveSpeed = originalSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && isCharging && !isAttacking)
        {
            isAttacking = true;
            rb.linearVelocity = Vector2.zero;
            animator.SetTrigger("EnemyAttack");

            Thongtin playerThongTin = collision.gameObject.GetComponent<Thongtin>();
            if (playerThongTin != null)
            {
                playerThongTin.TakeDamage(enemyDame);
            }
        }
    }

    public void EndChargeFromAnim()
    {
        isCharging = false;
        isAttacking = false;
        moveCoroutine = StartCoroutine(MoveRandomly());
    }

    private IEnumerator ChangeColorOnDamage()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr != null)
        {
            sr.color = Color.red;
            yield return new WaitForSeconds(0.2f);
            sr.color = Color.white;
        }
    }

    private void UpdateSpriteDirection(Vector2 moveDirection)
    {
        if (spriteRenderer == null) return;

        if (moveDirection.x > 0.01f)
            spriteRenderer.flipX = false; // Quay phải
        else if (moveDirection.x < -0.01f)
            spriteRenderer.flipX = true; // Quay trái
    }
}

[System.Serializable]
public class DropItem
{
    public GameObject itemPrefab;
    [Range(0f, 1f)] public float dropChance;
}
