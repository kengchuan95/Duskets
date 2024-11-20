using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Target : HealthScript
{
    public GameObject target;
    public AudioClip deathSound;
    private GameStateBehaviour state;
    //public static int numberOfEnemies;

    void OnEnable()
    {
        //numberOfEnemies++;
    }

    void OnDisable()
    {
        //numberOfEnemies--;
    }
    void Start()
    {
        var behaviorObject = GameObject.FindGameObjectWithTag("GameController");
        state = behaviorObject.GetComponent<GameStateBehaviour>();
        maxHealth = currentHealth;
        state.incrementEnemies(true);
    }

    void Update()
    {
        if (!isLive)
        {
            AudioSource.PlayClipAtPoint(deathSound, target.transform.position, 1f);
            Destroy(target);
            state.incrementEnemies(false);
            
        }
    }   
}
