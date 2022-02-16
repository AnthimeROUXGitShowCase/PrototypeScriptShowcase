using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    public CharacterController2D controller;
    public float default_speed;
    public float speed;
   
    public Vector2 MoveAxis;

    public Dino_Interact dinoAtRange;

    public Bottle bottleAtRange;
    private bool haveABottle;
    public Sprite NextBottleSprite;
    public Vector2 Aim;
    public GameObject bottleThrowPrefab;
    public Animator animator;

    public bool isSommersaulting;
    private Vector2 SommerSaultDirection;
    [SerializeField] private float sommerSaultDecreasingSpeed;
    [SerializeField] private float somerSaultSpeed; 
   
 

    public void OnMove(InputAction.CallbackContext value)
    {
        if (!isSommersaulting)
        {
            MoveAxis = value.ReadValue<Vector2>();
        }
        else
        {
            MoveAxis = new Vector2(0, 0);   
        }

 
    }

    public void Interact(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (dinoAtRange != null)
            {
                dinoAtRange.InteractDino();
            }

            if ((bottleAtRange != null) && (haveABottle == false))
            {
                haveABottle = true;
                NextBottleSprite = bottleAtRange.BottleSprite.sprite; 
                bottleAtRange.gameObject.SetActive(false);
            }
        }
 
    }

    public void Sommersaults(InputAction.CallbackContext ctx)
    {
        if ((ctx.performed) && (dinoAtRange == null) && (!isSommersaulting) && (MoveAxis != new Vector2(0,0))) 
        {
            isSommersaulting = true;
            SommerSaultDirection = MoveAxis;
            speed = somerSaultSpeed; 
        }
    }

    public void Throw(InputAction.CallbackContext ctx)
    {
        if ((haveABottle == true) && (ctx.performed) && (Aim != null))
        {
            GameObject newBottle = Instantiate(bottleThrowPrefab);
            newBottle.transform.position = this.gameObject.transform.position;
            newBottle.GetComponent<Bottle>().IsThrown = true;
            newBottle.GetComponent<Bottle>().direction = Aim;
            haveABottle = false; 
        }
    }

    public void OnAim(InputAction.CallbackContext value)
    {
        Aim = value.ReadValue<Vector2>();
    }


    public void FixedUpdate()
    {
        controller.Move(MoveAxis.x * speed , MoveAxis.y * speed);
        AnimController();
        
        if (isSommersaulting)
        {
            SommerSault();
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Dino_Interact>())
        {
            dinoAtRange = collision.GetComponent<Dino_Interact>();
        }

        if (collision.GetComponent<Bottle>())
        {
            bottleAtRange = collision.GetComponent<Bottle>();
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        if (other.GetComponent<Dino_Interact>())
        {
            dinoAtRange = null;
        }

        if (other.GetComponent<Bottle>())
        {
            bottleAtRange = null;
        }
    }

    private void SommerSault()
    {
        if (speed > 0)
        {
            controller.Move(SommerSaultDirection.x * speed, SommerSaultDirection.y * speed);
            speed -= sommerSaultDecreasingSpeed;
        }
        else
        {
            isSommersaulting = false;
            speed = default_speed;
        }
       
    }

    private void AnimController()
    {
        if (Math.Abs(MoveAxis.x) > Mathf.Abs(MoveAxis.y))
        {
            animator.SetBool("Up", false);
            animator.SetBool("Down", false);

            if (MoveAxis.x > 0)
            {
                animator.SetBool("Right", true);
                animator.SetBool("Left", false);
            }
            else
            {
                animator.SetBool("Left", true);
                animator.SetBool("Right", false);
            }
        }

        if (Math.Abs(MoveAxis.x) < Mathf.Abs(MoveAxis.y))
        {
            animator.SetBool("Left", false);
            animator.SetBool("Right", false);

            if (MoveAxis.y > 0)
            {
                animator.SetBool("Up", true);
                animator.SetBool("Down", false);
            }
            else
            {
                animator.SetBool("Down", true);
                animator.SetBool("Up", false);
            }
        }
        
    }
}
