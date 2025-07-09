using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    public Image[] heartImages; // Mảng chứa các hình trái tim

    public void UpdateHealth(int health)
    {
        for (int i = 0; i < heartImages.Length; i++)
        {
            heartImages[i].enabled = i < health; // Hiển thị trái tim nếu còn mạng
        }
    }
}
