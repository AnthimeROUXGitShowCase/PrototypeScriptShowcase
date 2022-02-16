using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour, IUsable
{
   [SerializeField] private int ammo;
   private Shoot gun;
   
   public void PickUpAmmo()
   {
      gun = GameObject.Find("Gun").GetComponent<Shoot>(); 
      gun.currentAmmo += ammo;
      gun.textCurrentAmmo.text = gun.currentAmmo.ToString();
   }

   public void Use()
   {
      PickUpAmmo();
   }
}
