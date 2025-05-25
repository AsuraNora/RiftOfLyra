using UnityEngine;

public class WarriorSkillsController : MonoBehaviour
{
    [Header("Normal Attack Skill")]
    [SerializeField] private TextMesh textLevelSkillAttackWarrior;
    [SerializeField] private TextMesh textDameSkillNormalAttack;
    [Header("Rise Speed Skill")]
    [SerializeField] private TextMesh textLevelSkillRiseSpeed;
    [Header("Fire Circle Skill")]
    [SerializeField] private TextMesh textLevelSkillFireCircle;
    [SerializeField] private TextMesh textDameSkillFireCircle;
    [Header("Fire Meteorite Skill")]
    [SerializeField] private TextMesh textLevelSkillFireMeteorite;
    [SerializeField] private TextMesh textDameSkillFireMeteorite;
    [Header("Skill Points")]
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
        ShowLevelSkillFireMeteorite();
    }

    // Skill Normal Attack
    private void showLevelSkillAttackWarrior()
    {
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        if (thongtin != null)
        {
            AttackNormal attackNormal = GameObject.FindGameObjectWithTag("Player").GetComponent<AttackNormal>();
            textLevelSkillAttackWarrior.text = "Level: " + attackNormal.levelSkillAttackWarrior;
            textDameSkillNormalAttack.text = "ATK: " + attackNormal.attackDamage;
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
                    attackNormal.attackDamage += 5; 
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
            textDameSkillFireCircle.text = "ATK: " + skillFireCircle.fireBombDame;
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

    // Fire Meteorite
    public void OnButtonUpgradeSkillFireMeteorite()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            WarriorFireMeteorite warriorFireMeteorite = player.GetComponent<WarriorFireMeteorite>();
            if (thongtin != null && warriorFireMeteorite != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    warriorFireMeteorite.fireMeteoriteLevel++;
                    warriorFireMeteorite.fireMeteoriteDame += 5;
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin or FireMeteoriteController component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void ShowLevelSkillFireMeteorite()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            WarriorFireMeteorite fireMeteoriteController = player.GetComponent<WarriorFireMeteorite>();
            if (fireMeteoriteController != null)
            {
                textLevelSkillFireCircle.text = "Level: " + fireMeteoriteController.fireMeteoriteLevel;
                textDameSkillFireMeteorite.text = "ATK: " + fireMeteoriteController.fireMeteoriteDame;
            }
            else
            {
                Debug.LogError("FireMeteoriteController component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }


}
