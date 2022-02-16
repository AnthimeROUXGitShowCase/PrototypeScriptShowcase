using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Nuts : Interactable
{
    public int hp = 3;
    public string key = "Nuts";
    [SerializeField] private int hpMax = 3;
    [SerializeField] private float timeBeforeDisapearing = 30f;
    public Sprite[] nutsCostume;
    public SpriteRenderer spriteRenderer;
    private void OnCollisionEnter2D(Collision2D other)
    {
       TouchingGround(other);
    }

    public void TouchingGround(Collision2D other) 
    {
        if (other.gameObject.CompareTag("Ground") &&  (hp > 0))
        {
            if (hp == 1)
            {
                hp = 0;
                DeathChecker();
            }
            else
            {
                hp--;
                spriteRenderer.sprite = nutsCostume[hp-1];
            }
     
      
        }
    }

    private void OnEnable()
    {
        hp = hpMax;
        StartCoroutine(NutTimer());
        spriteRenderer.sprite = nutsCostume[hp-1];
    }

    private void LateUpdate()
    {
      DeathChecker();
    }
    

    public void DeathChecker()
    {
        if (hp == 0)
        {
            StopCoroutine(NutTimer());
            Pooler.instance.DePop(key, gameObject);
            hp = hpMax;
            LevelManager.instance.noNutsInLVL--;
        }
    }
    

    private IEnumerator NutTimer()
    {
        yield return new WaitForSeconds(timeBeforeDisapearing);
        Pooler.instance.DePop(key, gameObject);
        hp = hpMax;
    }

    public virtual void Grabbed()
    {
    }
}
