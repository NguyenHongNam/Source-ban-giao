using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField] Animator transitionAnimation;
    [SerializeField] private GameObject[] levelPrefabs; // Danh sách các prefab level
    [SerializeField] private Transform levelSpawnPoint; // Vị trí spawn level
    [SerializeField] private TextMeshProUGUI levelText;

    private int currentLevelIndex = 0;
    private GameObject currentLevel;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Nếu muốn dùng trong nhiều scene
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadNextLevel()
    {
        currentLevelIndex++;

        if (currentLevelIndex >= levelPrefabs.Length)
        {
            Debug.Log("Completed last level. Restarting from level 1.");
            currentLevelIndex = 0; // Quay lại level đầu tiên
        }

        StartCoroutine(LoadLevelWithTransition(currentLevelIndex));
    }
    public void LoadLevelDirect(int index)
    {
        if (index >= 0 && index < levelPrefabs.Length)
        {
            currentLevelIndex = index;
            StartCoroutine(LoadLevelWithTransition(index));
        }
        else
        {
            Debug.LogWarning("Invalid level index!");
        }
    }
    public void RestartLevel()
    {
        StartCoroutine(LoadLevelWithTransition(currentLevelIndex));
    }

    IEnumerator LoadLevelWithTransition(int levelIndex)
    {

        transitionAnimation.SetTrigger("EndTransition");
        yield return new WaitForSeconds(1f);

        Debug.Log("Destroying current level");
        if (currentLevel != null)
            Destroy(currentLevel);

        Debug.Log("Loading level " + levelIndex);
        LoadLevel(levelIndex);

        transitionAnimation.SetTrigger("StartTransition");
    }

    private void LoadLevel(int index)
    {
        currentLevel = Instantiate(levelPrefabs[index], levelSpawnPoint.position, Quaternion.identity);
        if (levelText != null)
            levelText.text = $"Màn {index + 1}";
    }
    public int LevelCount => levelPrefabs.Length;
}
