using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

public class AttackNormal : MonoBehaviour
{
    private Animator animator;
    private bool canAttack = true; 
    public float attackCooldown = 1f; 
    public GameObject attackZone; 
    private Thongtin thôngtin; 
    private Dichuyển dichuyển; 
    public float attackRange = 0.5f; 
    public float attackDamage = 5f; 
    public int levelSkillAttackWarrior = 1;

    void Start()
    {
        animator = GetComponent<Animator>(); 
        attackZone.SetActive(false); 
        thôngtin = GetComponent<Thongtin>(); 
        dichuyển = GetComponent<Dichuyển>(); 
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canAttack)
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
            attackZone.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
        else if (directionX > 0)
        {
            attackZone.transform.localRotation = Quaternion.Euler(0, 0, 0); 
        }
    }

}