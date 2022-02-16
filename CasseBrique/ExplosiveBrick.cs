using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBrick : Brick
{
    [SerializeField] private SphereCollider sphereTrigger;

    public override void BrickBoot()
    {
        hp = 1;
    }
    public override void Die()
    {
        sphereTrigger.enabled = true;
        StartCoroutine(waitForDeath());
     
    }

    public override void ColorSwitch()
    {
        
    }
    
    public override void Damage()
    {
        hp--;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Brick>())
        {
            other.GetComponent<Brick>().Damage();
        }
    }

    public IEnumerator waitForDeath()
    {
        yield return new WaitForSeconds(0.1f);
        Pooler.instance.DePop("ExplosiveBrick", this.gameObject);
    }
}
