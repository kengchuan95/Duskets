using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject enemyType;
    private GameObject spawner;
    void Start()
    {
        spawnEnemies();
    }
    private void spawnEnemies()
    {

        var spawnChance = new System.Random().Next(1, 13);
        var spawnRange = new System.Random().Next(1, 9);
        if (spawnRange <= spawnChance)
        {
            Instantiate(enemyType, transform.position, Quaternion.identity);
        }
    }
}
