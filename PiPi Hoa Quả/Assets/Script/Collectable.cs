using UnityEngine;

public class Collectable : MonoBehaviour
{
    public int scoreValue = 1; // Điểm cộng khi hứng
    public bool isDangerous = false; // Xác định đây là vật nguy hiểm
    public bool isShield;
    public bool isHeart = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager gameController = FindObjectOfType<GameManager>();
            if (isShield)
            {
                gameController.ActivateShield(); // Kích hoạt khiên
            }
            else if (isDangerous)
            {
                gameController.TakeDamage(1); // Trừ máu
            }
            else if (isHeart)
            {
                gameController.GiveHeart(); // Hồi máu
            }
            else
            {
                gameController.AddScore(scoreValue); // Cộng điểm
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Ground"))
        {
            Destroy(gameObject);
        }
    }
}
