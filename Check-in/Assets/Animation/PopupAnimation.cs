using DG.Tweening;
using UnityEngine;

public class PopupAnimation : MonoBehaviour
{
    public float zoomScale = 0.8f; // Kích thước ban đầu khi xuất hiện
    public float duration = 0.5f; // Thời gian hiệu ứng

    void Start()
    {
        transform.localScale = Vector3.zero; // Panel bắt đầu từ kích thước 0
    }

    public void ShowPopup()
    {
        Debug.Log("Hiển thị Prize Popup với hiệu ứng Zoom In!");
        transform.localScale = Vector3.zero; // Reset kích thước
        transform.DOScale(zoomScale, duration / 2).SetEase(Ease.OutBack) // Zoom đến mức trung gian
                .OnComplete(() => transform.DOScale(1f, duration / 2).SetEase(Ease.OutBack)); // Zoom đến kích thước gốc
    }

    public void ClosePopup()
    {
        Debug.Log("Đóng Prize Popup với hiệu ứng Zoom Out!");
        transform.DOScale(0f, duration / 2).SetEase(Ease.InBack) // Zoom nhỏ lại khi ẩn
                .OnComplete(() => gameObject.SetActive(false)); // Tắt popup sau khi hiệu ứng kết thúc
    }
}
