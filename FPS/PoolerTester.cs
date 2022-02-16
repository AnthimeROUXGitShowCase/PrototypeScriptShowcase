using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolerTester : MonoBehaviour
{
    private void Start()
    {
        Pooler.instance.Pop("Missile");
    }
}
