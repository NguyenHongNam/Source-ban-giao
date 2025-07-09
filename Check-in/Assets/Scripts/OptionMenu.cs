using DG.Tweening;
using UnityEngine;

public class OptionMenu : MonoBehaviour
{
    public RectTransform optionPanel; // Panel chứa các nút
    public GameObject[] childButtons; // Các nút con: Âm lượng, Thể lệ, Lịch sử
    public float expandedHeight = 100f; // Chiều cao khi mở
    public float collapsedHeight = 50f; // Chiều cao khi đóng
    public float animationDuration = 0.3f; // Thời gian hiệu ứng

    private bool isExpanded = false; // Trạng thái hiện tại của menu
    private Vector2 originalPosition; // Vị trí gốc của panel

    void Start()
    {
        // Lưu vị trí gốc của panel
        originalPosition = optionPanel.anchoredPosition;

        // Thiết lập panel ẩn khi bắt đầu
        optionPanel.sizeDelta = new Vector2(optionPanel.sizeDelta.x, collapsedHeight); // Đặt chiều cao về trạng thái thu gọn
        optionPanel.anchoredPosition = originalPosition; // Đặt lại vị trí gốc
        optionPanel.gameObject.SetActive(false);
        SetChildButtonsActive(false);
    }

    public void ToggleOptionMenu()
    {
        if (isExpanded)
        {
            // Thu gọn menu: Đóng lên trên
            optionPanel.DOSizeDelta(new Vector2(optionPanel.sizeDelta.x, collapsedHeight), animationDuration)
                       .SetEase(Ease.InOutSine);
            optionPanel.DOAnchorPos(originalPosition, animationDuration)
                       .SetEase(Ease.InOutSine)
                       .OnComplete(() =>
                       {
                           SetChildButtonsActive(false);
                           optionPanel.gameObject.SetActive(false); // Ẩn panel khi đóng
                       });
        }
        else
        {
            // Hiện panel trước khi mở rộng
            optionPanel.gameObject.SetActive(true);
            SetChildButtonsActive(true); // Hiện các nút con trước
            optionPanel.DOSizeDelta(new Vector2(optionPanel.sizeDelta.x, expandedHeight), animationDuration)
                       .SetEase(Ease.InOutSine);
            optionPanel.DOAnchorPos(new Vector2(originalPosition.x, originalPosition.y - (expandedHeight - collapsedHeight) / 2), animationDuration)
                       .SetEase(Ease.InOutSine);
        }

        isExpanded = !isExpanded; // Đảo trạng thái
    }

    // Hàm bật/tắt trạng thái các nút con
    void SetChildButtonsActive(bool isActive)
    {
        foreach (var button in childButtons)
        {
            button.SetActive(isActive);
        }
    }
}
