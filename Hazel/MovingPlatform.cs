using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private float speed;
    private Vector3 direction;
    [SerializeField]private Transform[] waypoints;
    private Transform currentWaypoint;
    private int currentWaypointNo;
    private float distance;
    private float playerMultiplier = 2.5f;

    private void OnEnable()
    {
        ChangeWaypoint();
    }

    private void FixedUpdate()
    {
        transform.Translate(direction*speed);

        distance = Vector3.Distance(currentWaypoint.position, transform.position);

        if (distance < 1)
        {
            if (currentWaypointNo == waypoints.Length-1)
            {
                currentWaypointNo = 0;
                ChangeWaypoint();
            }
            else
            {
                currentWaypointNo++;
                ChangeWaypoint();
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.GetComponent<Rigidbody2D>())
        {
            col.transform.parent = transform;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        other.transform.parent = null;
    }

    private void ChangeWaypoint()
    {
        currentWaypoint = waypoints[currentWaypointNo];
        direction = (currentWaypoint.position- transform.position).normalized;
    }
}
