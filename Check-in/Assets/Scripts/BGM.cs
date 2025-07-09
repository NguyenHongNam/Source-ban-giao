using UnityEngine;
using UnityEngine.UI;

public class BGM : MonoBehaviour
{
    public AudioSource backgroundMusic; // AudioSource chứa nhạc nền
    public Button toggleButton; // Button UI để bật/tắt nhạc

    private static bool isMusicPlaying = true; // Trạng thái nhạc (mặc định bật)

    void Start()
    {
        // Kiểm tra nếu nhạc nền đang phát
        if (backgroundMusic != null && backgroundMusic.isPlaying)
        {
            isMusicPlaying = true;
        }
        else
        {
            isMusicPlaying = false;
        }

        // Gắn sự kiện cho nút
        toggleButton.onClick.AddListener(ToggleMusic);
    }

    public void ToggleMusic()
    {
        isMusicPlaying = !isMusicPlaying; // Đảo trạng thái
        backgroundMusic.mute = !isMusicPlaying; // Tắt/bật nhạc
    }
}
