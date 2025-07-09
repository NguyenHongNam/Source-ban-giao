using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class RewardFetcher : MonoBehaviour
{
    private string apiUrl = "https://apigamifi.mobifoneplus.vn/reward-history/historyByUser";
    private string craftUrl = "https://apigamifi.mobifoneplus.vn/rewards/craftRewards";
    public GameObject rewardPrefab;  // Drag prefab with 2 Text UI in the inspector
    public Transform rewardContainer;  // Content area in ScrollView (where rewards will be instantiated)

    public Text airpodPiece1Text;
    public Text airpodPiece2Text;
    public Text airpodPiece3Text;
    public Text ipPiece1Text;
    public Text ipPiece2Text;
    public Text ipPiece3Text;

    public Button claimAirpodButton;
    public Button claimIphoneButton;

    public Sprite buttonActiveSprite; // Sprite khi nút có thể bấm
    public Sprite buttonInactiveSprite;

    public GameObject ipWinPopup;
    public GameObject apWinPopup;
    public GameObject errorPopup;

    List<int> airpodPieceIds = new List<int>();
    List<int> iphonePieceIds = new List<int>();

    private int airpodPiece1 = 0, airpodPiece2 = 0, airpodPiece3 = 0;
    private int ipPiece1 = 0, ipPiece2 = 0, ipPiece3 = 0;
    void Start()
    {
        ipWinPopup.SetActive(false);
        apWinPopup.SetActive(false);
        errorPopup.SetActive(false);
        string authToken = APIManager.authToken;
        UpdateButtons();
    }

    public IEnumerator FetchRewards(string authToken)
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        request.SetRequestHeader("accept", "*");
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
        request.SetRequestHeader("Content-Type", "application/json");

        // Gửi yêu cầu và chờ phản hồi
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            // Phản hồi thành công
            string jsonResponse = request.downloadHandler.text;
            HandleRewardData(jsonResponse);
        }
        else
        {
            UnityEngine.Debug.LogError("Lỗi khi gọi API: " + request.error);
        }
    }

    public void HandleRewardData(string jsonResponse)
    {
        var response = JsonConvert.DeserializeObject<FetchResponse>(jsonResponse);

        if (response.Status == "success")
        {

            foreach (var rewardData in response.Data)
            {
                string rewardValue = rewardData.reward.value;
                int count = rewardData.count;

                if (rewardValue == "AIRPOD_PIECE_1" || rewardValue == "AIRPOD_PIECE_2" || rewardValue == "AIRPOD_PIECE_3")
                {
                    airpodPieceIds.Add(rewardData.reward.id);
                }
                else if (rewardValue == "IP_PIECE_1" || rewardValue == "IP_PIECE_2" || rewardValue == "IP_PIECE_3")
                {
                    iphonePieceIds.Add(rewardData.reward.id);
                }

                // Kiểm tra nếu phần thưởng đã tồn tại trong UI (dựa vào tên phần thưởng)
                if (rewardValue == "AIRPOD_PIECE_1")
                {
                    airpodPiece1 = count;
                }
                else if (rewardValue == "AIRPOD_PIECE_2")
                {
                    airpodPiece2 = count;
                }
                else if (rewardValue == "AIRPOD_PIECE_3")
                {
                    airpodPiece3 = count;
                }
                else if (rewardValue == "IP_PIECE_1")
                {
                    ipPiece1 = count;
                }
                else if (rewardValue == "IP_PIECE_2")
                {
                    ipPiece2 = count;
                }
                else if (rewardValue == "IP_PIECE_3")
                {
                    ipPiece3 = count;
                }

                // Nếu phần thưởng không có, bạn có thể tạo mới prefab hoặc chỉ tạo một lần khi dữ liệu chưa có
                // Tạo đối tượng mới từ prefab và hiển thị thông tin trong ScrollView (chỉ tạo khi cần thiết)
                GameObject rewardItem = null;
                // Kiểm tra nếu đã có item, nếu chưa có thì tạo mới
                if (rewardContainer.Find(rewardData.name) == null) // Kiểm tra tên
                {
                    rewardItem = Instantiate(rewardPrefab, rewardContainer);
                    rewardItem.name = rewardData.name; // Đặt tên cho prefab mới để dễ dàng tìm kiếm sau này
                    Text nameText = rewardItem.GetComponentInChildren<Text>(); // Giả sử Text UI đầu tiên là tên
                    Text countText = rewardItem.GetComponentsInChildren<Text>()[1];

                    // Cập nhật tên và số lượng
                    nameText.text = rewardData.name; // Tên phần thưởng
                    countText.text = "" + rewardData.count; // Số lượng phần thưởng
                }
                else
                {
                    rewardItem = rewardContainer.Find(rewardData.name).gameObject; // Lấy phần thưởng đã tồn tại
                    Text countText = rewardItem.GetComponentsInChildren<Text>()[1];
                    countText.text = "" + rewardData.count; // Cập nhật lại số lượng
                }
            }
            this.airpodPieceIds = airpodPieceIds;
            this.iphonePieceIds = iphonePieceIds;

            // Cập nhật UI Text với số lượng
            airpodPiece1Text.text = airpodPiece1.ToString();
            airpodPiece2Text.text = airpodPiece2.ToString();
            airpodPiece3Text.text = airpodPiece3.ToString();
            ipPiece1Text.text = ipPiece1.ToString();
            ipPiece2Text.text = ipPiece2.ToString();
            ipPiece3Text.text = ipPiece3.ToString();

            UpdateButtons();
        }
    }
    void UpdateButtons()
    {
        bool hasIphoneReward = false;
        bool hasAirpodReward = false;

        foreach (Transform rewardItem in rewardContainer)
        {
            Text nameText = rewardItem.GetComponentInChildren<Text>();
            if (nameText.text == "Iphone")
            {
                hasIphoneReward = true;
            }
            else if (nameText.text == "Airpod")
            {
                hasAirpodReward = true;
            }
        }
        bool canClaimAirpod = airpodPiece1 >= 1 && airpodPiece2 >= 1 && airpodPiece3 >= 1 && !hasAirpodReward;
        bool canClaimIphone = ipPiece1 >= 1 && ipPiece2 >= 1 && ipPiece3 >= 1! && !hasIphoneReward;

        claimAirpodButton.GetComponent<Image>().sprite = canClaimAirpod ? buttonActiveSprite : buttonInactiveSprite;
        claimAirpodButton.interactable = canClaimAirpod;

        claimIphoneButton.GetComponent<Image>().sprite = canClaimIphone ? buttonActiveSprite : buttonInactiveSprite;
        claimIphoneButton.interactable = canClaimIphone;
    }

    public void ClaimAirpods()
    {
        if (airpodPiece1 >= 1 && airpodPiece2 >= 1 && airpodPiece3 >= 1)
        {
            StartCoroutine(CraftReward(airpodPieceIds, true));
        }
    }

    public void ClaimIphone()
    {
        if (ipPiece1 >= 1 && ipPiece2 >= 1 && ipPiece3 >= 1)
        {
            StartCoroutine(CraftReward(iphonePieceIds, false));
        }
    }
    private IEnumerator CraftReward(List<int> rewardIds, bool isClaimingAirpod)
    {
        string authToken = APIManager.authToken;

        var jsonBody = JsonConvert.SerializeObject(new { rewardIds });
        byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonBody);

        UnityWebRequest request = new UnityWebRequest(craftUrl, "POST");
        request.SetRequestHeader("accept", "*");
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
        request.SetRequestHeader("Content-Type", "application/json");
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            UnityEngine.Debug.Log("Nhận thưởng thành công: " + request.downloadHandler.text);
            HandleRewardResponse(request.downloadHandler.text);

            if (isClaimingAirpod)
            {
                apWinPopup.SetActive(true);
            }
            else
            {
                ipWinPopup.SetActive(true);
            }

            StartCoroutine(FetchRewards(authToken));
        }
        else
        {
            UnityEngine.Debug.LogError("Lỗi khi nhận thưởng: " + request.error);
            errorPopup.SetActive(true);
        }
    }

    private void HandleRewardResponse(string responseJson)
    {
        // Xử lý phản hồi từ API (nếu cần)
        UnityEngine.Debug.Log("Response: " + responseJson);
    }
}
[System.Serializable]
public class RewardData
{
    [JsonProperty("reward")]
    public Reward reward; // Thông tin về phần thưởng (chứa "value")

    [JsonProperty("name")]
    public string name; // Tên phần thưởng

    [JsonProperty("count")]
    public int count; // Số lượng phần thưởng
}
[Serializable]
public class Reward
{
    [JsonProperty("value")]
    public string value;

    [JsonProperty("id")]
    public int id;
}
[Serializable]
public class FetchResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("data")]
    public List<RewardData> Data { get; set; } // Danh sách phần thưởng
}
