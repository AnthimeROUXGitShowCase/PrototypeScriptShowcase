using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody rb;

    
    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }

    private void OnEnable()
    {
        StartCoroutine(SelfDestruction());
    }

    private void OnDisable()
    {
        StopCoroutine(SelfDestruction());
    }

    private void OnTriggerEnter(Collider other)
    {
        Disable();
    }

    private void OnCollisionEnter(Collision other)
    {
        Disable();
    }

    private void Disable()
    {
        Pooler.instance.DePop("BasicBullet",gameObject);
    }
    private IEnumerator SelfDestruction()
    {
        yield return new WaitForSeconds(15f);
        Disable();
    }
}
