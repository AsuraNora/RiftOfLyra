using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerSystem : MonoBehaviour
{
    public GameObject canvasButtonMenu;
    public GameObject canvasMenu;
    public GameObject canvasStatus;
    public GameObject canvasBag;
    [SerializeField] private GameObject canvasWizardSkill;
    [SerializeField] private GameObject canvasWarriorSkill;
    [SerializeField] private GameObject canvasRank;
    [SerializeField] public GameObject canvasInteract;

    private Vector3 hidePosition = new Vector3(9999f, 9999f, 9999f);
    private float originalSpeed;


    void Start()
    {
        Screen.SetResolution(1280, 720, false);
        // Bật/tắt các canvas cơ bản
        if (canvasButtonMenu != null) canvasButtonMenu.SetActive(true);
        if (canvasMenu != null) canvasMenu.transform.position = hidePosition;
        if (canvasStatus != null) canvasStatus.transform.position = hidePosition;
        if (canvasBag != null) canvasBag.transform.position = hidePosition;
        if (canvasRank != null) canvasRank.transform.position = hidePosition;
        if (canvasInteract != null) canvasInteract.transform.position = hidePosition;

        // Kiểm tra class của nhân vật và bật đúng canvas kỹ năng
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        string characterClass = PlayerPrefs.GetString(loggedInUser + "_CharacterClass", "Warrior");

        if (characterClass == "Wizard")
        {
            if (canvasWizardSkill != null)
            {
                canvasWizardSkill.transform.position = hidePosition;
            }
            if (canvasWarriorSkill != null)
            {
                canvasWarriorSkill.SetActive(false);
            }
        }
        else if (characterClass == "Warrior")
        {
            if (canvasWarriorSkill != null)
            {
                canvasWarriorSkill.transform.position = hidePosition;
            }
            if (canvasWizardSkill != null)
            {
                canvasWizardSkill.SetActive(false);
            }
        }
        else
        {
            Debug.LogWarning("Unknown character class: " + characterClass);
        }

    }

    public void OnButtonMenuClick()
    {
        if (canvasMenu != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
            string characterClass = PlayerPrefs.GetString(loggedInUser + "_CharacterClass", "Warrior");


            if (characterClass == "Wizard")
            {
                WizardMovement wizardMovement = player.GetComponent<WizardMovement>();
                wizardMovement.moveSpeed = 0;
            }
            else if (characterClass == "Warrior" )
            {
                Dichuyển move = player.GetComponent<Dichuyển>();
                move.moveSpeed = 0;
            }
            canvasMenu.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z);
            canvasButtonMenu.SetActive(false);
        }
    }

    public void OnButtonStatusClick()
    {
        Debug.Log("Status button clicked!");
        if (canvasStatus != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasStatus.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        }
    }

    public void OnButtonSkillClick()
    {
        string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
        string characterClass = PlayerPrefs.GetString(loggedInUser + "_CharacterClass", "Warrior");


        if (characterClass == "Wizard" && canvasWizardSkill != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasWizardSkill.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        }
        else if (characterClass == "Warrior" && canvasWarriorSkill != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasWarriorSkill.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        }
    }

    public void OnButtonBagClick()
    {
        if (canvasBag != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasBag.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        }
    }

    public void OnButtonInteractClick()
    {
        if (canvasInteract != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasInteract.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
            Interact interact = GameObject.FindObjectOfType<Interact>();
            interact.showTaskDetails();
        }
    }

    public void OnButtonQuitClick()
    {
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Destroy(player);
        }
        SceneManager.LoadScene("LogIn");
    }

    public void OnButtonRankClick()
    {
        if (canvasRank != null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            canvasMenu.transform.position = hidePosition;
            canvasRank.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 1, player.transform.position.z);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            GameObject player = GameObject.FindWithTag("Player");
            string loggedInUser = PlayerPrefs.GetString("LoggedInUser");
            string characterClass = PlayerPrefs.GetString(loggedInUser + "_CharacterClass", "Warrior");


            if (characterClass == "Wizard")
            {
                WizardMovement wizardMovement = player.GetComponent<WizardMovement>();
                wizardMovement.moveSpeed = 5f;
            }
            else if (characterClass == "Warrior")
            {
                Dichuyển move = player.GetComponent<Dichuyển>();
                move.moveSpeed = 5f;
            }

            if (canvasMenu != null) canvasMenu.transform.position = hidePosition;
            if (canvasStatus != null) canvasStatus.transform.position = hidePosition;
            if (canvasButtonMenu != null) canvasButtonMenu.SetActive(true);
            if (canvasBag != null) canvasBag.transform.position = hidePosition;
            if (canvasWizardSkill != null) canvasWizardSkill.transform.position = hidePosition;
            if (canvasWarriorSkill != null) canvasWarriorSkill.transform.position = hidePosition;
            if (canvasRank != null) canvasRank.transform.position = hidePosition;
            if (canvasInteract != null) canvasInteract.transform.position = hidePosition;
        }
    }
}
