using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class GameManager : MonoBehaviour
{
    public Problem[] problems;
    public int curProblem;
    public float timePerProblem;
    public float remainingTime;
    public PlayerController player;

    public static GameManager instance;
    void Awake()
    {
        // set the instance
        instance = this;
    }
    private void Start()
    {
        SetProblem(0);
    }
    void SetProblem(int problem)
    {
        curProblem = problem;
        remainingTime = timePerProblem;
        UI.instance.SetProblemText(problems[curProblem]);

    }
    void Update()
    {
        remainingTime -= Time.deltaTime;
        if (remainingTime <= 0)
        {
            Lose();
        }
    }
    void Lose()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(false);
    }
    void Win()
    {
        Time.timeScale = 0.0f;
        UI.instance.SetEndText(true);
    }
    public void OnPlayerEnterTube(int tube)
    {
        if (tube == problems[curProblem].correctTube)
        {
            CorrectAnswer();
        }
        else
        {
            IncorrectAnswer();
        }

    }
    void CorrectAnswer()
    {
        // is this the last problem?
        if (problems.Length - 1 == curProblem)
        {
            Win();
        }
        else
        {
            SetProblem(curProblem + 1);
        }
    }
    void IncorrectAnswer()
    {
        player.Stun();
    }
}
