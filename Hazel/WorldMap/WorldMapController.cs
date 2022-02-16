using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WorldMapController : MonoBehaviour
{
    private Vector2 vec2Move;
    [SerializeField] private float speed;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float lvlCheckerDistance;
    [SerializeField] private LvlWorldMap overLvl;
    
    public void Move(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            vec2Move = ctx.ReadValue<Vector2>();
        }

        if (ctx.canceled)
        {
            vec2Move = Vector2.zero;
        }
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if ((ctx.performed) && (overLvl != null))
        {
            overLvl.GoToLvl();
        }
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<LvlWorldMap>())
        {
            overLvl = other.GetComponent<LvlWorldMap>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<LvlWorldMap>())
        {
            overLvl = null;
        }
    }

    private void FixedUpdate()
    {
        rb.velocity = vec2Move*speed;
    }
}
