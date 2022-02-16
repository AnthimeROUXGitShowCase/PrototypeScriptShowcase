using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    private float rotationX;
    public float limitX = 75;

    [SerializeField] private float rotationSpeed = 1;
    [SerializeField] private Transform head;

    public float inputRotationX;
    public float inputRotationY;

    void Update()
    {
        rotationX -= Input.GetAxis("Mouse Y")* rotationSpeed;
        rotationX = Mathf.Clamp(rotationX, -limitX, limitX);
        head.localRotation = Quaternion.Euler(rotationX,0,0);
        
        transform.localRotation *= Quaternion.Euler(0,Input.GetAxis("Mouse X") * rotationSpeed,0);
    }
}
