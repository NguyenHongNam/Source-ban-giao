using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Spider : MonoBehaviour
{
    public GameObject[] pathPoints;  // Array of gameObjects defining the path
    public float speed = 1f; // The speed of movement
    public float rotationSpeed = 5f; // The speed of rotation
    public float rotationAngle = 90; // The rotation angle
    private int currentPointIndex = 0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GameObject currentPoint = pathPoints[currentPointIndex];

        if (currentPoint != null && currentPointIndex + 1 <= pathPoints.Length)
        {
            /**Rotation logic **/
            // Get the direction from the current object to the target object
            Vector3 rotateDirection = currentPoint.transform.position - transform.position;

            // Calculate the rotation angle in degrees
            float angle = Mathf.Atan2(rotateDirection.y, rotateDirection.x) * Mathf.Rad2Deg + rotationAngle;

            // Rotate the current object to face the target object
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);

            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * rotationSpeed);

            /*Movement logic*/

            // Calculate the direction to the current waypoint
            Vector2 direction = pathPoints[currentPointIndex].transform.position - transform.position;

            // Normalize the direction and move towards the waypoint
            transform.Translate(direction.normalized * speed * Time.deltaTime);

            // Check if the object has reached the current point
            if (Vector2.Distance(transform.position, currentPoint.transform.position) < 0.1f)
            {
                // Move to the next point in the path
                currentPointIndex = (currentPointIndex + 1) % pathPoints.Length;
            }
        }
    }

    // This method is called when another collider enters the trigger collider.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Ball")
        {
            SceneController.instance.RestartLevel();
        }
    }
}
