using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;


public class RoundManager : MonoBehaviour
{
    [SerializeField] private int creeperAtThisRound = 1;
    [SerializeField] private int zombieAtThisRound = 5; 
    [SerializeField] public int creeperRemaining = 1;
    [SerializeField] public int zombieRemaining = 5;
    [SerializeField] public int currentCreeper;
    [SerializeField] private GameObject creeper; 
    [SerializeField] public int currentZombie;
    [SerializeField] private GameObject Zombie;
    private Vector3 circleSpawn;
    private int currentRound = 0;
    [SerializeField] private Transform[] spawnLocation;
    [SerializeField] private TextMeshProUGUI currentRoundText;

    private void Start()
    {
        StartCoroutine(SpawnerCheck());
        nextRound();
    }


    private void Update()
    {
       

        if ((creeperRemaining == 0) && (zombieRemaining == 0)&& (currentZombie == 0)&&(currentCreeper == 0))
        {
            nextRound();
        }
    }

    IEnumerator SpawnerCheck()
    {
        yield return new WaitForSeconds(2);
        if (creeperRemaining > 0)
        {
            StartCoroutine(PopACreeper());
        }

        if (zombieRemaining > 0)
        {
            StartCoroutine(PopAZombie());
        }
        StartCoroutine(SpawnerCheck());
    }

    IEnumerator PopACreeper()
    {
        creeperRemaining--;
        yield return new WaitForSeconds(Random.Range(0, 2));
        currentCreeper++;
        creeper = Pooler.instance.Pop("Creeper");
        Transform spawnPos = spawnLocation[Random.Range(0, spawnLocation.Length)];
        creeper.transform.position = spawnPos.position;
        creeper.GetComponent<Creeper>().currentWaypoint = spawnPos.GetChild(0).transform;
    }
    
    IEnumerator PopAZombie()
    {
        zombieRemaining--;
        yield return new WaitForSeconds(Random.Range(0, 2));
        currentZombie++;
        Zombie = Pooler.instance.Pop("Zombie");
        Transform spawnPos = spawnLocation[Random.Range(0, spawnLocation.Length)];
        Zombie.transform.position = spawnPos.position;
        Zombie.GetComponent<Monster>().currentWaypoint = spawnPos.GetChild(0).transform;
        
    }

    private void nextRound()
    {
        currentRound++;
        creeperAtThisRound++;
        zombieAtThisRound += 2;
        zombieRemaining = zombieAtThisRound;
        creeperRemaining = creeperAtThisRound;
        currentRoundText.text = "Round " + currentRound.ToString();
    }
}
