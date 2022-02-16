using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class SpawnerBlaster : MonoBehaviour
{
    [SerializeField] private Vector2 direction;
    [SerializeField] private float force;
    [SerializeField] private bool randomTimeIntervalle;
    [SerializeField] private Vector2 randomIntervalle;
    [SerializeField] private float regularIntervalle;
    [SerializeField] private string keyToPop;
    private Rigidbody2D lastObj;

    private void OnEnable()
    {
        StartCoroutine(SpawnerStart());
    }

    public IEnumerator SpawnerStart()
    {
        if (randomTimeIntervalle)
        {
            yield return new WaitForSeconds(Random.Range(randomIntervalle.x, randomIntervalle.y));
        }
        
        if (!randomTimeIntervalle)
        {
            yield return new WaitForSeconds(regularIntervalle);
        }
        lastObj = Pooler.instance.Pop(keyToPop).GetComponent<Rigidbody2D>();
        lastObj.transform.position = gameObject.transform.position;
        lastObj.AddForce(direction*force,ForceMode2D.Impulse);
        StartCoroutine(SpawnerStart());
    }
}
