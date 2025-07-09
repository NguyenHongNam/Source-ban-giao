using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class QuestionManager : MonoBehaviour
{
    [Header("UI References")]
    public Text questionText; // Text hiển thị câu hỏi
    public Button[] answerButtons; // Các Button cho đáp án
    public GameObject congratulationPopup;

    [Header("Question Data")]
    public QuestionData questionData; // Dữ liệu câu hỏi

    private Question currentQuestion; // Câu hỏi hiện tại
    private int correctAnswerIndex; // Đáp án đúng

    [Header("Audio Settings")]
    public AudioSource audioSource; // AudioSource để phát âm thanh
    public AudioClip correctAnswerClip; // Âm thanh trả lời đúng
    public AudioClip wrongAnswerClip;

    public MissionManager missionManager;
    public GameObject questionPanel;
    void Start()
    {
        // Hiển thị một câu hỏi ngẫu nhiên khi bắt đầu
        ShowRandomQuestion();
    }

    public void ShowRandomQuestion()
    {
        // Lấy ngẫu nhiên một câu hỏi từ dữ liệu
        int randomIndex = Random.Range(0, questionData.questions.Length);
        currentQuestion = questionData.questions[randomIndex];

        // Hiển thị câu hỏi và đáp án
        questionText.text = currentQuestion.questionText;
        for (int i = 0; i < answerButtons.Length; i++)
        {
            answerButtons[i].GetComponentInChildren<Text>().text = currentQuestion.answers[i];
            int answerIndex = i; // Lưu chỉ số của đáp án để dùng trong sự kiện
            answerButtons[i].onClick.RemoveAllListeners(); // Xóa các sự kiện cũ
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(answerIndex));
        }

        // Ghi nhớ đáp án đúng
        correctAnswerIndex = currentQuestion.correctAnswerIndex;
    }

    void OnAnswerSelected(int selectedIndex)
    {
        if (selectedIndex == correctAnswerIndex)
        {
            Debug.Log("Bạn đã trả lời đúng!");
            if (correctAnswerClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(correctAnswerClip);
                missionManager.CompleteMission(2);
            }
            ShowCongratulationPopup();
            questionPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Bạn đã trả lời sai!");
            if (correctAnswerClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(wrongAnswerClip);
            }
            StartCoroutine(ShakeButton(answerButtons[selectedIndex].GetComponent<RectTransform>()));
        }
    }
    IEnumerator ShakeButton(RectTransform buttonTransform)
    {
        Vector3 originalPosition = buttonTransform.localPosition;
        float elapsedTime = 0f;
        float duration = 0.5f; // Thời gian rung lắc
        float magnitude = 10f; // Độ mạnh của rung lắc

        while (elapsedTime < duration)
        {
            float offsetX = Random.Range(-1f, 1f) * magnitude;
            float offsetY = Random.Range(-1f, 1f) * magnitude;
            buttonTransform.localPosition = originalPosition + new Vector3(offsetX, offsetY, 0);

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Trả về vị trí ban đầu
        buttonTransform.localPosition = originalPosition;
    }

    void ShowCongratulationPopup()
    {
        // Hiển thị popup chúc mừng
        congratulationPopup.SetActive(true);
    }
}
