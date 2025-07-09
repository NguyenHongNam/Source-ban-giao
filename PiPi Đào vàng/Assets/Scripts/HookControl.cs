using UnityEngine;

public class HookControl : MonoBehaviour
{
    public float rotationSpeed = 50f;
    public float moveSpeed = 5f;
    public LineRenderer rope;
    private bool isMoving = false;
    private bool isReturning = false;
    public Vector3 startPosition;
    private Vector3 targetPosition;

    public AudioClip sfxShoot;
    public AudioClip sfxHitReward;
    public AudioClip sfxCollectReward;
    public AudioClip sfxTnT;

    private AudioSource audioSource;
    private AudioSource shootAudioSource;

    private RewardControl rewardOnHook = null;

    public GameObject floatingTextPrefab;
    public RectTransform scoreUIText;

    void Start()
    {
        startPosition = transform.position;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (!GameManager.Instance || !GameManager.Instance.isGameRunning)
            return;
        if (!isMoving)
        {
            float angle = Mathf.PingPong(Time.time * rotationSpeed, 120) - 60;
            transform.rotation = Quaternion.Euler(0, 0, angle);

            // 🛠️ Kiểm tra game còn đang chạy trước khi cho phép bắn
            if (Input.GetMouseButtonDown(0) && GameManager.Instance != null && GameManager.Instance.isGameRunning)
            {
                isMoving = true;
                isReturning = false;
                targetPosition = transform.position - transform.up * 10f;
                PlayShootSFX();
            }
        }
        else
        {
            if (!isReturning)
            {
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
                {
                    isReturning = true;
                    targetPosition = startPosition;
                }
            }
            else
            {
                float adjustedSpeed = rewardOnHook != null ? moveSpeed / rewardOnHook.weight : moveSpeed;
                transform.position = Vector3.MoveTowards(transform.position, startPosition, adjustedSpeed * Time.deltaTime);

                if (Vector3.Distance(transform.position, startPosition) < 0.1f)
                {
                    isMoving = false;
                    StopShootSFX();

                    if (rewardOnHook != null)
                    {
                        if (rewardOnHook.isBomb)
                        {
                            PlaySFX(sfxTnT);
                            GameManager.Instance.AddScore(-50);
                            DisplayFloatingText(-50);
                        }
                        else
                        {
                            PlaySFX(sfxCollectReward);
                            GameManager.Instance.AddScore(rewardOnHook.rewardValue);
                            DisplayFloatingText(rewardOnHook.rewardValue);
                        }

                        Destroy(rewardOnHook.gameObject);
                        rewardOnHook = null;
                    }
                }
            }
        }

        // ✅ Hiển thị dây phía trước BG (Z âm)
        Vector3 posStart = startPosition;
        Vector3 posEnd = transform.position;
        posStart.z = -1f;
        posEnd.z = -1f;

        rope.SetPosition(0, posStart);
        rope.SetPosition(1, posEnd);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Reward"))
        {
            isReturning = true;
            targetPosition = startPosition;

            rewardOnHook = collision.GetComponent<RewardControl>();
            if (rewardOnHook != null)
            {
                collision.transform.SetParent(transform);
                collision.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;

                PlaySFX(sfxHitReward);

                if (rewardOnHook.isBlindBag)
                {
                    rewardOnHook.rewardValue = Random.Range(100, 501); // Bao gồm 500
                }
            }
        }
    }

    public void PlaySFX(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }

    private void PlayShootSFX()
    {
        if (shootAudioSource != null && sfxShoot != null)
        {
            shootAudioSource.Play();
        }
    }

    private void StopShootSFX()
    {
        if (shootAudioSource != null && shootAudioSource.isPlaying)
        {
            shootAudioSource.Stop();
        }
    }

    private void DisplayFloatingText(int scoreValue)
    {
        if (floatingTextPrefab != null && scoreUIText != null)
        {
            GameObject floatingText = Instantiate(floatingTextPrefab, scoreUIText.position, Quaternion.identity);
            floatingText.transform.SetParent(GameObject.Find("Overlay").transform, false);

            Vector3 floatingTextPosition = scoreUIText.position;
            floatingTextPosition.y += 1f;
            floatingText.transform.position = floatingTextPosition;

            FloatingTextEffect effect = floatingText.GetComponent<FloatingTextEffect>();
            if (effect != null)
            {
                string prefix = scoreValue >= 0 ? "+" : "";
                effect.SetText(prefix + scoreValue.ToString());
            }
        }
    }

    // ✅ Public để gọi reset từ GameManager hoặc LevelManager
    public void ResetHook()
    {
        transform.rotation = Quaternion.identity;
        isMoving = false;
        isReturning = false;
        rewardOnHook = null;
    }
}
