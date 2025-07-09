using UnityEngine;
using UnityEngine.UI;

public class ScrollViewSwitcher : MonoBehaviour
{
    public GameObject scrollView1;
    public GameObject scrollView2;

    public Button button1;
    public Button button2;

    public Image button1Image;
    public Image button2Image;

    public Text button1Text;
    public Text button2Text;

    public Sprite activeButtonSprite;
    public Sprite inactiveButtonSprite;

    public Color activeTextColor = HexToColor("#64687E"); // Màu chữ cho nút active
    public Color inactiveTextColor = Color.white;
    private void Start()
    {
        ShowScrollView1();
    }
    public void ShowScrollView1()
    {
        scrollView1.SetActive(true); // Mở Quà của tôi
        scrollView2.SetActive(false); // Đóng Quà còn lại

        // Cập nhật sprite cho button và màu chữ
        button1Image.sprite = activeButtonSprite; // Button Quà của tôi active
        button2Image.sprite = inactiveButtonSprite; // Button Quà còn lại inactive

        button1Text.color = activeTextColor; // Màu chữ cho Quà của tôi active
        button2Text.color = inactiveTextColor;
    }

    public void ShowScrollView2()
    {
        scrollView1.SetActive(false); // Đóng Quà của tôi
        scrollView2.SetActive(true); // Mở Quà còn lại

        // Cập nhật sprite cho button và màu chữ
        button1Image.sprite = inactiveButtonSprite; // Button Quà của tôi inactive
        button2Image.sprite = activeButtonSprite; // Button Quà còn lại active

        button1Text.color = inactiveTextColor; // Màu chữ cho Quà của tôi inactive
        button2Text.color = activeTextColor; // Màu chữ cho Quà còn lại active
    }

    public static Color HexToColor(string hex)
    {
        // Xóa ký tự '#' nếu có
        hex = hex.Replace("#", "");

        // Nếu màu có 6 ký tự (RGB)
        if (hex.Length == 6)
        {
            byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
            byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
            byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
            return new Color32(r, g, b, 255);
        }
        else
        {
            return Color.white;
        }
    }
}