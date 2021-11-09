using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bases : MonoBehaviour
{
    [SerializeField]
    private List<Base> bases;
    public int BaseCount
    {
        get
        {
            return bases.Count;
        }
    }
    public Base GetRandomBase()
    {
        for (int i = BaseCount - 1; i >= 0; i--)
        {
            if (bases[i].health <= 0)
            {
                bases.RemoveAt(i);
            }
        }
        return bases[Random.Range(0, BaseCount)];
    }
}
