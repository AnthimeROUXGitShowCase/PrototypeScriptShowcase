using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinSpawner : MonoBehaviour
{
    [SerializeField] private Transform spawnPosition;
    private Transform lastObjSpawned;
    [SerializeField] private float nutsFrequency;
    [SerializeField] private string key;
    [SerializeField] private int nutsToSpawn;
    [SerializeField] private int spawnedNuts;



    private void OnEnable()
    {
        nutsToSpawn = LevelManager.instance.currentNuts;
        Pooler.instance.DePopAll();
        StartCoroutine(NutsSpawn());
    }
    

    private IEnumerator NutsSpawn()
    {
        if (spawnedNuts < nutsToSpawn)
        {
            yield return new WaitForSeconds(nutsFrequency);
            spawnedNuts++;
            lastObjSpawned = Pooler.instance.Pop(key).transform;
            lastObjSpawned.position = spawnPosition.position;
            StartCoroutine(NutsSpawn());
        }
    }
}
