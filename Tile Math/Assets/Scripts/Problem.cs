using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class Problem
{
    public enum MathsOperation
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }
    public float firstNumber;
    public float secondNumber;
    public MathsOperation operation;
    public float[] answers;
    public int correctTube;
}
