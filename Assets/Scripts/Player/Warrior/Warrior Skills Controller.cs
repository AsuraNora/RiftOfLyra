using UnityEngine;

public class WarriorSkillsController : MonoBehaviour
{
    [SerializeField] private TextMesh textLevelSkillAttackWarrior;
    [SerializeField] private TextMesh textLevelSkillRiseSpeed;
    [SerializeField] private TextMesh textLevelSkillFireCircle;
    [SerializeField] private TextMesh textSkillPoints;

    void Update()
    {
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (thongtin != null)
        {
            textSkillPoints.text = "KỸ NĂNG: " + thongtin.upgradeSkillPoint;
        }
        else
        {
            Debug.LogError("Thongtin component not found on Player!");
        }

        showLevelSkillAttackWarrior();
        showLevelSkillRiseSpeed();
        showLevelSkillFireCircle();
    }

    // Skill Normal Attack
    private void showLevelSkillAttackWarrior()
    {
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (thongtin != null)
        {
            AttackNormal attackNormal = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackNormal>();
            textLevelSkillAttackWarrior.text = "Level: " + attackNormal.levelSkillAttackWarrior;
        }
    }

    public void OnButtonUpgradeNormalAttack()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            AttackNormal attackNormal = player.GetComponent<AttackNormal>();
            if (thongtin != null && attackNormal != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    attackNormal.levelSkillAttackWarrior++;
                    attackNormal.attackDamage += 5; // Increase damage by 5
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin or AttackNormal component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    // Skill Rise Speed
    private void showLevelSkillRiseSpeed()
    {
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (thongtin != null)
        {
            SkillRiseMoveSpeed skillRiseMoveSpeed = GameObject.FindGameObjectWithTag("Player").GetComponent<SkillRiseMoveSpeed>();
            textLevelSkillRiseSpeed.text = "Level: " + skillRiseMoveSpeed.levelSkillRiseSpeed;
        }
    }

    public void OnButtonUpgradeSkillRiseSpeed()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            SkillRiseMoveSpeed skillRiseMoveSpeed = player.GetComponent<SkillRiseMoveSpeed>();
            if (thongtin != null && skillRiseMoveSpeed != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    skillRiseMoveSpeed.levelSkillRiseSpeed++;
                    skillRiseMoveSpeed.manaCost -= 5f; 
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin or SkillRiseMoveSpeed component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    // Skill Fire Circle
    private void showLevelSkillFireCircle()
    {
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (thongtin != null)
        {
            FireCircleController skillFireCircle = GameObject.FindGameObjectWithTag("Player").GetComponent<FireCircleController>();
            textLevelSkillFireCircle.text = "Level: " + skillFireCircle.textLevelSkillFireCircle;
        }
    }

    public void OnButtonUpgradeSkillFireCircle()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            FireCircleController skillFireCircle = player.GetComponent<FireCircleController>();
            if (thongtin != null && skillFireCircle != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    skillFireCircle.textLevelSkillFireCircle++;
                    skillFireCircle.fireBombDame += 5; 
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin or SkillPoisionSlash component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }


}
