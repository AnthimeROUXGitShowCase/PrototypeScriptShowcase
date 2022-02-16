using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticSpawner : MonoBehaviour
{
    [SerializeField] private int nutToSpawn;
    [SerializeField] private int nutsPerline;
    [SerializeField] private int line;
    private Transform lastNuts;

    private void OnEnable()
    {
        nutToSpawn = PlayerPrefs.GetInt("globalNuts");
        line = nutToSpawn / nutsPerline;
        StartCoroutine(SpawnDelay());
    }

    private IEnumerator SpawnDelay()
    {
        yield return new WaitForSeconds(0.1f);
        Spawn();
    }

    private void Spawn()
    {
        if (nutToSpawn != 0 )
        {
            for (int y = 0; y < line; y++)
            {
                for (int x = 0; x < nutToSpawn; x++)
                {
                    lastNuts = Pooler.instance.Pop("Nuts").transform;
                    lastNuts.position = gameObject.transform.position + new Vector3(x,y,0);
                }
            }
        }
    }
}
