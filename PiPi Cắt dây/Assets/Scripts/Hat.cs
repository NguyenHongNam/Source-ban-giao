using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hat : MonoBehaviour
{
    private Collider2D myCollider;
    public GameObject destination;

    // Start is called before the first frame update
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

    // This method is called when another collider enters the trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Enters to hat" + other.gameObject.name);
        if (other.name == "Ball")
        {
            teleportToDestination(other.gameObject);
        }
        if (other.name == "Bubble")
        {
            Destroy(other.gameObject);
        }
    }

    void teleportToDestination(GameObject gameObject){
        if(destination != null){
            Vector2 position = destination.transform.position;
		    gameObject.transform.position = position;
        }
	}

    // Update is called once per frame
    void Update()
    {

    }
}
