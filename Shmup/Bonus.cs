using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Bonus : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private float speed;
    public bool isANewWeapon;
    private int randomSave;
    [SerializeField] private MeshRenderer meshRender;
    [SerializeField] private Material upgradeMat;
    [SerializeField] private Material newWeaponMat;
 
    private void FixedUpdate()
    {
        rb.velocity = new Vector3(0 ,0,-speed);
    }

    private void OnEnable()
    {
        randomSave = Random.Range(0, 2);
        if (randomSave == 0)
        {
            isANewWeapon = false;
        }
        if (randomSave == 1)
        {
            isANewWeapon = true;
        }

        if (isANewWeapon)
        {
            meshRender.material = newWeaponMat;
        }
        else
        {
            meshRender.material = upgradeMat;
        }
    }
}
