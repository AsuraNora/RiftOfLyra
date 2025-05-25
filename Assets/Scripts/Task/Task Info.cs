using UnityEngine;

public class TaskInfo : MonoBehaviour
{
    [SerializeField] private TextMesh textTaskName;
    [SerializeField] private TextMesh textTaskDescription;

    void Update()
    {
        showTaskInfo();
    }
    private void showTaskInfo()
    {
        TaskManager taskManager = FindObjectOfType<TaskManager>();
        int currentTaskIndex = taskManager.currentTaskIndex;

        var task = taskManager.taskList[currentTaskIndex];

        if (task.taskStatus == TaskStatus.NotAccepted)
        {
            textTaskName.text = "Bạn chưa nhận nhiệm vụ";
            textTaskDescription.text = "Hãy đến gặp Trưởng Làng để nhận nhiệm vụ mới.";
        }
        else if (task.taskStatus == TaskStatus.InProgress)
        {
            textTaskName.text = task.taskName;
            textTaskDescription.text =
                "Tiến độ nhiệm vụ: Đã tiêu diệt " + task.taskQuantityCurrent + " / " + task.taskQuantityRequest + " quái vật \n" +
                "Phần thưởng: " + task.taskExpReward + " EXP \n";
        }
        else if (task.taskStatus == TaskStatus.Completed)
        {
            textTaskName.text = task.taskName;
            textTaskDescription.text =
                "Bạn đã hoàn thành nhiệm vụ này!\n" +
                "Hãy quay lại găn Trưởng Làng để nhận phần thưởng.\n";
        }
    }
}
