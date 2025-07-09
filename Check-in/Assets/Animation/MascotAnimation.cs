using DG.Tweening;
using UnityEngine;

public class MascotAnimation : MonoBehaviour
{
    public float bounceHeight = 10f; // Độ cao nhún nhảy (theo trục Y)
    public float bounceDuration = 1f; // Thời gian cho một lần nhún
    public float rotateAngle = 10f; // Góc lắc nhẹ
    public float rotateDuration = 0.5f; // Thời gian cho một lần lắc

    private Vector3 originalPosition; // Vị trí ban đầu của mascot
    private Vector3 originalRotation; // Góc xoay ban đầu

    public float squashScale = 0.8f; // Tỷ lệ ép dẹp
    public float stretchScale = 1.2f; // Tỷ lệ kéo dài
    public float duration = 0.1f; // Thời gian hiệu ứng cho mỗi bước

    private Vector3 originalScale; // Kích thước ban đầu

    void Start()
    {
        // Lưu vị trí và góc xoay ban đầu
        originalPosition = transform.localPosition;
        originalRotation = transform.localEulerAngles;

        originalScale = transform.localScale;
        // Bắt đầu hiệu ứng Idle
        StartIdleAnimation();
    }

    void StartIdleAnimation()
    {
        // Tạo hiệu ứng nhún nhảy (Bounce)
        transform.DOLocalMoveY(originalPosition.y + bounceHeight, bounceDuration)
                 .SetEase(Ease.InOutSine) // Hiệu ứng mượt
                 .SetLoops(-1, LoopType.Yoyo); // Lặp vô hạn
    }

    void OnDisable()
    {
        // Dừng mọi animation khi game object bị disable
        transform.DOKill();
    }

    public void OnButtonPressed()
    {
        Debug.Log("Nhân vật bị nhấn!");

        // Sequence để thực hiện chuỗi hiệu ứng
        Sequence sequence = DOTween.Sequence();

        sequence.Append(transform.DOScale(new Vector3(originalScale.x * squashScale, originalScale.y * stretchScale, originalScale.z), duration)) // Ép dẹp
                .Append(transform.DOScale(new Vector3(originalScale.x * stretchScale, originalScale.y * squashScale, originalScale.z), duration)) // Kéo dài
                .Append(transform.DOScale(originalScale, duration)) // Trở về kích thước ban đầu
                .OnComplete(() => Debug.Log("Hiệu ứng hoàn thành!"));
    }
}
