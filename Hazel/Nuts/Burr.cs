using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Burr : Nuts
{
    

    private void OnCollisionEnter2D(Collision2D other)
    {
        TouchingGround(other);
        if (other.transform.GetComponent<PlayerController>())
        {
            hp--;
            spriteRenderer.sprite = nutsCostume[hp];
        }
    }

    
    private void LateUpdate()
    {
        if (hp == 4)
        {
            gameObject.layer = 0;
        }
        else
        {
            gameObject.layer = 6;
        }
        DeathChecker();
    }

}
