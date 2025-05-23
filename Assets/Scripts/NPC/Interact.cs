using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private float interactionRange = 2f; 
    [SerializeField] private GameObject npcThink;
    [SerializeField] private GameObject canvasCommunication; 
    private bool playerInRange = false;

    void Start()
    {
        canvasCommunication.SetActive(false);
        npcThink.SetActive(false);
    }

    void Update()
    {
        CheckPlayerInRange();
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
}
