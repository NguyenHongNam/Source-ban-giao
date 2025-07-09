using UnityEngine;
using UnityEngine.UI;

public class MissionManager : MonoBehaviour
{
    [System.Serializable]
    public class Mission
    {
        public Button statusButton; // Nút trạng thái
        public Sprite notCompletedSprite; // Sprite chưa làm
        public Sprite completedSprite; // Sprite đã làm
        public bool isCompleted = false; // Trạng thái nhiệm vụ (mặc định chưa làm)
    }
    public Mission[] missions;
    public GameObject questionPanel; // Panel câu hỏi
    public GameObject videoPanel; // Panel video
    public GameObject minigamePanel; // Panel video
    public GameObject checkinPanel; // Panel checkin

    public GameObject congratulationPopup;
    public GameObject alreadyCompletedPopup;

    void Start()
    {
        // Ẩn các panel con
        questionPanel.SetActive(false);
        videoPanel.SetActive(false);
        minigamePanel.SetActive(false);
        congratulationPopup.SetActive(false);
        alreadyCompletedPopup.SetActive(false);
        foreach (var mission in missions)
        {
            UpdateMissionStatus(mission);
        }
    }

    // Hàm cập nhật trạng thái của nhiệm vụ
    void UpdateMissionStatus(Mission mission)
    {
        if (mission.isCompleted)
        {
            mission.statusButton.image.sprite = mission.completedSprite; // Đổi sprite sang "Đã làm"
            mission.statusButton.interactable = false; // Vô hiệu hóa nút
        }
        else
        {
            mission.statusButton.image.sprite = mission.notCompletedSprite; // Đổi sprite sang "Chưa làm"
            mission.statusButton.interactable = true; // Cho phép nhấn
        }
    }
    // Hàm gọi khi hoàn thành nhiệm vụ
    public void CompleteMission(int missionIndex)
    {
        if (missionIndex < 0 || missionIndex >= missions.Length)
        {
            Debug.LogError("Chỉ số nhiệm vụ không hợp lệ.");
            return;
        }

        var mission = missions[missionIndex];
        if (!mission.isCompleted)
        {
            mission.isCompleted = true; // Đánh dấu nhiệm vụ là đã hoàn thành
            UpdateMissionStatus(mission); // Cập nhật trạng thái giao diện
            //CheckAllMissionsCompleted(); // Kiểm tra tất cả nhiệm vụ đã hoàn thành chưa
        }
    }
    //Nhiệm vụ check in
    public void Checkin()
    {
        var checkinMission = missions[0]; // Giả định nhiệm vụ check-in là nhiệm vụ đầu tiên trong mảng

        if (checkinMission.isCompleted)
        {
            // Nếu nhiệm vụ đã hoàn thành, hiện speech bubble
            Debug.Log("Nhiệm vụ check-in đã hoàn thành trước đó.");
            ShowSpeechBubble("Bạn đã hoàn thành nhiệm vụ check-in rồi!"); // Nội dung thông báo
            return;
        }

        // Nếu chưa hoàn thành, đánh dấu nhiệm vụ là đã hoàn thành
        Debug.Log("Hoàn thành nhiệm vụ check-in.");
        CompleteMission(0); // Đánh dấu nhiệm vụ check-in là hoàn thành
        congratulationPopup.SetActive(true); // Hiển thị popup chúc mừng
    }

    //Nhiệm vụ trả lời câu hỏi
    public void OnQuestionButtonClicked()
    {
        Debug.Log("Nhiệm vụ: Trả lời câu hỏi");
        questionPanel.SetActive(true);
    }
    public void CloseQuestionPanel()
    {
        questionPanel.SetActive(false);
    }

    //Nhiệm vụ xem quảng cáo
    public void OnVideoButtonClicked()
    {
        Debug.Log("Nhiệm vụ: Xem video");
        videoPanel.SetActive(true);
    }
    public void CloseVideoPanel()
    {
        videoPanel.SetActive(false);
        CompleteMission(1);
    }

    //Nhiệm vụ mini game
    public void OnMiniGameButtonClicked()
    {
        Debug.Log("Nhiệm vụ: Mini game");
        minigamePanel.SetActive(true);
    }

    public void CloseMiniGamePanel()
    {
        minigamePanel.SetActive(false);
    }

    void ShowCongratulationPopup()
    {
        congratulationPopup.SetActive(true); // Hiện popup
    }

    void ShowSpeechBubble(string message)
    {
        alreadyCompletedPopup.SetActive(true); // Hiện speech bubble
        Invoke(nameof(HideSpeechBubble), 1.5f); // Tự động ẩn sau 3 giây
    }

    void HideSpeechBubble()
    {
        alreadyCompletedPopup.SetActive(false); // Ẩn speech bubble
    }
    void CheckAllMissionsCompleted()
    {
        foreach (var mission in missions)
        {
            if (!mission.isCompleted)
            {
                return; // Nếu có nhiệm vụ chưa làm, thoát khỏi hàm
            }
        }

        // Nếu tất cả nhiệm vụ đều hoàn thành
        Debug.Log("Tất cả nhiệm vụ đã hoàn thành!");
        ShowCongratulationPopup();
    }
}
