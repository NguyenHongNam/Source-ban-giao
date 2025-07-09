using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 5f;
    public float forwardSpeed = 3f;
    public Text scoreText;
    public GameObject gameOverPanel;
    public GameObject startPanel;
    public Text finalScoreText;
    public Text highScoreText;

    private Rigidbody2D rb;
    private int score = 0;
    private bool gameStarted = false;
    private bool gameOver = false;
    private Animator animator;

    public static int currentScore = 0;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        rb.gravityScale = 0;
        Time.timeScale = 1;
        gameOverPanel.SetActive(false);
        startPanel.SetActive(true);
        UpdateScore();
    }

    void Update()
    {
        if (gameOver) return;

        // Kiểm tra input để bắt đầu game hoặc nhảy
        if ((Input.GetMouseButtonDown(0) || (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)) && !gameOver)
        {
            if (!gameStarted)
            {
                StartGame();
            }
            else
            {
                Jump();
            }
        }

        // Giới hạn góc xoay của người chơi
        if (rb.linearVelocity.y < 0)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, -45f), Time.deltaTime * 5f);
        }
        else if (rb.linearVelocity.y > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 45f);
        }
    }

    void StartGame()
    {
        gameStarted = true;
        rb.gravityScale = 2.5f;
        rb.linearVelocity = Vector2.zero;
        Jump();
        startPanel.SetActive(false);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector2(0, jumpForce);
        AudioManager.instance.PlaySound("Jump");
    }

    void UpdateScore()
    {
        scoreText.text = score.ToString();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("ScoreZone"))
        {
            score++;
            currentScore = score;
            UpdateScore();
            AudioManager.instance.PlaySound("Score");
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstacle") || collision.gameObject.CompareTag("Ground"))
        {
            if (!gameOver)
            {
                GameOver();
            }
        }
    }

    void GameOver()
    {
        gameOver = true;
        finalScoreText.text = "" + score.ToString();
        gameOverPanel.SetActive(true);
        AudioManager.instance.PlaySound("Losing");

        // Lưu high score
        int highScore = PlayerPrefs.GetInt("HighScore", 0);
        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetInt("HighScore", highScore);
            PlayerPrefs.Save();
        }

        // Hiển thị điểm cao nhất trong màn hình Game Over
        highScoreText.text = "" + highScore.ToString();
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
