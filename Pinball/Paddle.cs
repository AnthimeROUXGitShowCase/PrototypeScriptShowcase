using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] private float targetPosition = 75;
    [SerializeField] private float originPosition = 0;

    [SerializeField] private HingeJoint hingeJoint;
    private JointSpring jointSpring;
    [SerializeField] private KeyCode key;

    private void Start()
    {
        jointSpring = hingeJoint.spring;
    }

    private void Update()
    {

        if (Input.GetKey(key))
        {
            jointSpring.targetPosition = targetPosition;
        }
        else
        {
            jointSpring.targetPosition = originPosition;
        }

        hingeJoint.spring = jointSpring;
    }
}
