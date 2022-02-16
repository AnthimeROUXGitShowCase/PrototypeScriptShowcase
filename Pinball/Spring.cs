using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    [SerializeField] private float minSize;
    [SerializeField] private float maxSize;
    [SerializeField] private float sizeSpeed;
    [SerializeField] private float releaseSpeed;
    [SerializeField] private KeyCode key;
    [SerializeField] private float force;
    [SerializeField] private float forceGain = 1f;
    [SerializeField] private float forceMax = 100;
    [SerializeField] private Rigidbody ball;
    public bool released;
    private float releasedTime;

    void Update()
    {
        if (Input.GetKey(key))
        {
            if (transform.localScale.x > minSize)
            {
                transform.localScale -= new Vector3(sizeSpeed, 0,0);
                
                if (force < forceMax)
                {
                    force += forceGain;
                }
            }
        }
        else if (transform.localScale.x < maxSize)
        {
            transform.localScale += new Vector3(releaseSpeed, 0, 0);
        }

        if (Input.GetKeyUp(key))
        {

            if ((ball != null))
            {
                ball.AddForce(new Vector3(-force, 0,0), ForceMode.Impulse); 
                releasedTime = 3f;
                released = true;
            }
            force = 0;
            ball = null;
            
        }

        if (released)
        {
            if (releasedTime > 0f)
            {
                releasedTime -= Time.deltaTime;
            }
            else
            {
                released = false;
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.GetComponent<Rigidbody>())
        {
            ball = other.gameObject.GetComponent<Rigidbody>();
        }
    }

}
