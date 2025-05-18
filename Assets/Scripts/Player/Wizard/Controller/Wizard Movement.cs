using UnityEngine;

public class WizardMovement : MonoBehaviour
{
    [SerializeField] public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>(); 
    }

    void Update()
    {
        Move();
    }
    private void Move()
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

        animator.SetFloat("Speed", playerInput.sqrMagnitude);
    }

    public void StartMoving()
    {
        animator.SetBool("isMoving", true);
    }


}
