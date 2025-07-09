using UnityEngine;

public class TapFX : MonoBehaviour
{
    public float scaleSpeed = 2f; // Tốc độ thay đổi kích thước
    public float maxScale = 1.1f; // Kích thước lớn nhất
    public float minScale = 0.9f; // Kích thước nhỏ nhất

    void Update()
    {
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(Time.time * scaleSpeed) + 1) / 2);
        transform.localScale = new Vector3(scale, scale, 1);
    }
}
