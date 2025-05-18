using UnityEngine;

public class ForceSortingLayerByTag : MonoBehaviour
{
    public string targetTag = "UI";               // Tag bạn muốn tìm
    public string sortingLayerName = "UI";        // Sorting Layer mới
    public int sortingOrder = 100;                // Thứ tự hiển thị

    void Start()
    {
        GameObject target = GameObject.FindWithTag(targetTag);
        if (target != null)
        {
            Renderer renderer = target.GetComponent<Renderer>();
            if (renderer != null)
            {
                renderer.sortingLayerName = sortingLayerName;
                renderer.sortingOrder = sortingOrder;
            }
            else
            {
                Debug.LogWarning("Không tìm thấy Renderer trên GameObject có tag: " + targetTag);
            }
        }
        else
        {
            Debug.LogWarning("Không tìm thấy GameObject với tag: " + targetTag);
        }
    }
}
