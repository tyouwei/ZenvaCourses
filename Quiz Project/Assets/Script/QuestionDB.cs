using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Questons", menuName = "Create Questions", order = 100)]

public class QuestionDB : ScriptableObject{
    public QuestionSet[] questionSets;
    public QuestionSet GetQuestionSet(int level){
        foreach (QuestionSet questionSet in questionSets)
        {
            if (questionSet.level == level)
            {
                return questionSet;
            }
        }
        return new QuestionSet();
    }
}

[System.Serializable]
public struct QuestionSet{
    public int level;
    public List<Question> questions;
}
