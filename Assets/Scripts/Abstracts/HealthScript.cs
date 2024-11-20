using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HealthScript : MonoBehaviour
{
    public int currentHealth;
    public int maxHealth;
    public bool isLive = true;

    private System.Random rand = new System.Random();

    public void Awake()
    {
        currentHealth = 10 + rand.Next(1, 11);
        maxHealth = currentHealth;
        isLive = true;
    }

    public void Damaged(int damage)
    {
        currentHealth -= damage;
        //clamp player health
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        //update UI
        isLive = (currentHealth > 0);
    }
}
