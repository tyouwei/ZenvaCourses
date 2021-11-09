using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageQuestion : QuestionUI
{
    [SerializeField]
    private TMPro.TextMeshProUGUI questionStringText;
    [SerializeField]
    private Image questionImage;
    public override void UpdateQuestionInfo(Question question)
    {
        questionStringText.text = question.textQuestion;
        questionImage.sprite = question.imageQuestion;
        base.UpdateQuestionInfo(question);
    }
}
