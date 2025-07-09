using UnityEngine;

public class MasterInfo : MonoBehaviour
{
    public static int coinCount = 0;
    [SerializeField] GameObject coinDisplay;

    void Start()
    {
        coinCount = 0; // Đặt lại số coin về 0 khi bắt đầu scene
    }

    void Update()
    {
        coinDisplay.GetComponent<UnityEngine.UI.Text>().text = "" + coinCount;
    }
}
