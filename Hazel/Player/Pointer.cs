using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pointer : MonoBehaviour
{
    public Vector3 pointer;
    [SerializeField] private Rigidbody2D grabedObjRb;
    [SerializeField] private Interactable grabedObj;
    [SerializeField] private float grabDistance = 1;
    [SerializeField] private float throwForce = 100f;
    [SerializeField] private Transform heavyObjPos;
    [SerializeField] private Transform rotationPointer;
    [SerializeField] private Transform smallObjPos;
    private Vector3 targetDirection;
    private float angle;
    [SerializeField]private bool useMouse;
    [SerializeField] private Collider2D[] objAtRange;
    [SerializeField] private Collider2D objAtRangeClosest;
    [SerializeField] private SpriteRenderer playerSprite;
    [SerializeField] private PlayerController player;
    [SerializeField] private float heavyObjOffset = 1;
    private float distance;
    private float distanceClosest;
    [SerializeField] private bool isMoving;
    [SerializeField] private float wallCheckerForHeavyObj = 2;

    public void OnPointer(InputAction.CallbackContext ctx)
    { 
        if (ctx.performed)
        {
            pointer = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            useMouse = true;
            isMoving = true;
        }

        if (ctx.canceled)
        {
            isMoving = false;
        }
    }

    public void OnRightStick(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            pointer = ctx.ReadValue<Vector2>();
            useMouse = false;
            isMoving = true;
        }
        if (ctx.canceled)
        {
            isMoving = false;
        }
      
    }

    public void OnGrab(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if ((transform.childCount == 1) && (heavyObjPos.childCount == 0))
            {
                grabedObj = null;
                grabedObjRb = null;
            }
            if (grabedObj == null)
            {
                ObjChecker();
                if (objAtRangeClosest != null)
                {
                    grabedObjRb = objAtRangeClosest.GetComponent<Rigidbody2D>();
                    grabedObj = objAtRangeClosest.GetComponent<Interactable>();
                    grabedObjRb.isKinematic = true;
            
                    if (objAtRangeClosest.GetComponent<Interactable>().isSmall)
                    {
                        if (grabedObj.GetComponent<Nuts>())
                        {
                            grabedObj.GetComponent<Nuts>().Grabbed();
                        }
                        grabedObjRb.simulated = false;
                        Debug.Log(grabedObj.name);
                        grabedObj.transform.parent = rotationPointer;
                        grabedObj.transform.position = smallObjPos.position;
                    }
                    else if (objAtRangeClosest.GetComponent<Interactable>().isSmall == false)
                    {
                        grabedObjRb.simulated = true;
                        grabedObj.transform.position = heavyObjPos.transform.position;
                        grabedObj.transform.parent = heavyObjPos.transform;
                    }
                }
            }
            else if (grabedObj.isSmall)
            {
                if (grabedObj.GetComponent<FrozenNuts>())
                {
                    StartCoroutine(player.DeSlow());
                }
                grabedObj.transform.parent = null;
                grabedObjRb.bodyType = RigidbodyType2D.Dynamic;
                grabedObjRb.simulated = true;
                grabedObjRb.angularVelocity = 0;
                grabedObjRb.velocity = Vector2.zero;
                grabedObjRb.AddForce(rotationPointer.right * throwForce,ForceMode2D.Impulse);
                grabedObj = null;
            }
            else if (grabedObj.isSmall == false)
            {
                grabedObjRb.bodyType = RigidbodyType2D.Dynamic;
                grabedObjRb.simulated = true;
             
                if (playerSprite.flipX == false)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.left, wallCheckerForHeavyObj);
                    if (hit.collider == null)
                    {
                        Debug.DrawRay(transform.position,Vector2.left,Color.green,3);
                        grabedObj.transform.position = new Vector3(transform.position.x - heavyObjOffset, gameObject.transform.position.y,grabedObj.transform.position.z);
                    }
                }
                if (playerSprite.flipX)
                {
                    RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right, wallCheckerForHeavyObj);
                    if (hit.collider == null)
                    {
                        Debug.DrawRay(transform.position,Vector2.right,Color.cyan,3);
                        grabedObj.transform.position = new Vector3(transform.position.x + heavyObjOffset, gameObject.transform.position.y,grabedObj.transform.position.z);
                    }
                }
                grabedObj.transform.parent = null;
                grabedObj = null;
                
            }
        }
    }

    private void ObjChecker()
    {
        objAtRangeClosest = null;
        distanceClosest = grabDistance;
        distance = 999;
        Physics2D.OverlapCircleNonAlloc(gameObject.transform.position, grabDistance,objAtRange);
        for (int y = 0; y < objAtRange.Length; y++)
        {
            if (objAtRange[y] == null)
            {   
                return;
            }
            if ((objAtRange[y] != null) && (objAtRange[y].transform.gameObject.layer == 6))
            {
            
                distance = Vector2.Distance(gameObject.transform.position, objAtRange[y].transform.position);
                if (distance < distanceClosest)
                {
                    objAtRangeClosest = objAtRange[y];
                }
            }
        }
    }
    
    private void RotatePointer()
    {
        if (isMoving)
        {
            if (useMouse)
            {
                targetDirection = (transform.position - pointer);
                angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;
                rotationPointer.rotation = Quaternion.Euler(new Vector3(0, 0, (angle - 180)));
                rotationPointer.position = gameObject.transform.position;
                if (grabedObj != null)
                {
                  
              
                }
            }
            else if (useMouse == false)
            {
                angle = Mathf.Atan2(pointer.y, pointer.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(new Vector3(0,0,angle));
                rotationPointer.position = gameObject.transform.position;
            }
        }
    }

    private void GrabedObjChecker()
    {
        if ((grabedObj != null) && (grabedObj.transform.parent == null))
        {
            if (grabedObj.isSmall)
            {
                grabedObj.transform.parent = rotationPointer;
                grabedObj.transform.position = smallObjPos.position;
            }

            if (!grabedObj.isSmall)
            {
                grabedObj.transform.position = heavyObjPos.transform.position;
                grabedObj.transform.parent = heavyObjPos.transform;
            }
        }
    }
    
    private void FixedUpdate()
    {
        GrabedObjChecker();
        RotatePointer();
    }
    
}