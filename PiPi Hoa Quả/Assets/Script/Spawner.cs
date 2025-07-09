using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject[] items;                     // Fruit, Bomb, Shield, Heart

    [Header("Spawn timing")]
    public float spawnRate = 1f;                 // Khoảng thời gian giữa 2 đợt
    public float spawnRangeX = 2f;                 // Biên X của vùng spawn
    public int minPerBurst = 2;                  // Số vật tối thiểu mỗi đợt
    public int maxPerBurst = 4;                  // Số vật tối đa mỗi đợt

    [Header("Difficulty")]
    public float bombSpawnChance = 0.1f;           // % bom rơi ở điểm < 50
    public GameManager gameManager;

    [Header("Overlap")]
    public float minSpacing = 0.7f;                // Khoảng cách an toàn giữa hai vật
    public int maxReattempts = 10;               // Số lần thử đổi vị trí trước khi bỏ

    private float timer;
    private void Start()
    {
        StartCoroutine(SpawnLoop());
    }
    /* -------------------------------------------- */
    /* ----------------- UPDATE ------------------- */
    private void Update()
    {
        UpdateDifficulty();
    }
    IEnumerator SpawnLoop()
    {
        while (true)
        {
            SpawnItem();

            float delay = UnityEngine.Random.Range(spawnRate * 0.6f, spawnRate * 1.4f); // ngẫu nhiên delay
            yield return new WaitForSeconds(delay);
        }
    }

    /* -------------------------------------------- */
    /* ------------- SPAWN BURST ------------------ */
    void SpawnItem()
    {
        if (gameManager == null) return;

        int itemsToSpawn = Random.Range(1, 4); // Spawn 1 đến 3 vật thể mỗi lần

        for (int i = 0; i < itemsToSpawn; i++)
        {
            GameObject itemToSpawn;
            float random = Random.value;

            if (random < bombSpawnChance)
            {
                itemToSpawn = GetRandomItemByTag("Bomb");
            }
            else
            {
                itemToSpawn = GetRandomItemByTag("Fruit", "Shield", "Heart");
            }

            if (itemToSpawn != null)
            {
                Vector3 spawnPosition = new Vector3(Random.Range(-spawnRangeX, spawnRangeX), 6f, 0f);
                Instantiate(itemToSpawn, spawnPosition, Quaternion.identity);
            }
        }
    }

    /* -------------------------------------------- */
    /* ------------ CHỌN PREFAB ------------------- */
    GameObject ChoosePrefab()
    {
        float r = UnityEngine.Random.value;
        if (r < bombSpawnChance)
            return GetRandomItemByTag("Bomb");
        return GetRandomItemByTag("Fruit", "Shield", "Heart");
    }

    /* -------------------------------------------- */
    /* --------- ĐIỀU CHỈNH ĐỘ KHÓ ---------------- */
    void UpdateDifficulty()
    {
        int score = gameManager.score;

        if (score >= 300) { bombSpawnChance = 0.50f; spawnRate = 0.40f; }
        else if (score >= 200) { bombSpawnChance = 0.40f; spawnRate = 0.50f; }
        else if (score >= 100) { bombSpawnChance = 0.30f; spawnRate = 0.60f; }
        else if (score >= 50) { bombSpawnChance = 0.20f; spawnRate = 0.70f; }
        else { bombSpawnChance = 0.10f; spawnRate = 1.00f; }
    }

    /* -------------------------------------------- */
    /* ------ LỌC PREFAB THEO TAG ----------------- */
    GameObject GetRandomItemByTag(params string[] tags)
    {
        List<GameObject> list = new();
        foreach (GameObject obj in items)
        {
            foreach (string t in tags)
            {
                if (obj.CompareTag(t)) { list.Add(obj); break; }
            }
        }
        return list.Count == 0 ? null : list[UnityEngine.Random.Range(0, list.Count)];
    }
}
