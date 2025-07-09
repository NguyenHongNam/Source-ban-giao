using DG.Tweening;
using UnityEngine;

public class RulePanelAnim : MonoBehaviour
{
    public RectTransform panel; // Panel cần thực hiện animation
    public float animationDuration = 0.5f; // Thời gian animation
    public Ease openEase = Ease.OutCubic; // Hiệu ứng mượt khi mở
    public Ease closeEase = Ease.InOutQuart; // Hiệu ứng mượt khi đóng

    private Vector2 hiddenPosition; // Vị trí panel khi ẩn
    private Vector2 visiblePosition; // Vị trí panel khi hiện
    private bool isVisible = false; // Trạng thái panel

    void Start()
    {
        // Lưu lại vị trí hiện tại làm vị trí hiển thị
        visiblePosition = panel.anchoredPosition;

        // Tính toán vị trí ẩn dựa trên chiều cao của panel
        hiddenPosition = new Vector2(visiblePosition.x, visiblePosition.y - panel.rect.height);

        // Đặt panel ở trạng thái ẩn ban đầu
        panel.anchoredPosition = hiddenPosition;
    }

    public void TogglePanel()
    {
        if (isVisible)
        {
            // Panel đóng: Di chuyển từ vị trí hiện về trên (ẩn)
            panel.DOAnchorPos(hiddenPosition, animationDuration).SetEase(closeEase);
        }
        else
        {
            // Panel mở: Di chuyển từ dưới lên vị trí hiển thị
            panel.DOAnchorPos(visiblePosition, animationDuration).SetEase(openEase);
        }

        isVisible = !isVisible; // Đảo trạng thái
    }
}
