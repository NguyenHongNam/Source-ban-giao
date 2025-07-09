using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClaimRewardButton : MonoBehaviour
{
    public GameObject buttonInactive;
    public GameObject buttonActive;

    //public void UpdateButtonState(List<Reward> rewards)
    //{
    //    bool isClaimable = true;

    //    // Kiểm tra số lượng mỗi loại mảnh ghép
    //    foreach (var reward in rewards)
    //    {
    //        if (reward.rewardid == "003" && reward.quantity < 1)
    //        {
    //            isClaimable = false;
    //        }
    //        else if (reward.rewardid == "004" && reward.quantity < 1)
    //        {
    //            isClaimable = false;
    //        }
    //        else if (reward.rewardid == "005" && reward.quantity < 1)
    //        {
    //            isClaimable = false;
    //        }
    //    }

    //    buttonActive.SetActive(isClaimable);
    //    buttonInactive.SetActive(!isClaimable);
    //}
}
