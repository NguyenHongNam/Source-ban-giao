using Newtonsoft.Json;
using System.Collections.Generic;
using UnityEngine;

public class UserTurn
{
    [System.Serializable]
    public class TurnData
    {
        public string phone;
        public string sub_id;
        public string fullname;
        public string checksum;

        [JsonProperty("total_turn")]
        public int total_turn { get; set; }

        public List<TurnDetail> list_turn;
    }

    [System.Serializable]
    public class TurnDetail
    {
        public int id;
        public string create_date;
        public string action;
        public int type;
        public int total;
    }
}
