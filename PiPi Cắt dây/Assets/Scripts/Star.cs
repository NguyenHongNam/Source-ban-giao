using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Star : MonoBehaviour
{
    public GameObject destination;
    private Collider2D myCollider;

    void Start()
    {
        // Get the Collider2D component attached to the GameObject
        myCollider = GetComponent<Collider2D>();

        // Check if the Collider2D component exists
        if (myCollider == null)
        {
            Debug.LogError("Collider2D not found on GameObject: " + gameObject.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This method is called when another collider enters the trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            AudioManager.instance.PlayCollectStar();
            moveStarToCounter(destination);
        }
    }

    void moveStarToCounter(GameObject gameObject){
		Vector2 position = gameObject.transform.position;
		transform.position = position;
	}
}
