using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ProgressButtonAnim : MonoBehaviour
{
    public Button[] buttons; // Danh sách các nút cần hiệu ứng
    public float pressedScaleY = 0.8f; // Tỉ lệ dẹt theo Y
    public float animationDuration = 0.1f; // Thời gian hiệu ứng

    void Start()
    {
        foreach (Button button in buttons)
        {
            // Thêm hiệu ứng nhấn vào mỗi nút
            button.onClick.AddListener(() => PlayButtonEffect(button.transform));
        }
    }

    void PlayButtonEffect(Transform buttonTransform)
    {
        Vector3 originalScale = buttonTransform.localScale;

        // Hiệu ứng co dẹt
        buttonTransform.DOScale(new Vector3(originalScale.x, originalScale.y * pressedScaleY, originalScale.z), animationDuration)
            .SetEase(Ease.OutQuad)
            .OnComplete(() =>
                // Trở lại kích thước ban đầu
                buttonTransform.DOScale(originalScale, animationDuration).SetEase(Ease.OutQuad)
            );
    }
}
