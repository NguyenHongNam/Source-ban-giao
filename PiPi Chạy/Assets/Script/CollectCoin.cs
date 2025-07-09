using UnityEngine;

public class CollectCoin : MonoBehaviour
{
    [SerializeField] AudioSource collectSound;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Tạo một âm thanh rời để phát mà không bị phá hủy
            AudioSource.PlayClipAtPoint(collectSound.clip, transform.position);

            // Cộng thêm vào số lượng coin
            MasterInfo.coinCount++;
            CarController.playerSpeed += 0.3f;
            // Hủy đối tượng ngay lập tức
            Destroy(gameObject);
        }
    }
}
