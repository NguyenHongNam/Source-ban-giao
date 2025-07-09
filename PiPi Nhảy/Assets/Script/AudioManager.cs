using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance; // Singleton instance

    [Header("Audio Sources")]
    public AudioSource musicSource;     // Nhạc nền
    public AudioSource sfxSource;       // Hiệu ứng âm thanh

    [Header("Audio Clips")]
    public AudioClip backgroundMusic;   // Nhạc nền
    public AudioClip[] sfxClips;        // Danh sách hiệu ứng âm thanh
    private bool isMusicOn = true;
    void Awake()
    {
        // Singleton Pattern: Đảm bảo chỉ có một AudioManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Không phá hủy khi chuyển scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().name != "Menu")
        {
            PlayMusic();
        }
        else
        {
            Destroy(gameObject); // Tự hủy nếu ở Menu để không giữ nhạc
        }
    }

    // Phát nhạc nền
    public void PlayMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.loop = true;
            musicSource.Play();
        }
    }

    // Phát hiệu ứng âm thanh
    public void PlaySFX(string sfxName)
    {
        if (sfxSource != null && sfxClips.Length > 0)
        {
            AudioClip clip = GetSFXClipByName(sfxName);
            if (clip != null)
            {
                sfxSource.PlayOneShot(clip);
            }
        }
    }

    // Lấy clip hiệu ứng âm thanh theo tên
    private AudioClip GetSFXClipByName(string name)
    {
        foreach (AudioClip clip in sfxClips)
        {
            if (clip.name == name)
            {
                return clip;
            }
        }
        return null;
    }

    // Bật/Tắt nhạc nền
    public void ToggleMusic()
    {
        if (musicSource != null)
        {
            musicSource.mute = !musicSource.mute;
        }
    }

    // Bật/Tắt hiệu ứng âm thanh
    public void ToggleBackgroundMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
            musicSource.Play();
        else
            musicSource.Pause();
    }
}
