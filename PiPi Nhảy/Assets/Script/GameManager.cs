using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject gameOverPanel;

    void Start()
    {
        // Đảm bảo Game Over Panel ẩn khi bắt đầu
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(false);
        }
    }

    // Gọi hàm này khi game kết thúc
    public void ShowGameOverPanel()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            Time.timeScale = 0f; // Dừng game
        }
    }

    // Restart game
    public void RestartGame()
    {
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    // Trở về màn hình chính
    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Khôi phục thời gian
        SceneManager.LoadScene("MainMenu"); // Đảm bảo bạn có Scene MainMenu
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Game"); // Đảm bảo bạn có Scene GameScene
    }
    public void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Load lại màn chơi hiện tại
    }
}
