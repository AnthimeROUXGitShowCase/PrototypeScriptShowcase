using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bumper : MonoBehaviour
{
    private Vector3 force;
    [SerializeField] private float strength = 200;

    private void OnCollisionEnter2D(Collision2D other)
    {
        Bounce(other);
    }

    void Bounce(Collision2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            force = (other.transform.position - transform.position) * strength;
            force.z = 0;
            other.rigidbody.AddForce(force);
        }
    }
}
