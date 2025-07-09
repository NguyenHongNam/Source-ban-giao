using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public AudioSource audioSource;
    public AudioSource loseSFX;

    public AudioClip bgm;
    public AudioClip lose;
    private bool isMusicOn = true;
    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (audioSource != null && bgm != null)
        {
            audioSource.clip = bgm;
            audioSource.loop = true;
            audioSource.Play();
        }
    }
    public void ToggleBackgroundMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
            audioSource.Play();
        else
            audioSource.Pause();
    }
    public void PlayBgm() => audioSource.PlayOneShot(bgm);
    public void PlayLose() => loseSFX.PlayOneShot(lose);
}
