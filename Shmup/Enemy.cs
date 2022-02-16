using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    [SerializeField] private Transform bulletSpawnPos;
    private GameObject lastBullet;
    public String nameOfEnemy;
    [SerializeField] private String nameOfBullet;
    [SerializeField] private float bulletFequency;
    public float score;
    public Rigidbody rb;

    public virtual void OnEnable()
    {
        StartCoroutine(Shoot());
    }

    private void FixedUpdate()
    {
        Movement();
    }

    public virtual void Movement()
    {
        rb.velocity = new Vector3(0 ,0,-speed);
    }
    
    public virtual void Died()
    {
        ScoreManager.instance.AddScore(score);
        StopAllCoroutines();
        Pooler.instance.DePop(nameOfEnemy,gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Bullet")) || (other.gameObject.name == "Player"))
        {
            Died();
        }
    }

    public IEnumerator Shoot()
    {
        yield return new WaitForSeconds(bulletFequency);
        lastBullet = Pooler.instance.Pop(nameOfBullet);
        lastBullet.transform.position = bulletSpawnPos.position;
    }
}
