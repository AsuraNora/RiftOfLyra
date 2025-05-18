using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AttackNormal : MonoBehaviour
{
    private Animator animator;
    private bool canAttack = true; // Biến để kiểm tra xem nhân vật có thể tấn công hay không
    public float attackCooldown = 1f; // Thời gian hồi chiêu cho tấn công
    public GameObject attackZone; // Tham chiếu đến GameObject AttackZone
    private Thongtin thôngtin; // Tham chiếu đến PlayerStats
    private Dichuyển dichuyển; // Tham chiếu đến script Dichuyển
    public float attackRange = 0.5f; // Khoảng cách tấn công
    public float attackDamage = 5f; // Sát thương tấn công
    public int levelSkillAttackWarrior = 1;

    void Start()
    {
        animator = GetComponent<Animator>(); // Lấy tham chiếu đến Animator
        attackZone.SetActive(false); // Vô hiệu hóa AttackZone ban đầu
        thôngtin = GetComponent<Thongtin>(); // Lấy tham chiếu đến PlayerStats
        dichuyển = GetComponent<Dichuyển>(); // Lấy tham chiếu đến script Dichuyển
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(1) && canAttack)
        {
                Attack();
        }
            
      
    }

    void Attack()
    {
        animator.SetTrigger("Attack");
        canAttack = false; 
        StartCoroutine(ActivateAttackZone()); 
        StartCoroutine(ResetAttackCooldown()); 
    }

    IEnumerator ActivateAttackZone()
    {
        attackZone.SetActive(true); 
        yield return new WaitForSeconds(0.5f); 
        attackZone.SetActive(false); 
    }

    IEnumerator ResetAttackCooldown()
    {
        yield return new WaitForSeconds(attackCooldown); 
        canAttack = true; 
    }

    public void UpdateAttackZoneDirection(float directionX)
    {
        // Cập nhật hướng của AttackZone dựa trên hướng của nhân vật theo trục X
        if (directionX < 0)
        {
            attackZone.transform.localRotation = Quaternion.Euler(0, 180, 0); // Quay mặt sang trái
        }
        else if (directionX > 0)
        {
            attackZone.transform.localRotation = Quaternion.Euler(0, 0, 0); // Quay mặt sang phải
        }
    }

}