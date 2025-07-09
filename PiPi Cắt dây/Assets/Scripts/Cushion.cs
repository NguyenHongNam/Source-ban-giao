using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cushion : MonoBehaviour 
{
    public GameObject windZone; // GameObject có AreaEffector2D

    void Start()
    {
        if (windZone != null)
            windZone.SetActive(false); // tắt vùng gió ban đầu
    }

    private void OnMouseDown()
    {
        Debug.Log("Cushion activated!");
        if (windZone != null)
        {
            StartCoroutine(ActivateWind());
            AudioManager.instance.PlayWindMachine();
        }
    }

    private IEnumerator ActivateWind()
    {
        windZone.SetActive(true);
        yield return new WaitForSeconds(0.3f); // thời gian gió thổi (có thể điều chỉnh)
        windZone.SetActive(false);
    }
}
