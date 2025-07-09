using UnityEngine;

public class MascotManager : MonoBehaviour
{
    public GameObject mascot; // Linh vật
    public Transform[] buttons; // Mảng chứa các button theo thứ tự ngày
    public Vector3 offset = new Vector3(0, 150, 0); // Khoảng cách dịch chuyển (theo trục Y)
    public CheckInManager checkInManager; // Tham chiếu đến CheckInManager

    void Start()
    {
        UpdateMascotPosition();
    }

    public void UpdateMascotPosition()
    {
        int buttonIndex = checkInManager.checkInDays % buttons.Length; // Tính chỉ số button tương ứng
        if (buttonIndex < 0 || buttonIndex >= buttons.Length)
        {
            Debug.LogError("Chỉ số button không hợp lệ.");
            return;
        }

        // Cập nhật vị trí của linh vật
        mascot.transform.position = buttons[buttonIndex].position + offset;
        Debug.Log($"Linh vật được đặt ở button số {buttonIndex + 1}.");
    }
}
