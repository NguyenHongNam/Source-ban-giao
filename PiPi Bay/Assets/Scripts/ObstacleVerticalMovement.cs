using UnityEngine;

public class ObstacleVerticalMovement : MonoBehaviour
{
    public float moveAmplitude = 1f;  // biên độ di chuyển
    public float moveSpeed = 1f;      // tốc độ di chuyển lên xuống
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        // Chỉ bắt đầu di chuyển khi đạt mốc 30 điểm
        if (PlayerController.currentScore >= 30)
        {
            float newY = startPos.y + Mathf.Sin(Time.time * moveSpeed) * moveAmplitude;
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);
        }
    }
}
