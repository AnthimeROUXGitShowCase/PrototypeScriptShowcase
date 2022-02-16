using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class Brick : MonoBehaviour
{
    [SerializeField] public int hp;
    [SerializeField] private Material[] color;
    [SerializeField] private MeshRenderer mesh;

    public void OnEnable()
    {
        BrickBoot();
    }

    public virtual void BrickBoot()
    {
        hp = Random.Range(1, 4);
        ColorSwitch();
    }
    

    private void LateUpdate()
    {
        if (hp < 1f)
        {
            
            Die();
        }
    }

    public virtual void ColorSwitch()
    {
        if (hp > 0)
        {
            mesh.material = color[hp];
        }
    }
    
    public virtual void Die()
    {
        BrickManager.instance.currentNoBrick--;
        Pooler.instance.DePop("Brick", this.gameObject);
    }  
    public virtual void Damage()
    {
        hp--;
        ColorSwitch();
    }
    
}
