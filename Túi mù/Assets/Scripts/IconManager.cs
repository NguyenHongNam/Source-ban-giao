using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconManager : MonoBehaviour
{
    public Text airPod1;
    public Text airPod2;
    public Text airPod3;

    public Text iPhone1;
    public Text iPhone2;
    public Text iPhone3;

    //public void UpdateIcons(List<Reward> rewards)
    //{
    //    // Reset số lượng mảnh
    //    airPod1.text = "0";
    //    airPod2.text = "0";
    //    airPod3.text = "0";
    //    iPhone1.text = "0";
    //    iPhone2.text = "0";
    //    iPhone3.text = "0";

    //    // Duyệt qua danh sách quà và cập nhật số lượng
    //    foreach (var reward in rewards)
    //    {
    //        if (reward.rewardid == "003")
    //        {
    //            airPod1.text = reward.quantity.ToString();
    //        }
    //        else if (reward.rewardid == "004")
    //        {
    //            airPod2.text = reward.quantity.ToString();
    //        }
    //        else if (reward.rewardid == "005")
    //        {
    //            airPod3.text = reward.quantity.ToString();
    //        }
    //        else if (reward.rewardid == "006")
    //        {
    //            iPhone1.text = reward.quantity.ToString();
    //        }
    //        else if (reward.rewardid == "007")
    //        {
    //            iPhone2.text = reward.quantity.ToString();
    //        }
    //        else if (reward.rewardid == "008")
    //        {
    //            iPhone3.text = reward.quantity.ToString();
    //        }
    //    }
    //}
}
