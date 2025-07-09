using UnityEngine;

public class ObstacleSpawnner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 2f;
    public float minHeight = -1.5f;
    public float maxHeight = 1.5f;

    private float timer = 0;
    private bool gameStarted = false;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        if (!gameStarted && GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().enabled)
        {
            // Check if game started
            if (GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>().gravityScale > 0)
            {
                gameStarted = true;
            }
        }

        if (!gameStarted) return;

        timer += Time.deltaTime;
        if (timer >= spawnRate)
        {
            SpawnObstacle();
            timer = 0;
        }
    }

    void SpawnObstacle()
    {
        GameObject obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity);
        obstacle.transform.position = new Vector3(transform.position.x, Random.Range(minHeight, maxHeight), 0);
    }
}
