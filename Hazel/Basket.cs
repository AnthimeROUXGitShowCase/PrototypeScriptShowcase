using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Basket : Interactable
{
    [SerializeField] private ParticleSystem[] particles;
    [SerializeField] private bool[] particlesReady;



    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Nuts>())
        {
            ParticlesPlay(other);
            LevelManager.instance.AddNuts();
            LevelManager.instance.noNutsInLVL--;
            Pooler.instance.DePop(other.GetComponent<Nuts>().key, other.gameObject);
        }
    }

    private void ParticlesPlay(Collider2D other)
    {
        for (int x = 0; x < particlesReady.Length-1; x++)
        {
            if (particlesReady[x] == false)
            {
                StartCoroutine(ParticlesCD(x));
                particles[x].transform.position = other.gameObject.transform.position;
                particles[x].Play();
                break;
            }
        }
    }

    private IEnumerator ParticlesCD(int particleNo)
    {
        particlesReady[particleNo] = true;
        yield return new WaitForSeconds(0.2f);
        particlesReady[particleNo] = false;
    }
}