using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchLight : MonoBehaviour
{
    [SerializeField] Light[] lights;
    [SerializeField] private Spring spring;
    private bool lighOff;
    private float timer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Ball>() && (spring.released))
        {
            foreach (Light light in lights)
            {
                light.enabled = true;
                
            }

            lighOff = true;
            timer = 3f;
        }
    }

    private void Update()
    {
        if (lighOff)
        {
            if (timer > 0f)
            {
                timer -= Time.deltaTime;
            }
            else 
            {
                foreach (Light light in lights)
                {
                    light.enabled = false;
                
                }

                lighOff = false;
            }
        }
    }
}
    