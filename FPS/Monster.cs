using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Random = UnityEngine.Random;

public class Monster : MonoBehaviour
{
    public Rigidbody rb;
    public float speed;

    public  float distanceWithCurrentWaypoint;
    public Transform currentWaypoint;
    public bool chaseMode;
    public Transform player;
    public int hp = 3;
    public RoundManager roundManager;
    public AudioClip[] MonsterSound;
    public AudioSource soundPlayer;

 

    public virtual void OnEnable()
    {
        chaseMode = false;
        player = null;
        roundManager = GameObject.Find("--RoundManager--").GetComponent<RoundManager>();
        hp = 3;
    }

    public void Update()
    {
        if (chaseMode)
        {
            Chase();
        }
        else
        {
            Patrol();
        }

        if (hp <= 0)
        {
            Died();
         
        }
    }

    public void Patrol()
    {
        distanceWithCurrentWaypoint = Vector3.Distance(currentWaypoint.position, transform.position);
        if (distanceWithCurrentWaypoint < 1.5f)
        {
            player = GameObject.Find("Player").transform;
            chaseMode = true;
        }
        else
        {
            transform.LookAt(new Vector3(currentWaypoint.position.x,transform.position.y, currentWaypoint.position.z));
            rb.velocity = (transform.forward*speed)+ new Vector3(0,rb.velocity.y,0);
        }

    }

    public void Chase()
    {
        transform.LookAt(new Vector3(player.position.x,transform.position.y, player.position.z));
        rb.velocity = transform.forward * speed + new Vector3(0,rb.velocity.y,0);
        distanceWithCurrentWaypoint = Vector3.Distance(player.position, transform.position);
    }
    

    public void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.name == "Bullet(Clone)")
        {
            hp--;
        }
    }
    
    public virtual void Died()
    {
        roundManager.currentZombie--;
        Pooler.instance.DePop("Zombie", this.gameObject);
    }
}
