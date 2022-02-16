using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PatternSpawner : MonoBehaviour
{
    public bool hasADelay;
    public bool hasDifferentnutsType;
    public bool isThrown;
    [HideInInspector] public float delaySpawn;


    [HideInInspector] public Vector2 throwingDirection;
    [HideInInspector] public float throwingForce;
    public Transform[] nutsPlacement;


    [HideInInspector] public List<string> typesOfNuts;
    [HideInInspector] public string nutsType;
    private Transform lastNuts;

    private void Start()
    {
        StartCoroutine(Spawn());
    }
    

    IEnumerator Spawn()
    {
        Debug.Log(gameObject.name);
        for (int x = 0; x < nutsPlacement.Length; x++)
        {
            if (hasDifferentnutsType)
            {
                lastNuts = Pooler.instance.Pop(typesOfNuts[x]).transform;
            }
            else
            {
                lastNuts = Pooler.instance.Pop(nutsType).transform;
            }

            lastNuts.transform.position = nutsPlacement[x].position;
            if (isThrown)
            {
                lastNuts.GetComponent<Rigidbody2D>().AddForce(throwingDirection * throwingForce);
            }

            if (hasADelay)
            {
                yield return new WaitForSeconds(delaySpawn);
            }

        }
    }
}


