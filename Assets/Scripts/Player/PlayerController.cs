using Inventory.Model;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject fireDartPrefab;
    private Thongtin thongTin;
    public float fireDartCooldown = 3f;
    private float lastFireDartTime;
    public float fireDartManaCost = 10f;
    public float fireDartSpeed = 10f;
    private bool canFireDart = false;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    [SerializeField] public float fireDartDame = 10f;
    [SerializeField] public int levelSkillFireDart = 1;

    void Start()
    {
        thongTin = GetComponent<Thongtin>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        lastFireDartTime = -fireDartCooldown;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time >= lastFireDartTime + fireDartCooldown)
        {
            if (thongTin != null && thongTin.currentMana >= fireDartManaCost)
            {
                 FlipCharacter();
                animator.SetTrigger("Attack");
            }
            else
            {
                if (thongTin == null)
                {
                    Debug.Log("Thongtin component is null");
                }
                if (thongTin != null && thongTin.currentMana < fireDartManaCost)
                {
                    Debug.Log("Not enough mana");
                }
            }
        }
    }

    public void OnAttackAnimationComplete()
    {
        if (Time.time >= lastFireDartTime + fireDartCooldown)
        {
            FireDart();
        }
        else
        {
            Debug.Log("Kỹ năng đang trong thời gian hồi chiêu");
        }
    }

    void FireDart()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0; 

        Vector3 direction = (mousePosition - transform.position).normalized;

        GameObject fireDart = Instantiate(fireDartPrefab, transform.position, Quaternion.identity);

        EnergyBall fireDartScript = fireDart.GetComponent<EnergyBall>();

        if (fireDartScript != null)
        {
            fireDartScript.speed = fireDartSpeed; 
            fireDartScript.SetDirection(direction);
            fireDartScript.SetMaxDistance(10f);
        }
        else
        {
            Debug.LogError("FireDart script not found on the prefab.");
        }

        thongTin.currentMana -= fireDartManaCost;

        lastFireDartTime = Time.time;
    }

    private void FlipCharacter()
    {
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        if (mousePosition.x < transform.position.x)
        {
            spriteRenderer.flipX = true; 
        }
        else
        {
            spriteRenderer.flipX = false; 
        }
    }
}
