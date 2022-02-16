using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileEnemy : Enemy
{
    [SerializeField] private float directionSwitchFrequency = 1f;
    private bool direction;
    public override void OnEnable()
    {
        StartCoroutine(DirectionSwitcher());
        StartCoroutine(Shoot());
    }

    public override void Movement()
    {
        if (direction)
        {
            rb.velocity = new Vector3(speed ,0,-speed);
        }
        else
        {
            rb.velocity = new Vector3(-speed ,0,-speed);
        }

    }

    private IEnumerator DirectionSwitcher()
    {
        yield return new WaitForSeconds(directionSwitchFrequency);
        direction = !direction;
        StartCoroutine(DirectionSwitcher());
    }
}
