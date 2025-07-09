using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefabs; // Danh sách các prefab đám mây
    public float spawnInterval = 5f; // Thời gian giữa các lần spawn
    public float spawnXMin = -10f; // Giới hạn X nhỏ nhất
    public float spawnXMax = -80f; // Giới hạn X lớn nhất
    public float spawnYMin = 12f; // Giới hạn Y nhỏ nhất
    public float spawnYMax = 70f; // Giới hạn Y lớn nhất
    public float spawnZ = 6f; // Vị trí Z cố định để spawn

    private float timer = 0f;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnCloud();
            timer = 0f;
        }
    }

    void SpawnCloud()
    {
        // Chọn một prefab ngẫu nhiên
        GameObject cloudPrefab = cloudPrefabs[Random.Range(0, cloudPrefabs.Length)];

        // Tạo vị trí ngẫu nhiên theo trục X và Y
        Vector3 spawnPosition = new Vector3(
            Random.Range(spawnXMin, spawnXMax), // Giá trị ngẫu nhiên cho X
            Random.Range(spawnYMin, spawnYMax), // Giá trị ngẫu nhiên cho Y
            spawnZ // Giá trị cố định cho Z
        );

        // Tạo đám mây tại vị trí ngẫu nhiên
        Instantiate(cloudPrefab, spawnPosition, Quaternion.identity);
    }
}
