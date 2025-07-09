using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.5f;
    private SpriteRenderer spriteRenderer; // Thay đổi từ MeshRenderer thành SpriteRenderer

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // Lấy SpriteRenderer thay vì MeshRenderer
    }

    void Update()
    {
        float offset = Time.time * scrollSpeed;
        spriteRenderer.material.mainTextureOffset = new Vector2(offset, 0);
    }
}
