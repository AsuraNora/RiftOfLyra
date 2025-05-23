using System;
using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    [Header("Danh sách nhiệm vụ")]
    [SerializeField] private List<TaskData> taskList = new List<TaskData>();

    // Truy cập danh sách nhiệm vụ
    public List<TaskData> GetTaskList()
    {
        return taskList;
    }

    // Thêm nhiệm vụ mới 
    public void AddTask(TaskData task)
    {
        taskList.Add(task);
    }

    // Cập nhật tiến độ nhiệm vụ theo tên
    public void UpdateTaskProgress(string taskName)
    {
        foreach (var task in taskList)
        {
            if (task.taskName == taskName && task.taskStatus == TaskStatus.InProgress)
            {
                task.taskQuantityCurrent++;

                if (task.taskQuantityCurrent >= task.taskQuantityRequest)
                {
                    task.taskStatus = TaskStatus.Completed;
                    Debug.Log($"Nhiệm vụ hoàn thành: {task.taskName}");
                }
            }
        }
    }

    // Hàm nhận nhiệm vụ 
    public bool AcceptTask(string taskName)
    {
        var task = taskList.Find(t => t.taskName == taskName);
        if (task != null && task.taskStatus == TaskStatus.NotAccepted)
        {
            task.taskStatus = TaskStatus.InProgress;
            Debug.Log($"Đã nhận nhiệm vụ: {task.taskName}");
            return true;
        }
        return false;
    }

    // Hàm hủy nhiệm vụ 
    public bool CancelTask(string taskName)
    {
        var task = taskList.Find(t => t.taskName == taskName);
        if (task != null && task.taskStatus == TaskStatus.InProgress)
        {
            task.taskStatus = TaskStatus.NotAccepted;
            task.taskQuantityCurrent = 0;
            Debug.Log($"Đã hủy nhiệm vụ: {task.taskName}");
            return true;
        }
        return false;
    }
}

[Serializable]
public class TaskData
{
    public string taskName;
    public string taskDescription;
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
