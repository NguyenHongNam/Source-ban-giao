using UnityEngine;

public class DifficultyScaler : MonoBehaviour
{
    public Spawner spawner;
    public float difficultyIncreaseRate = 10f;
    private float timer;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= difficultyIncreaseRate)
        {
            spawner.spawnRate = Mathf.Max(0.5f, spawner.spawnRate - 0.1f); // Giảm spawnRate
            timer = 0;
        }
    }
}
