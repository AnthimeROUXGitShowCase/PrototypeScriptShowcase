using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;

public class Shoot : MonoBehaviour
{

    
    [SerializeField] private Transform bulletSpawn;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Animator animator;
    [SerializeField] private bool automatic;
    [SerializeField] private float bulletSpeed = 1000;
    public TextMeshProUGUI textCurrentAmmo;
    public int currentAmmo = 50;
    private void Start()
    {
      //public Gun[0] = new Gun[1];
      textCurrentAmmo.text = currentAmmo.ToString();
    }

    void Update()
    {
        if (automatic)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                Fire();
            }
        }
        

        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            Aim();
        }

        if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            UnAim();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            SwitchFireMode();
        }
    }

    private void Fire()
    {
        if (currentAmmo > 0)
        {
            GameObject bullet = Pooler.instance.Pop("Missile");
            bullet.transform.position = bulletSpawn.transform.position;
            bullet.GetComponent<Rigidbody>().velocity = Vector3.zero;
    
            currentAmmo--;
            textCurrentAmmo.text = currentAmmo.ToString();
            if (animator.GetBool("Aim"))
            {
                bullet.GetComponent<Rigidbody>().AddForce(bulletSpawn.forward*bulletSpeed);
            }
            else if (!animator.GetBool("Aim"))
            {
                bullet.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward*bulletSpeed);
            }
        }
    }

    private void SwitchFireMode()
    {
        automatic = !automatic;
    }

    private void Aim()
    {
        animator.SetBool("Aim", true);
    }
    private void UnAim()
    {
        animator.SetBool("Aim", false);
    }
}
