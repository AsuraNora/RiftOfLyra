using UnityEngine;

public class RemainGameObject : MonoBehaviour
{
    public GameObject[] gameObjects;

    void Awake()
    {
        foreach (GameObject obj in gameObjects)
        {
            DontDestroyOnLoad(obj);
        }
    }

    
}
