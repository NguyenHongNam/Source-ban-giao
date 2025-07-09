using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Animator animator; // Animator của Panel

    public string slideInTrigger = "SlideIn";
    public string slideOutTrigger = "SlideOut";

    // Kích hoạt Animation Slide-In
    public void ShowPanel()
    {
        if (animator != null)
        {
            animator.SetTrigger(slideInTrigger);
        }
    }

    // Kích hoạt Animation Slide-Out
    public void HidePanel()
    {
        if (animator != null)
        {
            animator.SetTrigger(slideOutTrigger);
        }
    }
}
