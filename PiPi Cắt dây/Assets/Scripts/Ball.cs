using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{

    private Collider2D myCollider;
    private Renderer objectRenderer;
    private bool isFlyingInBubble = false;
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

        // Get the Renderer component attached to the GameObject
        objectRenderer = GetComponent<Renderer>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the collision involves a GameObject with the name "MainCharacter"
        if (collision.gameObject.name == "MainCharacter")
        {
            myCollider.enabled = false;
            objectRenderer.enabled = false;
            SceneController.instance.LoadNextLevel();
        }

        if (collision.gameObject.name == "Ground" ||
            collision.gameObject.name == "Obstacle")
        {
            SceneController.instance.RestartLevel();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void AttachToBubble()
    {
        isFlyingInBubble = true;
    }

    public void DetachFromBubble()
    {
        isFlyingInBubble = false;
    }
}
