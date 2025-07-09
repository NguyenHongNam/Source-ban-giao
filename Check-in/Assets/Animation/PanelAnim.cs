using DG.Tweening;
using UnityEngine;

public class PanelAnim : MonoBehaviour
{
    [System.Serializable]
    public class Panel
    {
        public string panelName; // Tên của panel (dùng để tham chiếu)
        public GameObject panelObject; // GameObject của panel
    }

    public Panel[] panels; // Danh sách các panel
    public float animationDuration = 0.3f; // Thời gian hiệu ứng
    public Vector3 closedScale = new Vector3(0f, 0f, 1f); // Kích thước khi panel đóng

    // Mở panel
    public void OpenPanel(string panelName)
    {
        Panel panel = GetPanelByName(panelName);
        if (panel != null && panel.panelObject != null)
        {
            panel.panelObject.SetActive(true);
            panel.panelObject.transform.localScale = closedScale; // Bắt đầu từ kích thước thu nhỏ
            panel.panelObject.transform.DOScale(Vector3.one, animationDuration)
                .SetEase(Ease.OutBack); // Mở rộng về kích thước ban đầu
        }
    }

    // Đóng panel
    public void ClosePanel(string panelName)
    {
        Panel panel = GetPanelByName(panelName);
        if (panel != null && panel.panelObject != null)
        {
            panel.panelObject.transform.DOScale(closedScale, animationDuration)
                .SetEase(Ease.InBack)
                .OnComplete(() => panel.panelObject.SetActive(false)); // Ẩn panel sau khi hiệu ứng hoàn tất
        }
    }

    // Lấy panel theo tên
    private Panel GetPanelByName(string panelName)
    {
        foreach (var panel in panels)
        {
            if (panel.panelName == panelName)
            {
                return panel;
            }
        }
        Debug.LogWarning($"Không tìm thấy panel có tên: {panelName}");
        return null;
    }
}
