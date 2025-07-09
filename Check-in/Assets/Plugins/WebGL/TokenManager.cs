using System.Runtime.InteropServices;
using UnityEngine;

public class TokenManager : MonoBehaviour
{
    [DllImport("__Internal")]
    private static extern string GetTokenFromHeader();

    private string token = "";

    void Start()
    {
        // Có thể tự động lấy token khi component được khởi tạo
#if UNITY_WEBGL && !UNITY_EDITOR
            FetchToken();
#endif
    }

    // Phương thức công khai để lấy token từ JavaScript
    public void FetchToken()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            token = GetTokenFromHeader();
            Debug.Log("Token received from WebView: " + (string.IsNullOrEmpty(token) ? "No token found" : "Token found"));
#else
        Debug.LogWarning("GetTokenFromHeader only works in WebGL builds, not in Editor");
        token = "sample_test_token"; // Token giả lập cho việc kiểm tra trong Editor
#endif
    }

    // Phương thức để các script khác truy cập token
    public string GetCurrentToken()
    {
        if (string.IsNullOrEmpty(token))
        {
            Debug.LogWarning("Token is empty, trying to fetch again");
            FetchToken();
        }
        return token;
    }

    // Phương thức debug để kiểm tra token trong Inspector
    void OnGUI()
    {
        // Chỉ hiển thị khi đang trong Development Build
#if DEVELOPMENT_BUILD
            GUI.Label(new Rect(10, 10, 300, 20), "Token Status: " + (string.IsNullOrEmpty(token) ? "Not loaded" : "Loaded"));
#endif
    }
}
