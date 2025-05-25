using UnityEngine;

public class WizardSkillsController : MonoBehaviour
{
    private GameObject player;
    [Header("Energy Ball")]
    [SerializeField] private TextMesh textLevelSkillFireDart;
    [SerializeField] private TextMesh textDameSkillNormalAttack;
    [Header("Teleport")]
    [SerializeField] private TextMesh textlevelSkillTeleport;
    [Header("Poision Slash")]
    [SerializeField] private TextMesh textLevelSkill;
    [SerializeField] private TextMesh textDameSkillPoisionSlash;
    [Header("Poision Needle")]
    [SerializeField] private TextMesh textLevelSkillPoisionNeedle;
    [SerializeField] private TextMesh textDameSkillPoisionNeedle;

    [SerializeField] private TextMesh textSkillPoints;

    void Start()
    {
        
    }

    void Update()
    {
        showLevelSkillPoiSionSlash();
        showLevelSkillTeleport();
        showLevelSkillFireDart();
        showLevelSkillPoisionNeedle();
        Thongtin thongtin = GameObject.FindGameObjectWithTag("Player").GetComponent<Thongtin>();
        textSkillPoints.text = "KỸ NĂNG: " + thongtin.upgradeSkillPoint.ToString();
    }
    // Skill Normal Attack
    public void OnButtonUpgradeNormalAttack()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            PlayerController fireDartInfo = player.GetComponent<PlayerController>();
            if (thongtin != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    if (fireDartInfo != null)
                    {
                        fireDartInfo.fireDartDame +=5;
                        fireDartInfo.levelSkillFireDart++;
                    }
                    else
                    {
                        Debug.LogError("WizardFireBombController not found on Player!");
                    }
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough upgrade skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    public void showLevelSkillFireDart()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController fireDartInfo = player.GetComponent<PlayerController>();
            textLevelSkill.text = "Level: " + fireDartInfo.levelSkillFireDart;
            textDameSkillNormalAttack.text = "ATL: " + fireDartInfo.fireDartDame;
        }
    }
     
    // Skill Teleport
    public void OnButtonUpgradeSkillTeleport()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            Dash teleportInfo = player.GetComponent<Dash>();
            if (thongtin != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    if (teleportInfo != null)
                    {
                        teleportInfo.manaCost -=5f;
                        teleportInfo.levelSkillTeleport++;
                    }
                    else
                    {
                        Debug.LogError("WizardFireBombController not found on Player!");
                    }
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough upgrade skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void showLevelSkillTeleport()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Dash teleportInfo = player.GetComponent<Dash>();
            textlevelSkillTeleport.text = "Level: " + teleportInfo.levelSkillTeleport;
        }
    }


    // Skill Poision Slash
    public void OnButtonUpgreadePoisionSlash()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            SkillPoisionSlashController fireBombInfo = player.GetComponent<SkillPoisionSlashController>();
            if (thongtin != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    if (fireBombInfo != null)
                    {
                        fireBombInfo.fireBombDame += 5;
                        fireBombInfo.levelSkill++;
                    }
                    else
                    {
                        Debug.LogError("WizardFireBombController not found on Player!");
                    }
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough upgrade skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void showLevelSkillPoiSionSlash()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            WizardPosisionNeedle wizardPosisionNeedle = player.GetComponent<WizardPosisionNeedle>();
            textLevelSkillPoisionNeedle.text = "Level: " + wizardPosisionNeedle.levelSkillPoisionNeedle;
            textDameSkillPoisionNeedle.text = "ATK: " + wizardPosisionNeedle.poisionNeedleDamage;
        }
    }

    // Poision Needle
    public void OnUpgradePoisionNeedle()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Thongtin thongtin = player.GetComponent<Thongtin>();
            WizardPosisionNeedle poisionNeedleInfo = player.GetComponent<WizardPosisionNeedle>();
            if (thongtin != null)
            {
                if (thongtin.upgradeSkillPoint > 0)
                {
                    thongtin.upgradeSkillPoint--;
                    if (poisionNeedleInfo != null)
                    {
                        poisionNeedleInfo.poisionNeedleDamage += 10;
                        poisionNeedleInfo.levelSkillPoisionNeedle++;
                    }
                    else
                    {
                        Debug.LogError("SkillPoisionNeedleController not found on Player!");
                    }
                    thongtin.SavePlayerData();
                }
                else
                {
                    Debug.LogWarning("Not enough upgrade skill points!");
                }
            }
            else
            {
                Debug.LogError("Thongtin component not found on Player!");
            }
        }
        else
        {
            Debug.LogError("Player not found!");
        }
    }

    private void showLevelSkillPoisionNeedle()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            WizardPosisionNeedle poisionNeedleInfo = player.GetComponent<WizardPosisionNeedle>();
            if (poisionNeedleInfo != null)
            {
                textLevelSkill.text = "Level: " + poisionNeedleInfo.levelSkillPoisionNeedle;
                textDameSkillPoisionSlash.text = "ATK: " + poisionNeedleInfo.poisionNeedleDamage;
            }
        }
    }

    // Show Tooltip
    public void OnSkillNormalAttackHover()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            PlayerController fireDartInfo = player.GetComponent<PlayerController>();
            if (fireDartInfo != null)
            {
                string skillInfo = $"Tấn công\nATK: {fireDartInfo.fireDartDame}\nLevel: {fireDartInfo.levelSkillFireDart}";
            }
        }
    }

    public void OnSkillTeleportHover()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            Dash teleportInfo = player.GetComponent<Dash>();
            if (teleportInfo != null)
            {
                string skillInfo = $"Teleport\nLevel: {teleportInfo.levelSkillTeleport}";
                Debug.LogWarning("tele");
            }
        }
    }
    public void OnSkillIconHoverEnter()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            SkillPoisionSlashController fireBombInfo = player.GetComponent<SkillPoisionSlashController>();
            if (fireBombInfo != null)
            {
                string skillInfo = $"Poision Slash\nATK: {fireBombInfo.fireBombDame}\nLevel: {fireBombInfo.levelSkill}";
            }
        }
    }
}
