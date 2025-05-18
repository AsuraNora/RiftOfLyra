using Inventory;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    
    enum ButtonType
    {
        State,
        Quit,
        Skill,
        Bag,
        RiseHP,
        RiseMP,
        RiseATK,
        RiseLevelAttackWizard,
        RiseLevelPoisonSlash,
        RiseLevelTeleport,
        RiseLevelAttackWarrior,
        RiseLevelFireCircle,
        RiseLevelSpeed,
    }

    [SerializeField] private ButtonType buttonType;

    private void OnMouseDown()
    {
        if (buttonType == ButtonType.State)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonStatusClick();
        }

        else if (buttonType == ButtonType.Quit)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonQuitClick();
        }

        else if (buttonType == ButtonType.Skill)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonSkillClick();
        }

        else if (buttonType == ButtonType.RiseHP)
        {
            StatusUI statusUI = FindObjectOfType<StatusUI>();
            statusUI.OnButtonHealthBonusClick();
        }
        else if (buttonType == ButtonType.RiseMP)
        {
            StatusUI statusUI = FindObjectOfType<StatusUI>();
            statusUI.OnButtonManaBonusClick();
        }
        else if (buttonType == ButtonType.RiseATK)
        {
            StatusUI statusUI = FindObjectOfType<StatusUI>();
            statusUI.OnButtonAttackBonusClick();
        }

        else if (buttonType == ButtonType.RiseLevelAttackWizard)
        {
            WizardSkillsController wizardSkillsController = FindObjectOfType<WizardSkillsController>();
            wizardSkillsController.OnButtonUpgradeNormalAttack();
        }

        else if (buttonType == ButtonType.RiseLevelPoisonSlash)
        {
            WizardSkillsController wizardSkillsController = FindObjectOfType<WizardSkillsController>();
            wizardSkillsController.OnButtonUpgreadePoisionSlash();
        }

        else if (buttonType == ButtonType.RiseLevelTeleport)
        {
            WizardSkillsController wizardSkillsController = FindObjectOfType<WizardSkillsController>();
            wizardSkillsController.OnButtonUpgradeSkillTeleport();
        }
        else if (buttonType == ButtonType.RiseLevelAttackWarrior)
        {
            WarriorSkillsController warriorSkillsController = FindObjectOfType<WarriorSkillsController>();
            warriorSkillsController.OnButtonUpgradeNormalAttack();
        }

        else if (buttonType == ButtonType.RiseLevelFireCircle)
        {
            WarriorSkillsController warriorSkillsController = FindObjectOfType<WarriorSkillsController>();
            warriorSkillsController.OnButtonUpgradeSkillFireCircle();
        }

        else if (buttonType == ButtonType.RiseLevelSpeed)
        {
            WarriorSkillsController warriorSkillsController = FindObjectOfType<WarriorSkillsController>();
            warriorSkillsController.OnButtonUpgradeSkillRiseSpeed();
        }

        else if (buttonType == ButtonType.Bag)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonBagClick();
        }
    }
}
