using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BonusSpawner : MonoBehaviour
{
    [SerializeField] private int nutsToSpawn = 10;
    [SerializeField] private float nutsIntervalle = 0.1f;
    [SerializeField] private Vector2 throwRange = new Vector2(1,10);
    [SerializeField] private Transform placeToSpawn;
    private bool safety;
    
    private Rigidbody2D currentRb;

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (safety == false)
        {
            StartCoroutine(CanonSpawn());
            safety = true;
        }

    }

    private IEnumerator CanonSpawn()
    {
        for (int x = 0; x < nutsToSpawn; x++)
        {
            currentRb = Pooler.instance.Pop("Nuts").GetComponent<Rigidbody2D>();
            currentRb.transform.position = placeToSpawn.position;
            currentRb.AddForce(Vector2.left * Random.Range(throwRange.x,throwRange.y));
            yield return new WaitForSeconds(nutsIntervalle);
        }
        this.gameObject.SetActive(false);
    }
}
