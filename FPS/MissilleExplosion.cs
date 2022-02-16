using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissilleExplosion : MonoBehaviour
{
    [SerializeField] private float explosionImpulse = 10;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>())
        {
            Debug.Log(other.name);
            other.GetComponent<Rigidbody>().AddForce((transform.position- other.transform.position) * explosionImpulse,ForceMode.Impulse);
        }
    }
}

