using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BonusCargo : Enemy
{
    
    private GameObject popedBonus;
    private int randomResults;
    public override void Movement()
    {
        rb.velocity = new Vector3(-speed ,0,0);
    }
    
    public override void Died()
    {
        ScoreManager.instance.AddScore(score);
        StopAllCoroutines();
        Pooler.instance.DePop(nameOfEnemy,gameObject);
        popedBonus = Pooler.instance.Pop("Bonus");
        popedBonus.transform.position = gameObject.transform.position;
    }
}
