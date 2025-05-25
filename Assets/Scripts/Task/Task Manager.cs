using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Danh sách nhiệm vụ")]
    [SerializeField] public List<TaskData> taskList = new List<TaskData>();
    public int currentTaskIndex = -1;

    // Truy cập danh sách nhiệm vụ
    public List<TaskData> GetTaskList()
    {
        return taskList;
    }

    void Start()
    {
        PickRandomTaskIndex();
    }

    void Update()
    {
        checkProgress();
    }

    // Thêm nhiệm vụ mới 
    public void AddTask(TaskData task)
    {
        taskList.Add(task);
    }

    // Cập nhật tiến độ nhiệm vụ theo tên
    public void checkProgress()
    {
        if (taskList[currentTaskIndex].taskQuantityRequest <= taskList[currentTaskIndex].taskQuantityCurrent)
        {
            taskList[currentTaskIndex].taskStatus = TaskStatus.Completed;
        }
    }

    // Hàm nhận nhiệm vụ 
    public bool AcceptTask()
    {
        if (taskList[currentTaskIndex].taskStatus != TaskStatus.NotAccepted)
        {
            Debug.LogWarning("Nhiệm vụ đã được nhận hoặc hoàn thành.");
            return false;
        }

        var task = taskList[currentTaskIndex].taskName;
        if (task != null && taskList[currentTaskIndex].taskStatus == TaskStatus.NotAccepted)
        {
            taskList[currentTaskIndex].taskStatus = TaskStatus.InProgress;
            Debug.Log($"Đã nhận nhiệm vụ: {taskList[currentTaskIndex].taskName}");
            return true;
        }
        return false;
    }

    // Hàm hủy nhiệm vụ 
    public bool CancelTask()
    {
        if (currentTaskIndex == -1) return false;

        var task = taskList[currentTaskIndex].taskName;
        if (task != null && taskList[currentTaskIndex].taskStatus == TaskStatus.InProgress)
        {
            taskList[currentTaskIndex].taskStatus = TaskStatus.NotAccepted;
            taskList[currentTaskIndex].taskQuantityCurrent = 0;

            Debug.Log($"Đã hủy nhiệm vụ: {taskList[currentTaskIndex].taskName}");
            PickRandomTaskIndex();
            return true;
        }
        return false;
    }

    // Hàm hoàn thành nhiệm vụ
    public bool CompleteTask()
    {
        if (currentTaskIndex == -1) return false;

        var task = taskList[currentTaskIndex].taskName;
        if (task != null && taskList[currentTaskIndex].taskStatus == TaskStatus.Completed)
        {
            taskList[currentTaskIndex].taskStatus = TaskStatus.NotAccepted;
            Debug.Log($"Đã hoàn thành nhiệm vụ: {taskList[currentTaskIndex].taskName}");
            taskList[currentTaskIndex].taskQuantityCurrent = 0;
            int expReward = taskList[currentTaskIndex].taskExpReward;
            Debug.Log($"Bạn đã nhận được {expReward} kinh nghiệm!");
            // Sau khi hoàn thành, chọn nhiệm vụ mới
            PickRandomTaskIndex();
            return true;
        }
        return false;
    }

    public void PickRandomTaskIndex()
    {
        if (taskList == null || taskList.Count == 0)
        {
            currentTaskIndex = -1;
            return;
        }

        // Lọc ra các nhiệm vụ chưa nhận hoặc đã hoàn thành
        List<int> availableIndexes = new List<int>();
        for (int i = 0; i < taskList.Count; i++)
        {
            if (taskList[i].taskStatus == TaskStatus.NotAccepted || taskList[i].taskStatus == TaskStatus.Completed)
                availableIndexes.Add(i);
        }

        if (availableIndexes.Count == 0)
        {
            currentTaskIndex = -1;
            return;
        }

        int randomIndex = UnityEngine.Random.Range(0, availableIndexes.Count);
        currentTaskIndex = availableIndexes[randomIndex];
    }

}

[Serializable]
public class TaskData
{

    public string taskName;
    [TextArea(3, 10)] public string taskDescription;
    public int taskQuantityRequest;
    public int taskQuantityCurrent;
    public int taskExpReward;
    public TaskStatus taskStatus;
}

public enum TaskStatus
{
    NotAccepted = 0,
    InProgress = 1,
    Completed = 2
}
