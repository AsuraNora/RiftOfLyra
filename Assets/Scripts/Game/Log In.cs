using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LogIn : MonoBehaviour
{
    public TMP_InputField usernameInput;
    public TMP_InputField passwordInput;
    public TMP_InputField registerUsernameInput;
    public TMP_InputField registerPasswordInput;
    public TMP_InputField characterNameInput;

    public TextMeshProUGUI messageText;
    public TextMeshProUGUI registerMessageText;

    public GameObject RegisterCanvas;
    public GameObject LoginCanVas;
    public GameObject CharacterCreationCanvas;

    public Button warriorButton;
    public Button wizardButton;
    [SerializeField] private GameObject canvasCommunication;
    [SerializeField] private Button exit;

    private bool isWarrior;

    void Start()
    {
        Screen.SetResolution(1440, 720, false);
        RegisterCanvas.SetActive(false);
        CharacterCreationCanvas.SetActive(false);
        canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);

        if (warriorButton != null)
            warriorButton.onClick.AddListener(OnWarriorButtonClicked);
        else
            Debug.LogError("Warrior Button is not assigned!");

        if (wizardButton != null)
            wizardButton.onClick.AddListener(OnWizardButtonClicked);
        else
            Debug.LogError("Wizard Button is not assigned!");
    }

    public void OnLoginButtonClicked()
    {
        string username = usernameInput.text;
        string password = passwordInput.text;

        if (PlayerPrefs.HasKey(username + "_Password") && PlayerPrefs.GetString(username + "_Password") == password)
        {
            messageText.text = "Đăng nhập thành công!";
            canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            PlayerPrefs.SetString("LoggedInUser", username);
            PlayerPrefs.Save();

            if (PlayerPrefs.HasKey(username + "_CharacterName"))
            {
                PlayerPrefs.SetString("LoggedInCharacter", PlayerPrefs.GetString(username + "_CharacterName"));
                PlayerPrefs.Save();
                Debug.Log("Đã đăng nhập với nhân vật: " + PlayerPrefs.GetString("LoggedInCharacter"));

                SceneManager.LoadScene("Map 1");
            }
            else
            {
                canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);
                OpenCharacterCreation();
            }
        }
        else
        {
            messageText.text = "Tên đăng nhập hoặc mật khẩu không đúng!";
            canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        }
    }

    public void OpenCharacterCreation()
    {
        LoginCanVas.SetActive(false);
        RegisterCanvas.SetActive(false);
        CharacterCreationCanvas.SetActive(true);
    }

    public void OnRegisterButtonClicked()
    {
        LoginCanVas.SetActive(false);
        RegisterCanvas.SetActive(true);
    }

    public void OnRegisterSubmitButtonClicked()
    {
        string username = registerUsernameInput.text;
        string password = registerPasswordInput.text;

        if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
        {
            registerMessageText.text = "Không được để trống tên tài khoản hoặc mật khẩu!";
            canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            return;
        }

        if (PlayerPrefs.HasKey(username + "_Password"))
        {
            registerMessageText.text = "Tên tài khoản đã tồn tại!";
            canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            return;
        }

        PlayerPrefs.SetString(username + "_Password", password);
        PlayerPrefs.Save();

        registerMessageText.text = "Đăng ký tài khoản thành công";
        canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        RegisterCanvas.SetActive(false);
        LoginCanVas.SetActive(true);
    }

    public void OnWarriorButtonClicked()
    {
        isWarrior = true;
        warriorButton.GetComponent<CanvasGroup>().alpha = 1f;
        wizardButton.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    public void OnWizardButtonClicked()
    {
        isWarrior = false;
        wizardButton.GetComponent<CanvasGroup>().alpha = 1f;
        warriorButton.GetComponent<CanvasGroup>().alpha = 0.5f;
    }

    public void OnCreateCharacterButtonClicked()
    {
        string username = PlayerPrefs.GetString("LoggedInUser", "");
        string characterName = characterNameInput.text;

        if (string.IsNullOrEmpty(characterName))
        {
            registerMessageText.text = "Tên nhân vật không được để trống!";
            canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            return;
        }

        string characterClass = isWarrior ? "Warrior" : "Wizard";

        PlayerPrefs.SetString(username + "_CharacterName", characterName);
        PlayerPrefs.SetString(username + "_CharacterClass", characterClass);
        PlayerPrefs.SetString("LoggedInCharacter", characterName);
        PlayerPrefs.Save();

        Debug.Log("Đã tạo nhân vật: " + characterName + " - Class: " + characterClass);
        registerMessageText.text = "Tạo nhân vật thành công!";
        canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
        SceneManager.LoadScene("Map 1");
    }

    public void OnBackToLoginButtonClicked()
    {
        RegisterCanvas.SetActive(false);
        LoginCanVas.SetActive(true);
    }

    public void OnExitButtonClicked()
    {
        canvasCommunication.GetComponent<RectTransform>().anchoredPosition = new Vector2(2000, 0);
    }
}
