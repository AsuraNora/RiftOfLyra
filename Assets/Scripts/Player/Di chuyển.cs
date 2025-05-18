using UnityEngine;

public class Dichuyển : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f; // Chỉnh thành float để di chuyển mượt hơn
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private AttackNormal attackNormal;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        attackNormal = GetComponent<AttackNormal>(); // Lấy tham chiếu đến AttackNormal
    }

    void Update()
    {
        Move();
    }

    public void Move()
    {
        Vector2 playerInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rb.linearVelocity = playerInput.normalized * moveSpeed; // Sử dụng velocity thay vì linearVelocity

        // Quay mặt nhân vật theo hướng di chuyển
        if (playerInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (playerInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Cập nhật các tham số của Animator
        animator.SetFloat("Speed", playerInput.sqrMagnitude);
       
        // Cập nhật hướng của AttackZone
        if (playerInput.x != 0)
        {
            attackNormal.UpdateAttackZoneDirection(playerInput.x);
        }
    }

    public void StartMoving()
    {
        animator.SetBool("isMoving", true);
    }

    public void StopMoving()
    {
        animator.SetBool("isMoving", false);
        rb.linearVelocity = Vector2.zero;
    }

    public void MoveTowards(Vector2 targetPosition)
    {
        Vector2 direction = (targetPosition - rb.position).normalized;
        rb.linearVelocity = direction * moveSpeed;

        // Quay mặt nhân vật theo hướng di chuyển
        if (direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        // Cập nhật các tham số của Animator
        animator.SetFloat("Speed", direction.sqrMagnitude);
    }
}
