using Newtonsoft.Json;
using System.Collections.Generic;
using System;

[Serializable]
public class RewardItem
{
    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("quantity")]
    public int Quantity { get; set; }
}

[Serializable]
public class RewardApiResponse
{
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("code")]
    public int Code { get; set; }

    [JsonProperty("data")]
    public List<RewardItem> Data { get; set; }
}



