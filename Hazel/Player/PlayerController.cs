using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Diagnostics;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance;
    [SerializeField] private Rigidbody2D rb;
    private Vector2 move;
    [SerializeField] private float jumpForce;

    [SerializeField] private float freezeTime = 3;
    [SerializeField] private float speed;
    [SerializeField] private float speedOnIce =20f;
    [SerializeField] private float normalSpeed;
    [SerializeField] private float speedWhenSlowed = 5;
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private bool isGrounded;
    private bool isGroundedL;
    private bool isGroundedR;
    public  bool isOnLadder;
    public bool isOnIce;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private Transform groundCheck1;
    [SerializeField] private Transform groundCheck2;
   
    [SerializeField] private float normalGravityScale;

    private void Awake()
    {
        instance = this;
    }

    public void OnMove(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            move = ctx.ReadValue<Vector2>();
            playerAnimator.SetBool("Move",true);
        }
        else
        {
            move = Vector2.zero;
            playerAnimator.SetBool("Move",false);
        }
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if ((ctx.performed) && (isGrounded))
        {
            rb.AddForce(Vector2.up*jumpForce);
            playerAnimator.SetBool("Jump",true);
        }
    }
    
    void FixedUpdate()
    {
        Move();
        
        isGroundedL = Physics2D.OverlapCircle(groundCheck1.position, groundCheckDistance);
        isGroundedR = Physics2D.OverlapCircle(groundCheck2.position, groundCheckDistance);
        isGrounded = isGroundedL || isGroundedR;
        if (isGrounded)
        {
            playerAnimator.SetBool("Jump",false);
        }

    }

    private void Move()
    {
        if (rb.velocity.x < 0)
        {
            playerSprite.flipX = false;
        }

        if (rb.velocity.x > 0)
        {
            playerSprite.flipX = true;
        }

        if (isOnLadder)
        {
            rb.velocity = (move*speed);
            isGrounded = true;
            rb.gravityScale = 0;
        }
        else
        {
            if (isOnIce)
            {
                rb.drag = 2f;
                rb.AddForce(new Vector2(move.x * speedOnIce, rb.velocity.y));
            }
            else
            {
                rb.drag = 0;
                rb.gravityScale = normalGravityScale;
                rb.velocity = (Vector2.right * (move.x*speed) + new Vector2(0, rb.velocity.y));
            }
        }
    }

    public void OnSlow()
    {
        speed = speedWhenSlowed;
    }

    public void OnUnSlow()
    {
        speed = normalSpeed;
    }

    public IEnumerator DeSlow()
    {
        yield return new WaitForSeconds(freezeTime);
        speed = normalSpeed;
    }
}
