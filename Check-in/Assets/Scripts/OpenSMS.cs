using UnityEngine;

public class OpenSMS : MonoBehaviour
{
    public string fixedPhoneNumber = "9084"; // Số điện thoại cố định
    [TextArea] public string[] messages; // Danh sách nội dung tin nhắn

    public void OpenMessagingApp(int buttonIndex)
    {
        // Kiểm tra index hợp lệ
        if (buttonIndex < 0 || buttonIndex >= messages.Length)
        {
            Debug.LogError("Index không hợp lệ!");
            return;
        }

        // Lấy nội dung tin nhắn từ danh sách
        string message = messages[buttonIndex];

        // Tạo URI cho ứng dụng tin nhắn
        string smsUri = $"sms:{fixedPhoneNumber}?body={WWW.EscapeURL(message)}";

        // Điều hướng sang ứng dụng tin nhắn
        Application.OpenURL(smsUri);
    }
}
