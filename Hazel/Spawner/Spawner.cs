using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;


public class Spawner : MonoBehaviour
{
    private Rigidbody2D lastObjSpawnedRB;
    private Transform lastObjSpawnedtransform;
    [SerializeField] private Vector2 nutsFrequency;
    public bool spawnPattern;
    public bool differentTypeOfObj;
    [HideInInspector]
    public List<string> keys;
    
    private int currentNutNo;
    [HideInInspector]
    public string key;
    [HideInInspector]
    public bool blast;
    [HideInInspector]
    public float blastForce;
    [HideInInspector]
    public Vector2 blastDirection;



    private void Start()
    {
        StartCoroutine(NutsSpawn());
    }
    

    private IEnumerator NutsSpawn()
    {
        Debug.Log(gameObject.name);
        yield return new WaitForSeconds(Random.Range(nutsFrequency.x, nutsFrequency.y));
        
        if (differentTypeOfObj)
        {

            lastObjSpawnedtransform = Pooler.instance.Pop(keys[currentNutNo]).transform;
            if (spawnPattern == false)
            {
                lastObjSpawnedRB = lastObjSpawnedtransform.GetComponent<Rigidbody2D>();
            }
            
            Pooler.instance.Pop(keys[currentNutNo]);

            currentNutNo++;
            if (currentNutNo > keys.Count)
            {
                currentNutNo = 0;
            }
        }
        else
        {
            lastObjSpawnedtransform = Pooler.instance.Pop(key).transform;
            if (spawnPattern == false)
            {
                lastObjSpawnedRB = lastObjSpawnedtransform.GetComponent<Rigidbody2D>();
            }
        }
            
        if (blast)
        {
            lastObjSpawnedRB.AddForce(blastDirection*blastForce);
        }

        if (spawnPattern == false)
        {
            lastObjSpawnedRB.position = transform.position;
        }

        StartCoroutine(NutsSpawn());
    }
}


