using UnityEngine;

public class PlayerTaskController : MonoBehaviour
{

    void Start()
    {

    }

    public void AcceptTaskClick()
    {
        GameObject gameManger = GameObject.FindGameObjectWithTag("Game Manager");
        TaskManager taskManager = gameManger.GetComponent<TaskManager>();
        if (taskManager != null)
        {
            taskManager.AcceptTask();
            Debug.Log("Đã gọi AcceptTaskClick ");
        }
        else
        {
            Debug.LogError("Không tìm thấy TaskManager trong scene.");
        }
    }

    public void CancelTaskClick()
    {
        GameObject gameManger = GameObject.FindGameObjectWithTag("Game Manager");
        TaskManager taskManager = gameManger.GetComponent<TaskManager>();
        if (taskManager != null)
        {
            taskManager.CancelTask();
            Debug.Log("Đã gọi AcceptTaskClick ");
        }
        else
        {
            Debug.LogError("Không tìm thấy TaskManager trong scene.");
        }
    }

    public void CompleteTaskClick()
    {
        GameObject gameManger = GameObject.FindGameObjectWithTag("Game Manager");
        TaskManager taskManager = gameManger.GetComponent<TaskManager>();
        if (taskManager != null)
        {
            taskManager.CompleteTask();
            Debug.Log("Đã gọi AcceptTaskClick ");
        }
        else
        {
            Debug.LogError("Không tìm thấy TaskManager trong scene.");
        }
    }


}
