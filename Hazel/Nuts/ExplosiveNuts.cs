using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveNuts : Nuts
{
    private Transform explsionTransform;
    private void OnDisable()
    {
        explsionTransform = Pooler.instance.Pop("ExplosionRadius").transform;
        explsionTransform.position = gameObject.transform.position;
    }
}
