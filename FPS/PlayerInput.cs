using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private RotationController rotationController;
    [SerializeField] private MoveController moveController;
    
    void Update()
    {
        rotationController.inputRotationX = Input.GetAxis("Mouse X");
        rotationController.inputRotationY = Input.GetAxis("Mouse Y");
        moveController.moveInput.x = Input.GetAxis("Vertical");
        moveController.moveInput.y = Input.GetAxis("Horizontal");
    }
}
