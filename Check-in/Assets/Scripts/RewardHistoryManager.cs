using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RewardHistoryManager : MonoBehaviour
{
    [System.Serializable]
    public class Reward
    {
        public string receivedTime; // Thời gian nhận
        public int points;          // Số điểm nhận
    }

    public GameObject rewardPrefab;  // Prefab của RewardItem
    public Transform contentParent;  // Content của ScrollView
    private List<Reward> rewardHistory = new List<Reward>(); // Danh sách lịch sử

    // Thêm phần thưởng vào lịch sử
    public void AddReward(string time, int points)
    {
        // Lưu vào danh sách
        Reward newReward = new Reward { receivedTime = time, points = points };
        rewardHistory.Add(newReward);

        // Hiển thị trong ScrollView
        GameObject rewardItem = Instantiate(rewardPrefab, contentParent);
        rewardItem.transform.Find("TimeText").GetComponent<Text>().text = time;   // Text hiển thị thời gian
        rewardItem.transform.Find("PointsText").GetComponent<Text>().text = points.ToString(); // Text hiển thị điểm
    }

    // Hàm test để thêm dữ liệu
    public void TestAddReward()
    {
        string currentTime = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        int randomPoints = Random.Range(10, 100);
        AddReward(currentTime, randomPoints);
    }
}
