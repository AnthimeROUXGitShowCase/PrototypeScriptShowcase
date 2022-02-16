using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Movement : MonoBehaviour
{
    public static Player_Movement instance;
    private Vector2 movement;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;

    private void Start()
    {
        instance = this;
    }

    public void OnMovement(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            movement = ctx.ReadValue<Vector2>();
        }

        if (ctx.canceled)
        {
            movement = Vector2.zero;
        }
    }

    private void FixedUpdate()
    {
        move();
    }

    void move()
    {
        rb.velocity = new Vector3(movement.x ,0,movement.y)*speed;
    }
}
