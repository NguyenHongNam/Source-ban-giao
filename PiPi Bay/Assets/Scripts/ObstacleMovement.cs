using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float destroyXPosition = -10f;

    void Update()
    {
        if (PlayerController.currentScore >= 10 && PlayerController.currentScore < 30)
        {
            moveSpeed = 3f; // hoặc bất kỳ giá trị nào bạn muốn
        }
        transform.position += Vector3.left * moveSpeed * Time.deltaTime;

        if (transform.position.x < destroyXPosition)
        {
            Destroy(gameObject);
        }
    }
}
