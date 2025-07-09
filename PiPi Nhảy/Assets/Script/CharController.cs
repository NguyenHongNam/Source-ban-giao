using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CharController : MonoBehaviour
{
    public float minJumpForce = 0.5f;
    public float maxJumpForce = 3f;
    public float chargeSpeed = 1f;
    public float forwardMultiplier = 0.5f;

    public Transform directionTarget;
    private GameObject lastPlatformTouched = null;

    private Rigidbody rb;
    private bool isGrounded = true;
    private float currentJumpForce = 0f;
    private bool isCharging = false;

    [Header("UI")]
    public Slider jumpForceSlider;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (jumpForceSlider != null)
        {
            jumpForceSlider.minValue = minJumpForce;
            jumpForceSlider.maxValue = maxJumpForce;
            jumpForceSlider.value = 0f;
        }
    }

    void Update()
    {
        if (IsPointerOverUI())
            return;

        if (Input.GetMouseButtonDown(0) && isGrounded)
        {
            isCharging = true;
            currentJumpForce = minJumpForce;

            if (jumpForceSlider != null)
                jumpForceSlider.value = currentJumpForce;
        }

        if (Input.GetMouseButton(0) && isGrounded && isCharging)
        {
            currentJumpForce += chargeSpeed * Time.deltaTime;
            currentJumpForce = Mathf.Clamp(currentJumpForce, minJumpForce, maxJumpForce);

            if (jumpForceSlider != null)
                jumpForceSlider.value = currentJumpForce;
        }

        if (Input.GetMouseButtonUp(0) && isGrounded && isCharging)
        {
            Jump();
            AudioManager.Instance.PlaySFX("jump");
            isCharging = false;

            if (jumpForceSlider != null)
                jumpForceSlider.value = 0f;

            Camera.main.GetComponent<CameraFollow>()?.StopFollowing();
        }
    }

    void Jump()
    {
        Vector3 direction = directionTarget != null ?
                           (directionTarget.position - transform.position).normalized :
                           Vector3.right;

        Vector3 jumpDir = direction * (currentJumpForce * forwardMultiplier) + Vector3.up * currentJumpForce;
        rb.AddForce(jumpDir, ForceMode.Impulse);

        currentJumpForce = 0f;
        isGrounded = false;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Platform") && collision.gameObject != lastPlatformTouched)
        {
            isGrounded = true;
            lastPlatformTouched = collision.gameObject;

            AudioManager.Instance.PlaySFX("success");

            CameraFollow cameraFollow = Camera.main?.GetComponent<CameraFollow>();
            cameraFollow?.StartFollowing();

            PlatformSpawner spawner = FindObjectOfType<PlatformSpawner>();
            if (spawner != null)
            {
                spawner.SpawnNextPlatform();

                if (spawner.lastLandingPoint != null)
                {
                    directionTarget = spawner.lastLandingPoint;
                    Vector3 lookTarget = new Vector3(directionTarget.position.x, transform.position.y, directionTarget.position.z);
                    transform.LookAt(lookTarget);
                }
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Respone"))
        {
            ShowGameOver();
        }
    }

    void ShowGameOver()
    {
        GameManager gameOverManager = FindObjectOfType<GameManager>();
        gameOverManager?.ShowGameOverPanel();
    }

    bool IsPointerOverUI()
    {
        // PC (Editor or Standalone)
        if (Application.isEditor || Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.OSXPlayer)
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }

        // Mobile
        if (Input.touchCount > 0)
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(Input.GetTouch(0).fingerId);
        }

        return false;
    }
}
