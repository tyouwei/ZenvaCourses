using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    public TMPro.TextMeshProUGUI problemText;
    public TMPro.TextMeshProUGUI[] answerText;
    public Image timeDial;
    private float timeDialRate;
    public TMPro.TextMeshProUGUI endText;
    string operatorText = "";

    public static UI instance;
    void Awake()
    {
        // set the instance
        instance = this;
    }
    private void Start()
    {
        timeDialRate = 1.0f / GameManager.instance.timePerProblem;
    }
    void Update()
    {
        timeDial.fillAmount = timeDialRate * GameManager.instance.remainingTime;
    }
    public void SetProblemText(Problem problem)
    {
        switch (problem.operation)
        {
            case Problem.MathsOperation.Addition:
                operatorText = " + ";
                break;
            case Problem.MathsOperation.Subtraction:
                operatorText = " - ";
                break;
            case Problem.MathsOperation.Multiplication:
                operatorText = " x ";
                break;
            case Problem.MathsOperation.Division:
                operatorText = " ÷ ";
                break;
        }
        problemText.text = problem.firstNumber + operatorText + problem.secondNumber;
        for (int i = 0; i < answerText.Length; i++)
        {
            answerText[i].text = problem.answers[i].ToString();
        }
    }
    public void SetEndText(bool win)
    {
        //endText.enabled = true;
        endText.gameObject.SetActive(true);
        if (win)
        {
            endText.text= "You Win!";
            endText.color = Color.green;
        }
        else
        {
            endText.text = "Game Over!";
            endText.color = Color.red;
        }
    }
}
