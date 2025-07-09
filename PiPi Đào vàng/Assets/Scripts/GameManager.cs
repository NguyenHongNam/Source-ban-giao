using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public Text scoreText;
    public Text targetScoreText;
    public Text timerText;

    public GameObject endGamePanel;
    public Text finalScoreText;

    public GameObject victoryPanel;
    public Button nextLevelButton;

    private int score = 0;
    private int targetScore = 0;
    private float timeRemaining = 60f;

    public bool isGameRunning = true;
    private bool levelCompleted = false;

    private AudioSource audioSource;
    public AudioClip sfxCollectReward;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start()
    {
        RefreshTargetScore(LevelManager.Instance.GetTargetScore());
        UpdateTimerText();

        endGamePanel.SetActive(false);
        victoryPanel.SetActive(false);

        levelCompleted = false;
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    void Update()
    {
        if (isGameRunning)
        {
            UpdateTimer();
        }
    }

    public void AddScore(int value)
    {
        if (!isGameRunning || levelCompleted)
            return;

        score += value;
        scoreText.text = score.ToString();
        finalScoreText.text = score.ToString();

        if (score >= targetScore)
        {
            levelCompleted = true;
            ShowVictoryPanel();
        }
    }

    public void RefreshTargetScore(int newTarget)
    {
        targetScore = newTarget;
        if (targetScoreText != null)
            targetScoreText.text = " " + targetScore.ToString();
    }

    private void ShowVictoryPanel()
    {
        isGameRunning = false;
        victoryPanel.SetActive(true);
    }
    private void UpdateTimer()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else
        {
            timeRemaining = 0;
            EndGame();
        }
    }

    private void UpdateTimerText()
    {
        int seconds = Mathf.CeilToInt(timeRemaining);
        timerText.text = seconds.ToString();
    }

    private void EndGame()
    {
        if (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            UpdateTimerText();
        }
        else if (!levelCompleted) // 👈 chỉ xử lý thua nếu chưa thắng
        {
            timeRemaining = 0;
            endGamePanel.SetActive(true);
        }
    }
    public void NextLevel()
    {
        victoryPanel.SetActive(false);
        LevelManager.Instance.LoadNextLevel();
    }

    public void ResetGame()
    {
        score = 0;
        timeRemaining = 60f;
        isGameRunning = true;
        levelCompleted = false;

        scoreText.text = "0";
        finalScoreText.text = "0";
        UpdateTimerText();

        endGamePanel.SetActive(false);
        victoryPanel.SetActive(false);

        //// Reset Hook nếu có
        //HookControl hook = FindObjectOfType<HookControl>();
        //if (hook != null)
        //    hook.ResetHook();
    }
    private void PlaySFX(AudioClip clip)
    {
        if (clip != null && audioSource != null)
            audioSource.PlayOneShot(clip);
    }
}
