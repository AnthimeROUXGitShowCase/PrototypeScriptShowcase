using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallSpawner : MonoBehaviour
{

    [SerializeField] private int noBallToSpawn;
    [SerializeField] private GameObject ballToSpawn;
    [SerializeField] private float stunTimer;
    [SerializeField] private float stunTimerMax;
    [SerializeField] private float spawnIntervall;
    [SerializeField] KeyCode key;
    [SerializeField] private bool hasABall;
    [SerializeField] private Transform spawnLocation;
    private Ball ball;

    void OnTriggerEnter(Collider other)
    {
        if ((other.GetComponent<Ball>())) //&& (GameManager.currentBall == 1) && (!hasABall))
        {
            ball = other.GetComponent<Ball>();
            ball.enabled = false;
            stunTimer = stunTimerMax;
            hasABall = true;
        }
    }

    private void Update()
    {
        if ((stunTimer > 0f) && (hasABall))
        {
            stunTimer -= Time.deltaTime; 

            if (Input.GetKeyDown(key))
            {
                noBallToSpawn++;
            }

            
        }
        else if (stunTimer < 0f)
        {
            ball.enabled = true;
            StartCoroutine(BallSpawn());
            hasABall = false;
            
        }
    }

    private IEnumerator BallSpawn()
    {
        
        if (noBallToSpawn > 0)
        {
            Instantiate(ballToSpawn, spawnLocation.position, spawnLocation.rotation);
            noBallToSpawn--;
        }
        else
        {
            StopAllCoroutines();
        }

        yield return new WaitForSeconds(spawnIntervall);
    }

}
