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
        Rank,
        Task,
        AcceptAsk,
        Escape,
        UpgradePoisionNeedle
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

        else if (buttonType == ButtonType.Rank)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonRankClick();
        }

        else if (buttonType == ButtonType.Task)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonTaskInfoClick();
        }

        else if (buttonType == ButtonType.AcceptAsk)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            TaskManager taskManager = FindObjectOfType<TaskManager>();
            int currentTaskIndex = taskManager.currentTaskIndex;
            if (player == null)
            {
                Debug.LogError("Không tìm thấy đối tượng Player trong scene.");
                return;
            }
            else
            {
                if (taskManager.GetTaskList()[currentTaskIndex].taskStatus == TaskStatus.NotAccepted)
                {
                    PlayerTaskController playerTaskController = player.GetComponent<PlayerTaskController>();
                    playerTaskController.AcceptTaskClick();
                }
                else if (taskManager.GetTaskList()[currentTaskIndex].taskStatus == TaskStatus.InProgress)
                {
                    PlayerTaskController playerTaskController = player.GetComponent<PlayerTaskController>();
                    playerTaskController.CancelTaskClick();
                }
                else if (taskManager.GetTaskList()[currentTaskIndex].taskStatus == TaskStatus.Completed)
                {
                    PlayerTaskController playerTaskController = player.GetComponent<PlayerTaskController>();
                    playerTaskController.CompleteTaskClick();
                }
            }

            gameManagerSystem.canvasInteract.transform.position = new Vector3(9999f, 9999f, 9999f);

        }

        else if (buttonType == ButtonType.Escape)
        {
            GameManagerSystem gameManagerSystem = FindObjectOfType<GameManagerSystem>();
            gameManagerSystem.OnButtonEscapeClick();
        }

        else if (buttonType == ButtonType.UpgradePoisionNeedle)
        {
            WizardSkillsController wizardSkillsController = FindObjectOfType<WizardSkillsController>();
            wizardSkillsController.OnUpgradePoisionNeedle();
            
        }

    }
}
