using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.UI;

public class MoveController : MonoBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private Rigidbody rb;
    [SerializeField] public Vector2 moveInput;
    private float gravity = -10f;
    [SerializeField] private float jumpStrength = 50f;
   void Update()
   {
       rb.velocity = transform.forward * moveInput.x *speed + transform.right * moveInput.y * speed + new Vector3(0,rb.velocity.y,0);

       if (Input.GetKeyDown(KeyCode.Space))
       {
           rb.AddForce(Vector3.up * jumpStrength, ForceMode.Impulse);
       }
   }
}
