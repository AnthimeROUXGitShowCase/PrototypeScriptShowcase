using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    private Vector3 force;
    [SerializeField] private float strength = 200;
    [SerializeField] private int damage;
    [SerializeField] private float playerMultiplier = 2.5f;

    private void OnEnable()
    {
        StartCoroutine(Disabler());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bounce(other);
    }

    void Bounce(Collider2D other)
    {
        if (other.gameObject.GetComponent<Rigidbody2D>())
        {
            force = (other.transform.position - transform.position) * strength;
            force.z = 0;
            if (other.gameObject.GetComponent<PlayerController>())
            {
                other.GetComponent<Rigidbody2D>().AddForce(force *playerMultiplier);
            }
            else
            {
                other.GetComponent<Rigidbody2D>().AddForce(force);
            }
        
         
        }

        if (other.gameObject.GetComponent<Nuts>())
        {
            other.gameObject.GetComponent<Nuts>().hp -= damage;
        }

        if (other.gameObject.GetComponent<PlayerHp>())
        {
            other.gameObject.GetComponent<PlayerHp>().AddRemoveHp(-damage);
        }
    }

    private IEnumerator Disabler()
    {
        yield return new WaitForSeconds(0.5f);
        Pooler.instance.DePop("ExplosionRadius",gameObject);
    }
}
