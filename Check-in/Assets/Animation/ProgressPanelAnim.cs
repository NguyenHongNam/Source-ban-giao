using DG.Tweening;
using UnityEngine;

public class ProgressPanelAnim : MonoBehaviour
{
    [Header("Popup Settings")]
    public GameObject congratulationPopup; // Panel popup chúc mừng

    public void CloseCongratulationPopup()
    {
        congratulationPopup.transform.DOScale(Vector3.zero, 0.3f)
               .SetEase(Ease.InBack) // Hiệu ứng thu nhỏ
               .OnComplete(() =>
               {
                   congratulationPopup.SetActive(false); // Ẩn sau khi thu nhỏ
                   congratulationPopup.transform.localScale = Vector3.one; // Đặt lại kích thước ban đầu
               });
    }
}
