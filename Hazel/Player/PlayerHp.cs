using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : MonoBehaviour
{
    [SerializeField] private int hp;

    private void Start()
    {
        LevelManager.instance.HpTextUpdater(hp);
    }

    public void AddRemoveHp(int hpChange)
    {
        hp += hpChange;
        LevelManager.instance.HpTextUpdater(hp);
        if (hp < 1)
        {
            LevelManager.instance.Lose();
        }
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Danger"))
        {
            AddRemoveHp(-1);
        }
    }
}
