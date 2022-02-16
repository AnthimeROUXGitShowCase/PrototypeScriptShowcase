using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public class BrickManager : MonoBehaviour
{
    public static BrickManager instance;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Vector2 rowAndColumns;
    [SerializeField] private float offsetX;
    [SerializeField] private float offsetY;
    private int roulette;
    private GameObject brick;
    public int currentNoBrick;
    private bool firstSpawn;

    private void Start()
    {
        SpawnLVL();
        firstSpawn = true;
        instance = this;
    }

    //private void LateUpdate()
    //{
    //    if ((currentNoBrick < 1) && (firstSpawn))
    //    {
    //        SpawnLVL();
    //    }
    //}

    public void SpawnLVL()
    {
        for (int x = 0; x < rowAndColumns.x; x++)
        {
            for (int y = 0; y < rowAndColumns.y; y++)
            {
                currentNoBrick++;
                roulette = Random.Range(1, 6);
                if (roulette == 1)
                {
                    brick = Pooler.instance.Pop("ExplosiveBrick");
                }
                else if (roulette == 2)
                {
                    brick = Pooler.instance.Pop("BonusBrick");
                }
                else
                {
                    brick = Pooler.instance.Pop("Brick");
                }
       
                brick.transform.position = new Vector3(spawnPoint.position.x + x * offsetX, spawnPoint.position.y - y * offsetY,0);
            }
            
        }
    }
}
