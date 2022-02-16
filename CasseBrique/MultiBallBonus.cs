using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiBallBonus : MonoBehaviour, IBonus
{
    
    public void PickedUp()
    {
        GameManager.instance.NewBall();
        Destroy(gameObject);
    }
}
