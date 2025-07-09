using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cutter : MonoBehaviour
{
    public GameObject objectToIgnore;

    private Collider2D cutterCollider;

    void Start()
    {
        cutterCollider = GetComponent<Collider2D>();
        cutterCollider.isTrigger = true;
        if (objectToIgnore != null)
        {
            Physics2D.IgnoreCollision(cutterCollider, objectToIgnore.GetComponent<Collider2D>(), true);
        }
        else
        {
            Debug.LogWarning("GameObject to ignore not found.");
        }

        // Ban đầu tắt collider để không cắt lung tung
        cutterCollider.enabled = false;
    }

    void Update()
    {
        // Khi người chơi nhấn giữ chuột trái (hoặc chạm giữ trên màn hình)
        if (Input.GetMouseButton(0))
        {
            Vector2 worldMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = worldMousePosition;

            // Bật collider để có thể cắt
            cutterCollider.enabled = true;
        }
        else
        {
            // Không nhấn giữ => không cắt
            cutterCollider.enabled = false;
        }
    }
}
