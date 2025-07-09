using DG.Tweening;
using UnityEngine;

public class VersionWarning : MonoBehaviour
{
    public RectTransform popup; // Kéo popup vào đây
    public float moveDistance = 100f; // Khoảng cách di chuyển
    public float moveDuration = 1f; // Thời gian di chuyển
    public float stayDuration = 2f; // Thời gian popup ở trạng thái hiển thị

    void Start()
    {
        if (popup != null)
        {
            Vector2 originalPosition = popup.anchoredPosition;
            Vector2 targetPosition = originalPosition - new Vector2(0, moveDistance);

            popup.DOAnchorPos(targetPosition, moveDuration) // Di chuyển xuống
                .OnComplete(() =>
                {
                    // Chờ một chút rồi di chuyển lên
                    DOVirtual.DelayedCall(stayDuration, () =>
                    {
                        popup.DOAnchorPos(originalPosition, moveDuration);
                    });
                });
        }
    }
}
