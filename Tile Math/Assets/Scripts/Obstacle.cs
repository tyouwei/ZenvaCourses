using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public Vector3 moveDir;
    public float moveSpeed;
    public float rotateSpeed;
    public float aliveTime = 4.0f;

    private void OnEnable()
    {
        CancelInvoke("Deactivate");
        Invoke("Deactivate", aliveTime);
    }
    void Deactivate()
    {
        gameObject.SetActive(false);
    }
    private void Update()
    {
        transform.position += moveDir * moveSpeed * Time.deltaTime;
        transform.Rotate(Vector3.back * moveDir.x * rotateSpeed * Time.deltaTime);
    }
}
