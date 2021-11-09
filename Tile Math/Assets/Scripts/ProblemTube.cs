using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProblemTube : MonoBehaviour
{
    [SerializeField]
    public int tubeId;
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameManager.instance.OnPlayerEnterTube(tubeId);
            Debug.Log(tubeId);
        }
        Debug.Log("test");
    }
}
