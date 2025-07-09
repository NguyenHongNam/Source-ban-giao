using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonManaget : MonoBehaviour
{
    public GameObject musicObject;  // Tham chiếu đến GameObject chứa nhạc nền
    public Button toggleButton;     // Tham chiếu đến Button điều khiển nhạc nền

    void Start()
    {
        // Kiểm tra nếu Button và Music Object đã được gán
        if (toggleButton != null && musicObject != null)
        {
            // Đăng ký sự kiện click của button
            toggleButton.onClick.AddListener(ToggleMusic);
        }
        else
        {
            Debug.LogError("Chưa gán Button hoặc Music Object!");
        }
    }

    // Hàm để bật/tắt nhạc nền
    void ToggleMusic()
    {
        musicObject.SetActive(!musicObject.activeSelf);
    }
}
