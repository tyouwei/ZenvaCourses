using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Base : MonoBehaviour
{
    public float health;
    public void Damage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        Debug.Log("A base died!");
    }
    private void Awake()
    {
        health = 3;
    }
}
