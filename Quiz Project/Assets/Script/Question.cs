using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public enum QuestionType{Text = 0, Image = 1, Audio = 2}
    public QuestionType questionType;
    public string textQuestion;
    public Sprite imageQuestion;
    public AudioClip audioQuestion;
    public string correctAnswer;
    public string[] answerChoices;
    
}

