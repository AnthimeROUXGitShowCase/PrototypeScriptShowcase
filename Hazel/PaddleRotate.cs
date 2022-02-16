using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleRotate : MonoBehaviour
{
    [SerializeField] private float rotateFactor = 1;
    [SerializeField] private float waitFactor = 1;
    [SerializeField] private bool way;

    private void Start()
    {
        StartCoroutine(Switcher());
    }

    void FixedUpdate()
    {
        if (way)
        {
            transform.Rotate(new Vector3(0,0,rotateFactor));
        }
        else if (way == false)
        {
            transform.Rotate(new Vector3(0,0,-rotateFactor));
        }
       
    }

    private IEnumerator Switcher()
    {
        yield return new WaitForSeconds(waitFactor);
        way = !way;
        StartCoroutine(Switcher());

    }
}
