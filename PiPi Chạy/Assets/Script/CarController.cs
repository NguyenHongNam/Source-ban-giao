using System.Collections;
using UnityEngine;

public class CarController : MonoBehaviour
{
    public static float playerSpeed = 5f;
    public float laneWidth = 3f;
    public float horizontalSpeed = 10f;
    public float jumpHeight = 3f;
    public float jumpDuration = 1f;

    private int currentLaneIndex = 0;
    private int minLaneIndex = -1;
    private int maxLaneIndex = 1;

    private Vector2 startTouchPosition;
    private Vector2 endTouchPosition;

    private bool canChangeLane = true;
    private bool isJumping = false;

    private Animator animator;
    private float groundY;
    private bool isGrounded;

    void Start()
    {
        animator = GetComponent<Animator>();
        groundY = transform.position.y;
    }

    void Update()
    {
        // Di chuyển xe tiến về trước
        transform.Translate(Vector3.forward * Time.deltaTime * playerSpeed, Space.World);

        // Xử lý input
        HandleSwipe();
        HandleMouseInput();

        // Tính vị trí đích theo lane
        float targetX = currentLaneIndex * laneWidth;
        Vector3 newPosition = transform.position;
        newPosition.x = Mathf.Lerp(transform.position.x, targetX, Time.deltaTime * horizontalSpeed);
        transform.position = newPosition;

        // Reset flag chuyển làn khi gần tới vị trí đích
        if (Mathf.Abs(transform.position.x - targetX) < 0.05f)
        {
            canChangeLane = true;
        }
    }

    void HandleSwipe()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                startTouchPosition = touch.position;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                endTouchPosition = touch.position;
                Vector2 delta = endTouchPosition - startTouchPosition;

                if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // Vuốt ngang
                {
                    if (delta.x < -50 && currentLaneIndex > minLaneIndex && canChangeLane)
                    {
                        currentLaneIndex--;
                        canChangeLane = false;
                    }
                    else if (delta.x > 50 && currentLaneIndex < maxLaneIndex && canChangeLane)
                    {
                        currentLaneIndex++;
                        canChangeLane = false;
                    }
                }
                else // Vuốt dọc
                {
                    if (delta.y > 50)
                    {
                        StartCoroutine(Jump());
                    }
                    else if (delta.y < -50)
                    {
                        StartCoroutine(FlipRotate());
                    }
                }
            }
        }
    }

    void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startTouchPosition = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            endTouchPosition = Input.mousePosition;
            Vector2 delta = endTouchPosition - startTouchPosition;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y)) // vuốt ngang
            {
                if (delta.x < -50 && currentLaneIndex > minLaneIndex && canChangeLane)
                {
                    currentLaneIndex--;
                    canChangeLane = false;
                }
                else if (delta.x > 50 && currentLaneIndex < maxLaneIndex && canChangeLane)
                {
                    currentLaneIndex++;
                    canChangeLane = false;
                }
            }
            else // vuốt dọc
            {
                if (delta.y > 50)
                {
                    StartCoroutine(Jump());
                }
                else if (delta.y < -50)
                {
                    StartCoroutine(FlipRotate());
                }
            }
        }
    }

    IEnumerator Jump()
    {
        if (isJumping) yield break;

        isJumping = true;
        float startY = transform.position.y;
        float time = 0f;

        if (animator != null)
            animator.SetTrigger("Jump");

        while (time < jumpDuration)
        {
            time += Time.deltaTime;
            float percent = time / jumpDuration;
            float height = 4 * jumpHeight * percent * (1 - percent); // parabolic
            Vector3 pos = transform.position;
            pos.y = groundY + height;
            transform.position = pos;
            yield return null;
        }

        // Đảm bảo quay lại đúng mặt đất
        Vector3 finalPos = transform.position;
        finalPos.y = groundY;
        transform.position = finalPos;

        isJumping = false;
    }

    IEnumerator FlipRotate()
    {
        float duration = 0.3f;
        float elapsed = 0f;
        Quaternion startRot = transform.rotation;
        Quaternion targetRot = startRot * Quaternion.Euler(360, 0, 0); // lộn 1 vòng quanh trục X

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            transform.rotation = Quaternion.Slerp(startRot, targetRot, elapsed / duration);
            yield return null;
        }

        transform.rotation = startRot;
    }
}
