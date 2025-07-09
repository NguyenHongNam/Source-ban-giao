using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // Câu hỏi
    public string[] answers = new string[4]; // 4 đáp án
    public int correctAnswerIndex; // Chỉ số của đáp án đúng (0, 1, 2, 3)
}

[CreateAssetMenu(fileName = "QuestionData", menuName = "Quiz/QuestionData", order = 1)]
public class QuestionData : ScriptableObject
{
    public Question[] questions; // Danh sách các câu hỏi
}
