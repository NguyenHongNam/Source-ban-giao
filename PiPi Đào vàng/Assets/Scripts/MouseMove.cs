using UnityEngine;

public class MouseMove : MonoBehaviour
{
    public float moveDistance = 2f;
    public float moveSpeed = 1f;

    private Vector3 startPos;
    private float lastX;
    private bool facingRight = true;

    void Start()
    {
        startPos = transform.position;
        lastX = transform.position.x;
    }

    void Update()
    {
        float offset = Mathf.PingPong(Time.time * moveSpeed, moveDistance * 2) - moveDistance;
        float newX = startPos.x + offset;
        transform.position = new Vector3(newX, startPos.y, startPos.z);

        // So sánh hướng hiện tại với hướng cũ
        if (newX > lastX && !facingRight)
        {
            Flip(true); // sang phải
        }
        else if (newX < lastX && facingRight)
        {
            Flip(false); // sang trái
        }

        lastX = newX;
    }

    void Flip(bool toRight)
    {
        facingRight = toRight;
        Vector3 scale = transform.localScale;
        scale.x = toRight ? Mathf.Abs(scale.x) : -Mathf.Abs(scale.x);
        transform.localScale = scale;
    }
}
