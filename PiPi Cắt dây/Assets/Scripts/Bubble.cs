using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private Rigidbody2D rb;
    private Renderer objectRenderer;

    public float gravityScaleValue = 0.1f;
    private bool isLifting = false;

    private GameObject attachedBall;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        objectRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        // Nếu đang nâng kẹo, thì move bubble lên
        if (isLifting && attachedBall != null)
        {
            transform.position += Vector3.up * 1.5f * Time.deltaTime;
            attachedBall.transform.position = transform.position;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            attachedBall = other.gameObject;
            liftBubble();
        }
        else if (other.name == "Cutter")
        {
            AudioManager.instance.PlayPopBubble();
            destroyBubble();
        }
    }

    private void liftBubble()
    {
        //if (attachedBall == null) return;

        //// Tắt gravity của kẹo
        //var ballRb = attachedBall.GetComponent<Rigidbody2D>();
        //ballRb.gravityScale = 0;
        //ballRb.linearVelocity = Vector2.zero;

        //// Bắt đầu nâng
        //isLifting = true;
        isLifting = true;

        // Gắn ball làm con của bubble để đi theo
        attachedBall.transform.SetParent(transform);

        // Tắt ảnh hưởng vật lý
        Rigidbody2D ballRb = attachedBall.GetComponent<Rigidbody2D>();
        ballRb.linearVelocity = Vector2.zero;
        ballRb.gravityScale = 0;
        ballRb.isKinematic = true; // Tắt physics nếu cần

        // Di chuyển ball tới đúng vị trí bong bóng
        attachedBall.transform.position = transform.position;
    }

    private void destroyBubble()
    {
        if (attachedBall != null)
        {
            var ballRb = attachedBall.GetComponent<Rigidbody2D>();
            ballRb.gravityScale = 0.55f;
            ballRb.isKinematic = false;
            attachedBall.transform.SetParent(null); // bỏ gắn con
        }

        Destroy(gameObject);
    }
}
