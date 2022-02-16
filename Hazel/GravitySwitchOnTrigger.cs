using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitchOnTrigger : MonoBehaviour
{
    [SerializeField] private float gravityModifier = -1;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Rigidbody2D>())
        {
            other.GetComponent<Rigidbody2D>().gravityScale = gravityModifier;
        }
    }
}