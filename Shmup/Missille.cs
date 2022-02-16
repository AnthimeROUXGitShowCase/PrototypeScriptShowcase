using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missille : MonoBehaviour
{
    private Transform playerPos;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    void Start()
    {
        playerPos = Player_Movement.instance.transform;
    }

    private void FixedUpdate()
    {
        transform.LookAt(new Vector3(playerPos.position.x, transform.position.y, playerPos.position.z));
        rb.velocity = transform.forward * speed;
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            ScoreManager.instance.AddScore(5);
            Pooler.instance.DePop("EMissile",gameObject);
        }
        if (other.name == "Player")
        {
            Pooler.instance.DePop("EMissile",gameObject);
        }
    }
}
