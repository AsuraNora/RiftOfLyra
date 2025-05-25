using System.Threading.Tasks;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f;
    [SerializeField] private GameObject npcThink;
    [SerializeField] private GameObject canvasCommunication;
    [SerializeField] private TextMesh textTaskName;
    [SerializeField] private TextMesh textTaskDescription;
    public bool playerInRange = false;

    void Start()
    {
        npcThink.SetActive(false);
    }

    void Update()
    {
        CheckPlayerInRange();

        if (playerInRange && Input.GetKeyDown(KeyCode.Return))
        {
            GameManagerSystem gameManager = FindObjectOfType<GameManagerSystem>();
            gameManager.OnButtonInteractClick();
        }
    }

    void CheckPlayerInRange()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, interactionRange);
        playerInRange = false;
        foreach (var col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                playerInRange = true;
                npcThink.SetActive(true);
                break;
            }
            else
            {
                npcThink.SetActive(false);
            }
        }
    }

    public void showTaskDetails()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        int currentTaskIndex = taskManager.currentTaskIndex;
    
        var task = taskManager.taskList[currentTaskIndex];

        if (task.taskStatus == TaskStatus.NotAccepted)
        {
            textTaskName.text = task.taskName;
            textTaskDescription.text = task.taskDescription;
        }
        else if (task.taskStatus == TaskStatus.InProgress)
        {
            textTaskName.text = task.taskName;
            textTaskDescription.text =
                $"Tiến độ: {task.taskQuantityCurrent}/{task.taskQuantityRequest}\n" +
                "Bạn có muốn hủy nhiệm vụ này không?";
        }
        else if (task.taskStatus == TaskStatus.Completed)
        {
            textTaskName.text = task.taskName;
            textTaskDescription.text =
                "Nhiệm vụ đã hoàn thành!\n" +
                "Bạn có muốn nhận phần thưởng không?";
        }
    }
}
