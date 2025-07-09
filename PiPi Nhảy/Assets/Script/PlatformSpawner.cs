using System.Collections.Generic;
using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    public List<GameObject> platformPrefabs;
    public float minDistance = 3f;
    public float maxDistance = 5f;
    public int maxPlatforms = 10;
    public CameraFollow cameraFollow;

    private Vector3 lastPlatformPosition;
    private List<GameObject> spawnedPlatforms = new List<GameObject>();
    public Transform lastLandingPoint;

    void Start()
    {
        lastPlatformPosition = transform.position;
    }

    public void SpawnNextPlatform()
    {
        GameObject selectedPrefab = platformPrefabs[Random.Range(0, platformPrefabs.Count)];
        float randomDistance = Random.Range(minDistance, maxDistance);

        Vector3 newPosition = lastPlatformPosition + new Vector3(randomDistance, 0, 0);

        GameObject newPlatform = Instantiate(selectedPrefab, newPosition, Quaternion.identity);
        spawnedPlatforms.Add(newPlatform);

        lastPlatformPosition = newPosition;
        lastLandingPoint = newPlatform.transform;

        CleanupPlatforms();
    }

    private void CleanupPlatforms()
    {
        if (spawnedPlatforms.Count > maxPlatforms)
        {
            GameObject oldPlatform = spawnedPlatforms[0];
            spawnedPlatforms.RemoveAt(0);
            Destroy(oldPlatform);
        }
    }
}
