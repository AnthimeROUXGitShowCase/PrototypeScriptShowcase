using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    [SerializeField] private float minSpeed;
    private Vector3 direction;
    private Vector3 lastVelocity;


    private void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    private void Start()
    {
        GameManager.instance.ChangeNoBall(1);
    }

    private void OnEnable()
    {
        rb.velocity = Vector3.zero;
        StartCoroutine(LaunchBallDelay());
    }

    private void OnCollisionEnter(Collision other)
    {
        speed += 0.5f;
        if (other.gameObject.name == "Paddle")
        {
            Bounce((other.contacts[0].normal+new Vector3(0,0,transform.position.z - other.gameObject.transform.position.z)).normalized);
        }
        else
        {
            Bounce(other.contacts[0].normal);
        }
      
        if (other.gameObject.GetComponent<Brick>())
        {
            other.gameObject.GetComponent<Brick>().Damage();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "DeathZone")
        {
            GameManager.instance.ChangeNoBall(-1);
            if (GameManager.instance.currentNoBall < 1)
            {
                GameManager.instance.ChangeNoLife(-1);
            }
            Pooler.instance.DePop("Ball", gameObject);
        }
    }
    
    private void Bounce(Vector3 collision)
    {
        direction = Vector3.Reflect(lastVelocity.normalized, collision);
        rb.velocity = direction * Mathf.Max(speed, minSpeed);
    }

    public IEnumerator LaunchBallDelay()
    {
        yield return new WaitForSeconds(0.1f);
        if (GameManager.instance.currentNoBall > 1)
        {
            rb.velocity = new Vector3(rb.velocity.x, -speed,  rb.velocity.z);
        }
        else
        {
            yield return new WaitForSeconds(3);
            rb.velocity = new Vector3(rb.velocity.x, -speed,  rb.velocity.z);
        }

    }
}
