using UnityEngine;
using UnityEngine.UI;

public class NewMonoBehaviourScript : MonoBehaviour
{
    public AudioClip buttonSFX; // Clip âm thanh dùng chung
    private AudioSource audioSource; // AudioSource để phát âm thanh

    void Start()
    {
        // Kiểm tra xem đã có AudioSource chưa, nếu chưa thì thêm vào
        audioSource = Object.FindFirstObjectByType<AudioSource>();
        if (audioSource == null)
        {
            GameObject audioSourceObject = new GameObject("GlobalAudioSource");
            audioSource = audioSourceObject.AddComponent<AudioSource>();
            DontDestroyOnLoad(audioSourceObject); // Giữ AudioSource khi chuyển scene
        }

        // Gắn sự kiện OnClick cho button
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(PlaySFX);
        }
    }

    // Hàm phát SFX
    public void PlaySFX()
    {
        if (buttonSFX != null && audioSource != null)
        {
            audioSource.PlayOneShot(buttonSFX);
        }
    }
}
