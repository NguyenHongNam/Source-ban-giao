using UnityEngine;

public class ModelRotator : MonoBehaviour
{
    public float idleRotationSpeed = 30f;    // Tốc độ xoay tự động
    public float dragRotationSpeed = 0.2f;   // Tốc độ xoay khi kéo

    private bool isDragging = false;
    private Vector2 lastMousePosition;

    void Update()
    {
        // Nếu đang nhấn chuột hoặc giữ ngón tay
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform == transform)
                {
                    isDragging = true;
                    lastMousePosition = Input.mousePosition;
                }
            }
        }

        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
        }

        if (isDragging)
        {
            Vector2 delta = (Vector2)Input.mousePosition - lastMousePosition;
            float rotationY = -delta.x * dragRotationSpeed;

            transform.Rotate(Vector3.up, rotationY, Space.World);

            lastMousePosition = Input.mousePosition;
        }
        else
        {
            // Xoay tự động khi không tương tác
            transform.Rotate(Vector3.up, idleRotationSpeed * Time.deltaTime, Space.World);
        }
    }
}
