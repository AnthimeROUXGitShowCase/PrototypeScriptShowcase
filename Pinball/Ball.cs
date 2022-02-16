using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
   [SerializeField] private Rigidbody rb;
   [SerializeField] private float force;

    public void Awake()
    {
        GameManager.currentBall++;
        if(gameObject.name == "Ball(Clone)")
        {
            this.gameObject.GetComponent<Ball>().enabled = true;
        }
    }

    void FixedUpdate()
   {
      rb.AddForce(Vector3.right * force ,ForceMode.Acceleration);
      
   }
}
