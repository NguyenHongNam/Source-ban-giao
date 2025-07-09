using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }

    public GameObject[] levelPrefabs;

    public int currentLevelIndex = 1;

    private GameObject currentLevelGO;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        LoadLevel(currentLevelIndex);
    }

    public int GetTargetScore()
    {
        if (currentLevelIndex <= 9)
        {
            return 100 + (currentLevelIndex - 1) * 50;
        }
        else
        {
            return 500 + (currentLevelIndex - 10) * 100;
        }
    }
    public void LoadNextLevel()
    {
        currentLevelIndex++;
        LoadLevel(currentLevelIndex);
    }

    private void LoadLevel(int index)
    {
        if (currentLevelGO != null)
            Destroy(currentLevelGO);

        if (levelPrefabs != null && levelPrefabs.Length >= index)
            currentLevelGO = Instantiate(levelPrefabs[index - 1]);
        else
            Debug.LogWarning("Không tìm thấy prefab cho level " + index);

        if (GameManager.Instance != null)
        {
            GameManager.Instance.RefreshTargetScore(GetTargetScore());
            GameManager.Instance.ResetGame(); // 👉 Reset sau khi load level mới
        }
    }

    public void ReplayGame()
    {
        if (LevelManager.Instance != null)
            LevelManager.Instance.ReloadCurrentLevel();
    }
    public void ReloadCurrentLevel()
    {
        LoadLevel(currentLevelIndex);
    }
}
