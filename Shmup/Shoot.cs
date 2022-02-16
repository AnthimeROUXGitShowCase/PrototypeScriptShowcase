using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class Shoot : MonoBehaviour
{
    delegate void Weapon();
    private Weapon currentWeapon;
    [SerializeField]private int baseWeaponLvl = 1;
    [SerializeField] private int shotgunWeaponLvl = 1;
    [SerializeField] private Transform[] baseWeaponSpawnPosition;
    [SerializeField] private Transform[] shotgunSpawnPosition;
    private GameObject lastBullet;
    [SerializeField] private TextMeshProUGUI currentWeaponTxt;
    
    

    public void OnShoot(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            currentWeapon.Invoke();
        }
    }
    private void OnEnable()
    {
        currentWeapon = BaseWeapon;
        currentWeaponTxt.text = "Base Weapon";
    }
    void BaseWeapon()
    {
 
        if (baseWeaponLvl == 1)
        {
            lastBullet = Pooler.instance.Pop("BasicBullet");
            lastBullet.transform.position = baseWeaponSpawnPosition[0].position;
        }
        if (baseWeaponLvl == 2)
        {
            for (int x = 0; x < 3; x++)
            {
                lastBullet = Pooler.instance.Pop("BasicBullet");
                lastBullet.transform.rotation = baseWeaponSpawnPosition[x].rotation;
                lastBullet.transform.position = baseWeaponSpawnPosition[x].position;

            }
        }
        if (baseWeaponLvl == 3)
        {
            for (int x = 0; x < 5; x++)
            {
                lastBullet = Pooler.instance.Pop("BasicBullet");
                lastBullet.transform.rotation = baseWeaponSpawnPosition[x].rotation;
                lastBullet.transform.position = baseWeaponSpawnPosition[x].position;
            }
        }
    }
 

    void ShotgunWeapon()
    {
        if (shotgunWeaponLvl == 1)
        {
            for (int x = 0; x < 3; x++)
            {
                lastBullet = Pooler.instance.Pop("BasicBullet");
                lastBullet.transform.rotation = shotgunSpawnPosition[x].rotation;
                lastBullet.transform.position = shotgunSpawnPosition[x].position;

            }
        }
        if (shotgunWeaponLvl == 2)
        {
            for (int x = 0; x < 5; x++)
            {
                lastBullet = Pooler.instance.Pop("BasicBullet");
                lastBullet.transform.rotation = shotgunSpawnPosition[x].rotation;
                lastBullet.transform.position = shotgunSpawnPosition[x].position;

            }
        }
        if (shotgunWeaponLvl == 3)
        {
            for (int x = 0; x < 7; x++)
            {
                lastBullet = Pooler.instance.Pop("BasicBullet");
                lastBullet.transform.rotation = shotgunSpawnPosition[x].rotation;
                lastBullet.transform.position = shotgunSpawnPosition[x].position;

            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bonus>())
        {
            ScoreManager.instance.AddScore(50);
            if (other.GetComponent<Bonus>().isANewWeapon)
            {
                if (currentWeapon == BaseWeapon)
                {
                    currentWeapon = ShotgunWeapon;
                    shotgunWeaponLvl = 1;
                    currentWeaponTxt.text = "Shotgun Weapon lvl" + shotgunWeaponLvl.ToString();
                }
                else
                {
                    currentWeapon = BaseWeapon;
                    baseWeaponLvl = 1;
                    currentWeaponTxt.text = "base weapon lvl" + baseWeaponLvl.ToString();
                }
            }
            else
            {
                if (currentWeapon == BaseWeapon)
                {
                    if (baseWeaponLvl < 4)
                    {
                        baseWeaponLvl++;
                        currentWeaponTxt.text = "base weapon lvl" + baseWeaponLvl.ToString();
                    }
              
                }
                if  (currentWeapon == ShotgunWeapon)
                {
                    if (baseWeaponLvl < 4)
                    {
                        shotgunWeaponLvl++;
                        currentWeaponTxt.text = "Shotgun Weapon lvl" + shotgunWeaponLvl.ToString();
                    }
            
                }

            }
            Pooler.instance.DePop("Bonus", other.gameObject);
        }
    }
}
