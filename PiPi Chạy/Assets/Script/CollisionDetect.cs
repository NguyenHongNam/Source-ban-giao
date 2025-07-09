using System.Collections;
using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    [SerializeField] GameObject thePlayer;
    [SerializeField] GameObject playerAnim;
    [SerializeField] GameObject gameOverPanel;
    [SerializeField] GameObject mainCam;
    [SerializeField] GameObject fadeOut;
    [SerializeField] AudioSource collisionFX;

    [SerializeField] GameObject gameoverPanel;
    void OnTriggerEnter(Collider other)
    {
        StartCoroutine(CollisionEnd()); // Bắt đầu coroutine xử lý va chạm
    }

    IEnumerator CollisionEnd()
    {
        collisionFX.Play(); // Dừng âm thanh va chạm
        thePlayer.GetComponent<CarController>().enabled = false; // Tắt điều khiển xe
        playerAnim.GetComponent<Animator>().Play("stumble"); // Kích hoạt hoạt ảnh va chạm
        mainCam.GetComponent<Animator>().Play("CollisionCam");
        yield return new WaitForSeconds(3f);
        gameOverPanel.SetActive(true); // Hiển thị bảng Game Over
    }
}

