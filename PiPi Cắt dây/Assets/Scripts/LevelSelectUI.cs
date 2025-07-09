using UnityEngine;
using UnityEngine.UI;

public class LevelSelectUI : MonoBehaviour
{
    [SerializeField] private Button[] levelButtons;
    [SerializeField] private GameObject levelSelectPanel;
    [SerializeField] private GameObject levelSelectButton; // nút "Chọn màn" trên UI

    void Start()
    {
        for (int i = 0; i < levelButtons.Length; i++)
        {
            int levelIndex = i;
            levelButtons[i].onClick.AddListener(() =>
            {
                SceneController.instance.LoadLevelDirect(levelIndex);
                if (levelSelectPanel != null)
                    levelSelectPanel.SetActive(false);

                if (levelSelectButton != null)
                    levelSelectButton.SetActive(true); // hiện nút "Chọn màn" sau khi vào game
            });
        }

        if (levelSelectButton != null)
        {
            // Gán hành động cho nút "Chọn màn"
            levelSelectButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                if (levelSelectPanel != null)
                    levelSelectPanel.SetActive(true);
            });
        }
    }
}
