using Newtonsoft.Json;
using System;
using UnityEngine;
public class Auth : MonoBehaviour
{
    //public static Auth Instance { get; private set; }

    //public string authToken;

    //private void Awake()
    //{
    //    if (Instance == null)
    //    {
    //        Instance = this;
    //        DontDestroyOnLoad(gameObject); // Giữ đối tượng này khi chuyển scene
    //    }
    //    else
    //    {
    //        Destroy(gameObject); // Xóa nếu đã có instance khác
    //    }
    //}

    //// Phương thức để cập nhật token
    //public void SetAuthToken(string token)
    //{
    //    authToken = token;
    //    Debug.Log($"AuthToken updated: {authToken}");
    //}

    //// Phương thức để lấy token
    //public string GetAuthToken()
    //{
    //    return authToken;
    //}
}
[System.Serializable]
public class SpinResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public SpinRewardData Data { get; set; }
}

[System.Serializable]
public class SpinRewardData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("quantity")]
    public string Quantity { get; set; }

    [JsonProperty("holdQuantity")]
    public string HoldQuantity { get; set; }

    [JsonProperty("winningRate")]
    public string WinningRate { get; set; }

    [JsonProperty("initialWinningRate")]
    public string InitialWinningRate { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("winningType")]
    public string WinningType { get; set; }

    [JsonProperty("createdAt")]
    public string CreatedAt { get; set; }

    [JsonProperty("updatedAt")]
    public string UpdatedAt { get; set; }

    [JsonProperty("nameType")]
    public string Name { get; set; }

    [JsonProperty("additionalVoucherData")]
    public AdditionalVoucherData AdditionalVoucherData { get; set; }

    [JsonProperty("turnType")]
    public TurnTypeData TurnTypeData { get; set; }
}
[System.Serializable]
public class AdditionalVoucherData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }
}

public class TurnTypeData
{
    [JsonProperty("id")]
    public int Id { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }
}