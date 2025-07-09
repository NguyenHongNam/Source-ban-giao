using UnityEngine;
using UnityEngine.UI;

public class FloatingTextEffect : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float fadeDuration = 2f;

    private Text textComponent;
    private float timer = 0f;

    void Start()
    {
        textComponent = GetComponent<Text>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        // Di chuyển lên
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        // Làm mờ dần
        if (timer > fadeDuration)
        {
            Destroy(gameObject); // Xóa sau khi hiệu ứng kết thúc
        }
        else
        {
            Color color = textComponent.color;
            color.a = Mathf.Lerp(1f, 0f, timer / fadeDuration); // Giảm độ trong suốt
            textComponent.color = color;
        }
    }

    public void SetText(string text)
    {
        if (textComponent != null)
        {
            textComponent.text = text;
        }
    }
}
