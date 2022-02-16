using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BonusBrick : Brick
{
    [SerializeField] private GameObject bonus;

    public override void BrickBoot()
    {
        hp = 1;
    }
    
    public override void Die()
    {
        Instantiate(bonus, transform.position, Quaternion.identity, null);
        Pooler.instance.DePop("BonusBrick", gameObject);
    }
    
    public override void Damage()
    {
        hp--;
    }
}
