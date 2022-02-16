using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollision : MonoBehaviour
{
     public EnemyAi myEnemy;


    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log(other.name);
        if ((other.name == "--PlayerLastKnownPosition--") && (myEnemy.alarmManager.Alert) && (!myEnemy.fov.playerSeen))
        {
            myEnemy.lookThisWay.position = myEnemy.playerLastKnownPosition.position;
            myEnemy.PLKP = true;
        }

        if ((other.transform == myEnemy.patrolWaypoint[myEnemy.currentWaypoint]) && (myEnemy.alarmManager.Alert == false))
        {
            myEnemy.currentWaypoint = myEnemy.currentWaypoint + 1;
            myEnemy.target = myEnemy.patrolWaypoint[myEnemy.currentWaypoint];
            myEnemy.destination.target = myEnemy.target;
        }
    }
}
