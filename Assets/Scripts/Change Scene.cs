using System;
using UnityEngine;

public class ChangeScene : MonoBehaviour
{
    [SerializeField] private GameObject positon; // Vị trí mục tiêu cho nhân vật
    [SerializeField] private float moveDistance = 2f; // Khoảng cách di chuyển qua lại
    [SerializeField] private float moveSpeed = 2f; // Tốc độ di chuyển qua lại

    private Vector3 startPosition; // Vị trí ban đầu của cổng

    private void Start()
    {
        // Lưu vị trí ban đầu của cổng
        startPosition = transform.position;
    }

    private void Update()
    {
        // Tạo hiệu ứng di chuyển qua lại theo trục X
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance) - (moveDistance / 2f);
        transform.position = new Vector3(startPosition.x + offset, startPosition.y, startPosition.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("Player đã chạm vào cổng!");

            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                Debug.Log("Player được tìm thấy: " + player.name);

                player.transform.position = positon.transform.position;

                // // Gắn lại camera nếu cần
                // if (Camera.main != null)
                // {
                //     Camera.main.transform.SetParent(player.transform);
                //     Camera.main.transform.localPosition = new Vector3(0, 1.6f, -4);
                // }
                // else
                // {
                //     Debug.LogWarning("Không tìm thấy Main Camera!");
                // }
                GameObject playerClone = GameObject.Find("Player(Clone)");
                if (playerClone != null)
                {
                    Destroy(playerClone);
                }
                GameObject TestPlayerClone = GameObject.Find("Test Player(Clone)");
                if (TestPlayerClone != null)
                {
                    Destroy(TestPlayerClone);
                }

            }
            else
            {
                Debug.LogWarning("Không tìm thấy Player!");
            }
        }
    }

}
