using Newtonsoft.Json;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Globalization;
using System;
[System.Serializable]
public class StockReward
{
    [JsonProperty("value")]
    public string value;
    [JsonProperty("quantity")]
    public int quantity;
}

[System.Serializable]
public class StockRewardRespone
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("data")]
    public List<StockReward> Data { get; set; } // Danh sách phần thưởng
}
public class RewardDisplay : MonoBehaviour
{
    private const string apiUrl = "https://apigamifi.mobifoneplus.vn/rewards/getRewardsStock";

    [System.Serializable]
    public class RewardTarget
    {
        public GameObject rewardObject; // GameObject chứa Text UI
        public string targetValue; // Giá trị cần hiển thị
        public Text rewardText;
    }
    public List<RewardTarget> rewardTargets;
    public void GetRewardStock()
    {
        string authToken = APIManager.authToken;
        //string authToken = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJpZCI6MSwiZW1haWwiOiJpbmFwcGdhbWUyNUBnbWFpbC5jb20iLCJyb2xlIjoiQURNSU4iLCJsbSI6IkVNQUlMIiwiaWF0IjoxNzM5MjQxMDc3LCJleHAiOjE3NDQ0MjUwNzd9.DpuJ-OFBm4LyO7ZjBLXDoUIcjsPi2l9Q_km0IKqdJSE";
        StartCoroutine(GetRewardData(authToken));
    }

    public IEnumerator GetRewardData(string authToken)
    {
        using UnityWebRequest www = UnityWebRequest.Get(apiUrl);
        www.SetRequestHeader("Authorization", "Bearer " + authToken);
        www.SetRequestHeader("Accept", "*/*");

        yield return www.SendWebRequest();

        if (www.result == UnityWebRequest.Result.Success)
        {
            string responseText = www.downloadHandler.text;
            StockRewardRespone rewardData = JsonConvert.DeserializeObject<StockRewardRespone>(responseText);

            // Tạo dictionary để tra cứu nhanh
            Dictionary<string, int> rewardDict = new Dictionary<string, int>();
            foreach (var reward in rewardData.Data)
            {
                if (!rewardDict.ContainsKey(reward.value))
                {
                    rewardDict[reward.value] = reward.quantity;
                }
            }

            // Duyệt qua từng `RewardTarget` và cập nhật thông tin
            foreach (var target in rewardTargets)
            {
                if (target.rewardText != null) // Kiểm tra nếu `rewardText` được gán
                {
                    if (rewardDict.ContainsKey(target.targetValue))
                    {
                        string formattedQuantity = rewardDict[target.targetValue].ToString("N0", new CultureInfo("vi-VN"));
                        target.rewardText.text = formattedQuantity;
                    }
                    else
                    {
                        target.rewardText.text = "0"; // Nếu không tìm thấy value
                    }
                }
                else if (target.rewardObject != null) // Dự phòng nếu `rewardText` không được gán
                {
                    Text fallbackText = target.rewardObject.GetComponentInChildren<Text>();
                    if (fallbackText != null)
                    {
                        if (rewardDict.ContainsKey(target.targetValue))
                        {
                            string formattedQuantity = rewardDict[target.targetValue].ToString("N0", new CultureInfo("vi-VN"));
                            fallbackText.text = formattedQuantity;
                        }
                        else
                        {
                            fallbackText.text = "0";
                        }
                    }
                }
            }
        }
        else
        {
            Debug.LogError("Error: " + www.error);
        }
    }
}