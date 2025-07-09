using UnityEngine;
using UnityEngine.UI;

public class ProgressManager : MonoBehaviour
{
    [Header("UI References")]
    public CheckInManager checkInManager; // Tham chiếu đến CheckInManager
    public Transform gridParent; // Grid chứa các ô ngày (30 ô)
    public Sprite completedSprite; // Sprite đã hoàn thành
    public Sprite defaultSprite; // Sprite chưa hoàn thành

    [Header("Popup Settings")]
    public GameObject congratulationPopup; // Panel popup chúc mừng
    public Image[] dayCells; // Các ô trong Grid

    void Start()
    {
        // Lấy các ô trong Grid
        dayCells = gridParent.GetComponentsInChildren<Image>();

        // Kiểm tra xem có đủ 30 ô không
        if (dayCells.Length < 30)
        {
            Debug.LogError("Số ô trong grid không đủ! Cần có 30 ô.");
            return;
        }

        // Ẩn popup lúc đầu
        congratulationPopup.SetActive(false);
    }

    public void ShowCongratulationPopup()
    {
        // Lấy số ngày check-in từ CheckInManager
        int checkInDays = checkInManager.checkInDays;

        // Cập nhật trạng thái cho từng ô
        for (int i = 0; i < dayCells.Length; i++)
        {
            if (i < checkInDays)
            {
                // Đã hoàn thành
                dayCells[i].sprite = completedSprite;
            }
            else
            {
                // Chưa hoàn thành
                dayCells[i].sprite = defaultSprite;
            }
        }

        // Hiển thị popup
        congratulationPopup.SetActive(true);
    }

    public void CloseCongratulationPopup()
    {
        // Ẩn popup
        congratulationPopup.SetActive(false);
    }
}
