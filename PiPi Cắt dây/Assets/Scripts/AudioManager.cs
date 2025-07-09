using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioSource bgMusicSource;
    public AudioSource sfxSource;

    public AudioClip bgMusic;
    public AudioClip eatCandyClip;
    public AudioClip cutRopeClip;
    public AudioClip windMachineClip;
    public AudioClip popBubbleClip;
    public AudioClip collectStarClip;

    private bool isMusicOn = true;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        PlayBackgroundMusic();
    }

    public void PlayBackgroundMusic()
    {
        if (bgMusicSource != null && bgMusic != null)
        {
            bgMusicSource.clip = bgMusic;
            bgMusicSource.loop = true;
            bgMusicSource.Play();
        }
    }

    public void PlayEatCandy() => sfxSource.PlayOneShot(eatCandyClip);
    public void PlayCutRope() => sfxSource.PlayOneShot(cutRopeClip);
    public void PlayWindMachine() => sfxSource.PlayOneShot(windMachineClip);
    public void PlayPopBubble() => sfxSource.PlayOneShot(popBubbleClip);
    public void PlayCollectStar() => sfxSource.PlayOneShot(collectStarClip);
    public void ToggleBackgroundMusic()
    {
        isMusicOn = !isMusicOn;

        if (isMusicOn)
            bgMusicSource.Play();
        else
            bgMusicSource.Pause();
    }

}
