using UnityEngine;

public class CameraBounds : MonoBehaviour
{
    public Camera mainCamera;
    private BoxCollider2D bounds;

    void Start()
    {
        bounds = GetComponent<BoxCollider2D>();

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
        }
    }

    void LateUpdate()
    {
        Vector3 cameraPosition = mainCamera.transform.position;
        Vector3 minBounds = bounds.bounds.min;
        Vector3 maxBounds = bounds.bounds.max;

        float cameraHalfWidth = mainCamera.orthographicSize * ((float)Screen.width / Screen.height);
        float cameraHalfHeight = mainCamera.orthographicSize;

        cameraPosition.x = Mathf.Clamp(cameraPosition.x, minBounds.x + cameraHalfWidth, maxBounds.x - cameraHalfWidth);
        cameraPosition.y = Mathf.Clamp(cameraPosition.y, minBounds.y + cameraHalfHeight, maxBounds.y - cameraHalfHeight);

        mainCamera.transform.position = cameraPosition;
    }
}
