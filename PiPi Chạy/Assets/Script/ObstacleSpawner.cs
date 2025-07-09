using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnRate = 1f;
    public Transform[] spawnPoints;

    void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), 0, spawnRate);
    }

    void SpawnObstacle()
    {
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Instantiate(obstaclePrefab, spawnPoints[randomIndex].position, Quaternion.identity);
    }
}
