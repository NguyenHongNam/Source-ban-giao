using UnityEngine;

public class PlayerBounceEffect : MonoBehaviour
{
    public Vector3 normalScale = new Vector3(1f, 1f, 1f);  // Kích thước bình thường
    public Vector3 squashedScale = new Vector3(1.2f, 0.8f, 1f); // Kích thước khi nhún
    public float bounceDuration = 0.2f;                     // Thời gian cho hiệu ứng nhún

    private Vector3 targetScale;                           // Kích thước đích
    private float lerpTime;                                // Thời gian nội suy

    private void Start()
    {
        targetScale = normalScale;                         // Bắt đầu với scale bình thường
        transform.localScale = normalScale;
    }

    private void Update()
    {
        // Nhấn giữ: Co lại
        if (Input.GetMouseButton(0))
        {
            StartBounceEffect(squashedScale);
        }

        // Thả tay: Quay về trạng thái bình thường
        if (Input.GetMouseButtonUp(0))
        {
            StartBounceEffect(normalScale);
        }

        // Thay đổi scale theo thời gian
        if (transform.localScale != targetScale)
        {
            lerpTime += Time.deltaTime / bounceDuration;
            transform.localScale = Vector3.Lerp(transform.localScale, targetScale, lerpTime);

            // Đảm bảo không vượt quá giá trị đích
            if (lerpTime >= 1f)
            {
                transform.localScale = targetScale;
                lerpTime = 0f;
            }
        }
    }

    private void StartBounceEffect(Vector3 newScale)
    {
        targetScale = newScale;
        lerpTime = 0f; // Reset thời gian nội suy
    }
}
