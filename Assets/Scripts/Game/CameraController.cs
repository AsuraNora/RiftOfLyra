using UnityEngine;
using Unity.Cinemachine;

public class CameraController : MonoBehaviour
{
    public CinemachineCamera virtualCamera;

    protected void Start()
    {
        // Đảm bảo rằng virtualCamera đã được gán trong Inspector
        if (virtualCamera == null)
        {
            Debug.LogError("Virtual Camera is not assigned in the Inspector.");
        }
    }

    public void SetTarget(Transform target)
    {
        if (virtualCamera != null)
        {
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
        }
    }
}
