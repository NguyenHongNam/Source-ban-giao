using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Link : MonoBehaviour
{	
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
	
	
	//void OnCollisionEnter2D(Collision2D collision)
 //   {
 //       if (collision.gameObject.CompareTag("Player"))
 //       {
 //           KeepOnlyOneSibling();
 //           Destroy(this);
 //       }
 //   }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.instance.PlayCutRope();
            KeepOnlyOneSibling();
            Destroy(this.gameObject);
        }
    }
    void KeepOnlyOneSibling()
    {
        // Get all siblings of the current GameObject
        Transform parentTransform = transform.parent;
        if (parentTransform != null)
        {
            int childCount = parentTransform.childCount;

            for (int i = 0; i < childCount; i++)
            {
                Transform child = parentTransform.GetChild(i);

                Destroy(child.gameObject);
            }
        }
        else
        {
            Debug.LogWarning("This GameObject has no parent, cannot delete siblings.");
        }
    }
}
