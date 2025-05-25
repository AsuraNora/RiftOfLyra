using UnityEngine;

public class PoisionNeedle : MonoBehaviour
{
    public void OnDestroyPoisionNeedle()
    {
        Destroy(gameObject);
    }
}
