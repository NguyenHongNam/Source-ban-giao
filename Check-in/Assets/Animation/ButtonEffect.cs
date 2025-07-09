using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ButtonEffect : MonoBehaviour
{
    public Button button; // Nút cần hiệu ứng
    public float pressedScale = 0.9f; // Kích thước khi bị nhấn (nhỏ hơn 1 để tạo hiệu ứng lún)
    public float duration = 0.1f; // Thời gian hiệu ứng

    private Vector3 originalScale; // Kích thước ban đầu của nút

    void Start()
    {
        // Lưu lại kích thước ban đầu
        originalScale = transform.localScale;

        // Gắn sự kiện nhấn cho nút
        button.onClick.AddListener(() => OnButtonClicked());
    }

    void OnButtonClicked()
    {
        // Hiệu ứng lún xuống và bật lại
        transform.DOScale(Vector3.one * pressedScale, duration)
            .OnComplete(() => transform.DOScale(originalScale, duration));
    }
}
