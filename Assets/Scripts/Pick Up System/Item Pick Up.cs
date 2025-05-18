using UnityEngine;
using Inventory.Model;
using System;
using System.Collections;

public class ItemPickUp : MonoBehaviour
{
    [field: SerializeField] public ItemSO InventoryItem { get; private set; }
    [field: SerializeField] public int Quantity { get;  set; } = 1;
    // [SerializeField] AudioSource audioSource;
    [SerializeField] float duaration = 0.3f;

    public void Start()
    {
        // Kiểm tra xem InventoryItem có được gán hay không
        if (InventoryItem == null)
        {
            Debug.LogError("InventoryItem is not assigned on " + gameObject.name);
            return;
        }

        // Kiểm tra xem SpriteRenderer có tồn tại hay không
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer not found on " + gameObject.name);
            return;
        }

        // Gán sprite từ InventoryItem
        spriteRenderer.sprite = InventoryItem.itemImage;
    }

    internal void DestroyItem() 
    {
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(AnimateItemPickUp());
    }

    private IEnumerator AnimateItemPickUp()
    {
        //audioSource.Play();
        Vector3 startScale = transform.localScale;
        Vector3 endScale = Vector3.zero;
        float currentTime = 0;

        while( currentTime < duaration ) 
        {
            currentTime += Time.deltaTime;
            transform.localScale = Vector3.Lerp(startScale, endScale, currentTime / duaration);
            yield return null;
        }

        transform.localScale = endScale;
        Destroy(gameObject);
    }
}
