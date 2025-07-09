using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Text scoreText;
    public int score = 0;
    public HealthUI healthUI;
    [SerializeField] private int playerHealth = 3;

    private bool isGameOver = false;
    private bool hasShield = false;
    public GameObject gameOverPanel; // Panel Game Over
    private PlayerController playerController;
    void Start()
    {
        gameOverPanel.SetActive(false); // Ẩn panel khi bắt đầu
        Time.timeScale = 1;
        playerController = FindObjectOfType<PlayerController>();
        if (playerController != null)
        {
            playerController.DisableShieldEffect(); // Tắt khiên ban đầu
        }
    }
    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "" + score;
    }
    public void ActivateShield()
    {
        hasShield = true;

        if (playerController != null)
        {
            playerController.EnableShieldEffect(); // Bật Sphere hiệu ứng khiên
        }
    }
    public void TakeDamage(int damage)
    {
        if (hasShield)
        {
            hasShield = false; // Mất khiên thay vì mất máu
            if (playerController != null)
                playerController.DisableShieldEffect(); // Tắt hiệu ứng khiên
            Debug.Log("Blocked damage with shield!");
            return;
        }

        playerHealth -= damage;
        playerHealth = Mathf.Clamp(playerHealth, 0, 3);

        healthUI.UpdateHealth(playerHealth);

        if (playerHealth <= 0)
        {
            GameOver();
        }
    }
    public void GiveHeart()
    {
        if (playerHealth < 3)
        {
            playerHealth++;
            healthUI.UpdateHealth(playerHealth);
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Time.timeScale = 0;
        gameOverPanel.SetActive(true); // Hiển thị panel Game Over
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // Tải lại scene hiện tại
    }

}
