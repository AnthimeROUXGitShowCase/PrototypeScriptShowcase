using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private Vector2 spawnerFrequency;
    private float wait;
    private int enemyToSpawn;
    private GameObject lastEnemy;
    

    private void OnEnable()
    {
        StartCoroutine(Spawner());
    }

    private IEnumerator Spawner()
    {
        wait = Random.Range(spawnerFrequency.x, spawnerFrequency.y);
        yield return new WaitForSeconds(wait);
        enemyToSpawn = Random.Range(0, 3);
        if (enemyToSpawn == 0)
        {
            lastEnemy = Pooler.instance.Pop("Enemy1");
            lastEnemy.transform.position = new Vector3(Random.Range(-15,15), 0, 23);
        }
        if (enemyToSpawn == 1)
        {
            lastEnemy = Pooler.instance.Pop("Enemy2");
            lastEnemy.transform.position = new Vector3(Random.Range(-15,15), 0, 23);

        }
        if (enemyToSpawn == 2)
        {
            lastEnemy = Pooler.instance.Pop("BonusCargo");
            lastEnemy.transform.position = new Vector3(29, 0, 23);
        }

        StartCoroutine(Spawner());
    }
}
