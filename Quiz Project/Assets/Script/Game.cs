using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public QuestionDB questionDatabase;
    private int level;
    private QuestionSet currentQuestionSet;
    private Question currentQuestion;
    private int currentQuestionIndex;
    [SerializeField]
    private Transform questionPanel;
    [SerializeField]
    private Transform answerPanel;
    private int correctAnswers;
    [SerializeField]
    private Transform scoreScreen, questionScreen;
    [SerializeField]
    private TMPro.TextMeshProUGUI scoreStats, scorePercentage;

    void LoadQuestionSet()
    {
        currentQuestionSet = questionDatabase.GetQuestionSet(level);
        currentQuestion = currentQuestionSet.questions[0];
    }
    public void LoadNextQuestionSet()
    {
        if(level < questionDatabase.questionSets.Length-1)
        {
            correctAnswers = 0;
            currentQuestionIndex = 0;
            level++;
            PlayerPrefs.SetInt("level", level);
            scoreScreen.gameObject.SetActive(false);
            questionScreen.gameObject.SetActive(true);
            LoadQuestionSet();
            UseQuestionTemplate(currentQuestion.questionType);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void LoadNextQuestion()
    {
        if(currentQuestionIndex < currentQuestionSet.questions.Count - 1)
        {
            currentQuestionIndex++;
            currentQuestion = currentQuestionSet.questions[currentQuestionIndex];
            UseQuestionTemplate(currentQuestion.questionType);
        }
        else
        {
            scoreScreen.gameObject.SetActive(true);
            questionScreen.gameObject.SetActive(false);
            scorePercentage.text = string.Format("Score: {0}%", ((float)correctAnswers / (float)currentQuestionSet.questions.Count)*100);
            scoreStats.text = string.Format("Questions: {0}\nCorrect Answers: {1}", currentQuestionSet.questions.Count, correctAnswers);
        }
    }
    void ClearAnswer()
    {
        foreach(Transform buttons in answerPanel)
        {
            Destroy(buttons.gameObject);
        }
    }
    public void CheckAnswer(string answer)
    {
        if (answer == currentQuestion.correctAnswer)
        {
            correctAnswers++;
            Debug.Log("That's correct!");
        }
        ClearAnswer();
        LoadNextQuestion();
    }
    void UseQuestionTemplate(Question.QuestionType questionType)
    {
        for (int i = 0; i < questionPanel.childCount; i++)
        {
            questionPanel.GetChild(i).gameObject.SetActive(i == (int)questionType);
            if (i == (int)questionType)
            {
                questionPanel.GetChild(i).gameObject.GetComponent<QuestionUI>().UpdateQuestionInfo(currentQuestion);
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        level = PlayerPrefs.GetInt("level", 0);
        LoadQuestionSet();
        UseQuestionTemplate(currentQuestion.questionType);
    }
}
