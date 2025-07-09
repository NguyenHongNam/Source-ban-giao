using UnityEngine;

public class CloudController : MonoBehaviour
{
    public float speed = 4f; // Tốc độ di chuyển
    public float destroyZ = -130f; // Vị trí Z để hủy đám mây

    void Update()
    {
        // Di chuyển đám mây theo trục Z
        transform.Translate(Vector3.back * speed * Time.deltaTime);

        // Kiểm tra nếu đám mây đã ra khỏi màn hình
        if (transform.position.z < destroyZ)
        {
            Destroy(gameObject); // Hủy đám mây
        }
    }
}
