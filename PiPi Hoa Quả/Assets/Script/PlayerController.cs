using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class PlayerController : MonoBehaviour
{
    public float speed = 10f; // Tốc độ di chuyển
    private Camera mainCamera; // Camera chính
    private float screenBoundsX; // Giới hạn X của màn hình
    private float playerWidth; // Chiều rộng của nhân vật

    public bool isDangerous = false; // Xác định đây là vật nguy hiểm

    public AudioSource audioSource; // Tham chiếu đến AudioSource
    public AudioClip eatSound; // Âm thanh khi ăn quả
    public AudioClip bombSound;
    public AudioClip shieldSound;
    public AudioClip heartSound;

    public Animator animator;
    public float shakeDuration = 0.2f; // Thời gian rung
    public float shakeMagnitude = 0.2f; // Độ mạnh của rung

    public GameObject shieldEffectObject;
    void Start()
    {
        mainCamera = Camera.main;

        // Tính toán giới hạn X dựa trên kích thước màn hình và nhân vật
        float screenAspect = (float)Screen.width / Screen.height;
        float cameraHeight = mainCamera.orthographicSize * 2;
        screenBoundsX = (cameraHeight * screenAspect) / 2;

        // Lấy chiều rộng của nhân vật từ Collider
        playerWidth = GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void Update()
    {
        // Xử lý cảm ứng
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                MoveCharacter(touch.position);
            }
        }

        // Xử lý chuột
        if (Input.GetMouseButton(0))
        {
            MoveCharacter(Input.mousePosition);
        }
    }

    void MoveCharacter(Vector3 inputPosition)
    {
        // Tính toán vị trí mục tiêu
        Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(inputPosition.x, inputPosition.y, 0));
        Vector3 targetPosition = new Vector3(worldPosition.x, transform.position.y, transform.position.z);

        // Giới hạn vị trí X trong phạm vi màn hình
        targetPosition.x = Mathf.Clamp(targetPosition.x, -screenBoundsX + playerWidth, screenBoundsX - playerWidth);

        // Di chuyển nhân vật
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fruit"))
        {
            animator.SetTrigger("EatFruit");
            audioSource.PlayOneShot(eatSound);
        }
        else if (other.CompareTag("Bomb"))
        {
            animator.SetTrigger("Cry");
            audioSource.PlayOneShot(bombSound);
            StartCoroutine(ShakeCharacter());
        }
        else if (other.CompareTag("Shield"))
        {
            animator.SetTrigger("EatFruit");
            audioSource.PlayOneShot(shieldSound);
        }
        else if (other.CompareTag("Heart"))
        {
            animator.SetTrigger("EatFruit");
            audioSource.PlayOneShot(heartSound);
        }
    }

    IEnumerator ShakeCharacter()
    {
        Vector3 originalPosition = transform.position;
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            float offsetX = Random.Range(-shakeMagnitude, shakeMagnitude);
            float offsetY = Random.Range(-shakeMagnitude, shakeMagnitude);

            transform.position = new Vector3(originalPosition.x + offsetX, originalPosition.y + offsetY, originalPosition.z);

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition;
    }
    public void EnableShieldEffect()
    {
        if (shieldEffectObject != null)
            shieldEffectObject.SetActive(true);
    }

    public void DisableShieldEffect()
    {
        if (shieldEffectObject != null)
            shieldEffectObject.SetActive(false);
    }
}
