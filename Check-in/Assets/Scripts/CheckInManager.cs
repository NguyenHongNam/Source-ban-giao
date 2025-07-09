using UnityEngine;
using UnityEngine.UI;

public class CheckInManager : MonoBehaviour
{
    public MascotManager mascotManager;

    [Header("UI References")]
    public GameObject missionPanel; // Panel hiển thị nhiệm vụ
    public Button[] dayButtons; // Các button ngày
    public Text[] dayTexts; // Các Text hiển thị số ngày trên button

    [Header("Check-in Data")]
    public int checkInDays; // Số ngày đã điểm danh (API trả về)

    [Header("Button Sprites")] 
    public Sprite completedSprite; // Sprite cho nút đã hoàn thành
    public Sprite notCompletedSprite; // Sprite cho nút chưa hoàn thành
    public Sprite currentDaySprite; // Sprite cho nút của ngày hiện tại
    void Start()
    {

        missionPanel.SetActive(false);

        // Cập nhật số ngày trên button
        UpdateDayButtons();
        mascotManager.UpdateMascotPosition();
    } 

    void UpdateDayButtons()
    {
        int pageStartDay = checkInDays - (checkInDays % dayButtons.Length); // Ngày bắt đầu của trang hiện tại

        for (int i = 0; i < dayButtons.Length; i++)
        {
            int dayNumber = pageStartDay + i + 1; // Số ngày cho button hiện tại
            dayTexts[i].text = dayNumber.ToString(); // Gán số ngày vào Text

            // Cập nhật trạng thái của nút
            if (i < checkInDays % dayButtons.Length) // Ngày đã hoàn thành
            {
                dayButtons[i].image.sprite = completedSprite;
            }
            else if (i == checkInDays % dayButtons.Length) // Ngày hiện tại
            {
                dayButtons[i].image.sprite = currentDaySprite;
                dayButtons[i].interactable = true; // Có thể nhấn
            }
            else // Ngày chưa hoàn thành
            {
                dayButtons[i].image.sprite = notCompletedSprite;
            }
        }
    }
    public void CloseMissionPanel()
    {
        missionPanel.SetActive(false);
    }
}
