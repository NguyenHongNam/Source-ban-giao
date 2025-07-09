using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class APIManager : MonoBehaviour
{
    private string authUrl = "https://apigamifi.mobifoneplus.vn/auth/registerGame";

    public RewardDisplay rewardDisplay;

    public Text totalTurnText;
    public IconManager iconManager;
    public ClaimRewardButton claimRewardButton;
    public Image rewardImageDisplay;
    public AudioSource audioSource;
    public AudioClip rewardMusic;

    public static string authToken;
    public static string tokenSso;
    public static string ctkmId;
    public static int spinTypeNumber;
    public static int totalTurn;

    public RewardFetcher rewardFetcher;
    public BlindbagManager blindbagManager;

    public Text deeplinkText;
    void Start()
    {
        string url = Application.absoluteURL;
        if (deeplinkText != null)
        {
            deeplinkText.text = $"Deeplink URL: {url}";
        }
        tokenSso = GetValueFromUrl("token");
        ctkmId = GetValueFromUrl("ctkm_id");
        //tokenSso = "7891be23-9a1b-4bae-96f6-b8a826af6ec8";
        //ctkmId = "644";
        if (!string.IsNullOrEmpty(tokenSso))
        {
            StartCoroutine(RegisterGame(tokenSso));
        }
        else
        {
            Debug.LogError("SSO Token is missing.");
        }
    }
    string GetValueFromUrl(string key)
    {
        string url = Application.absoluteURL;
        if (url.Contains(key))
        {
            Uri uri = new Uri(url);
            return System.Web.HttpUtility.ParseQueryString(uri.Query).Get(key);
        }
        Debug.LogError($"{key} not found in URL.");
        return null;
    }
    IEnumerator RegisterGame(string tokenSso)
    {
        var jsonBody = JsonConvert.SerializeObject(new { tokenSso });
        UnityWebRequest request = new UnityWebRequest(authUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");
        request.SetRequestHeader("accept", "*");

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            try
            {
                AuthResponse authResponse = JsonConvert.DeserializeObject<AuthResponse>(request.downloadHandler.text);
                if (authResponse != null && authResponse.Data != null)
                {
                    authToken = authResponse.Data.Token;
                    Debug.Log($"Token: {authToken}");
                    StartCoroutine(FetchTotalTurnWithToken(authToken));
                    StartCoroutine(rewardFetcher.FetchRewards(authToken));
                }
                else
                {
                    Debug.LogError("Failed to parse the token from the response.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON response: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Failed to authenticate: {request.error}");
        }
    }

    IEnumerator FetchTotalTurnWithToken(string authToken)
    {
        string url = "https://apigamifi.mobifoneplus.vn/auth/getTotalTurnMmbf";
        UnityWebRequest request = new UnityWebRequest(url, "POST");

        request.SetRequestHeader("accept", "*");
        request.SetRequestHeader("Authorization", $"Bearer {authToken}");
        request.SetRequestHeader("Content-Type", "application/json");

        SpinRequest spinRequest = new SpinRequest
        {
            tokenSso = tokenSso,
            ctkmId = ctkmId,
        };

        string jsonData = JsonUtility.ToJson(spinRequest);
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            string responseJson = request.downloadHandler.text;
            try
            {
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(responseJson);

                if (apiResponse != null && apiResponse.Data != null)
                {
                    blindbagManager.totalTurn = apiResponse.Data.totalturn;
                    blindbagManager.chancesText.text = blindbagManager.totalTurn.ToString();
                    totalTurn = apiResponse.Data.totalturn;
                    Debug.Log("" + totalTurn);
                    if (apiResponse.Data.spinTypeNumber != null)
                    {
                        foreach (var turn in apiResponse.Data.spinTypeNumber)
                        {
                            if (turn.type == 2)
                            {
                                spinTypeNumber = 2;
                                break;
                            }
                        }
                    }
                    else
                    {
                        Debug.LogError("spinTypeNumber is null.");
                    }
                }
                else
                {
                    Debug.LogError("API response or Data is null.");
                }
            }
            catch (Exception ex)
            {
                Debug.LogError($"Error parsing JSON response: {ex.Message}");
            }
        }
        else
        {
            Debug.LogError($"Failed to fetch rewards: {request.error}");
        }
    }
}
[Serializable]
public class ApiResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public turndata Data { get; set; }
}

[Serializable]
public class AuthResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public AuthData Data { get; set; }
}

[Serializable]
public class AuthData
{
    [JsonProperty("token")]
    public string Token { get; set; }
}
[System.Serializable]
public class SpinRequest
{
    public int spinTypeNumber;
    public string tokenSso;
    public string ctkmId;
}
[Serializable]
public class turndata
{
    [JsonProperty("action")]
    public string action { get; set; }

    [JsonProperty("total_turn")]
    public int totalturn { get; set; }

    [JsonProperty("list_turn")]
    public List<SpinType> spinTypeNumber { get; set; }

}
[Serializable]
public class SpinType
{
    [JsonProperty("type")]
    public int type { get; set; }
}
