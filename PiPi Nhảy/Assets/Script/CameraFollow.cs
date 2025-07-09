using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player;               // Transform của nhân vật
    public Vector3 offset = Vector3.zero;  // Offset nếu muốn camera nhìn lệch một chút
    public float followSpeed = 5f;         // Tốc độ theo dõi
    private bool shouldFollow = false;

    void LateUpdate()
    {
        if (player != null && shouldFollow)
        {
            Vector3 targetPosition = player.position + offset;
            transform.position = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
            // Không cần xoay nữa vì Pivot đã có rotation cố định
        }
    }

    public void StartFollowing()
    {
        shouldFollow = true;
    }

    public void StopFollowing()
    {
        shouldFollow = false;
    }
    public void AdjustCamera(Vector3 offset)
    {
        transform.position += offset;
    }
}
