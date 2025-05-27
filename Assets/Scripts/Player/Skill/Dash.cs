using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private float dashRadius = 5f; 
    [SerializeField] private float dashCooldown = 2f; 
    [SerializeField] public float manaCost = 10f;
    [SerializeField] public int levelSkillTeleport = 1; 
    private float lastDashTime;
    private Thongtin thongtin;
    private Rigidbody2D rb;
    private Animator animator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lastDashTime = -dashCooldown; 
        thongtin = GetComponent<Thongtin>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Q) && Time.time >= lastDashTime + dashCooldown && thongtin.currentMana >= manaCost)
        {
            animator.SetTrigger("Teleport"); 
        }
    }

    private void Teleprot()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0; 

            Vector3 direction = mousePosition - transform.position;
            float distance = direction.magnitude;

            if (distance <= dashRadius)
            {
                DashToPosition(mousePosition);
            }
            else
            {
                Vector3 targetPosition = transform.position + direction.normalized * dashRadius;
                DashToPosition(targetPosition);
            }

            // Trừ mana và đặt thời gian hồi chiêu
            thongtin.currentMana -= manaCost;
            lastDashTime = Time.time;
    }

    void DashToPosition(Vector3 targetPosition)
    {
        // Dịch chuyển nhân vật đến vị trí mục tiêu
        rb.MovePosition(targetPosition);
    }
}
