using UnityEngine;

public class RewardControl : MonoBehaviour
{
    public int rewardValue; // Giá trị phần thưởng
    public float weight;
    private bool isCaught = false;
    public bool isBlindBag = false;
    public bool isBomb = false;

    private Transform hookTransform;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Hook") && !isCaught)
        {
            Debug.Log("Caught by hook!");
            isCaught = true;
            hookTransform = other.transform;
            transform.parent = other.transform; // Gắn vào móc
            GetComponent<Rigidbody2D>().isKinematic = true;
        }
    }
    void Update()
    {
        if (isCaught && hookTransform != null)
        {
            // Kiểm tra nếu móc đã quay về vị trí bắt đầu
            if (Vector3.Distance(hookTransform.position, hookTransform.GetComponent<HookControl>().startPosition) < 0.1f)
            {
                // Cộng điểm
                GameManager.Instance.AddScore(rewardValue);

                // Hủy phần thưởng
                Destroy(gameObject);
            }
        }
    }
}
