using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    public SpriteRenderer BottleSprite;
    public bool IsThrown;
    public Vector2 direction;
    private Vector2 breakPosition;
    public EnemyAi[] EnemyHearingBottle;
    public float throwSpeed;



    private void Awake()
    {
        BottleSprite = this.gameObject.GetComponent<SpriteRenderer>();
      
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if ((collision.gameObject.name != "Player") && (collision.GetComponent<Room>() == false))
        {
            breakPosition = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y);
            Break();
        }
     
        
    }

    private void Break()
    {
       
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        IsThrown = false;

        Collider2D[] collider2Ds = Physics2D.OverlapCircleAll(breakPosition, 30f);
        foreach(Collider2D go in collider2Ds)
        {
            if (go.GetComponent<EnemyAi>())
            {
                go.GetComponent<EnemyAi>().alarmManager.Schearch = true;
                go.GetComponent<EnemyAi>().target = this.gameObject.transform;
            }
        }


    }

     

    private void Update()
    {
        if (IsThrown)
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            gameObject.GetComponent<CircleCollider2D>().enabled = false;
            transform.Translate(direction * throwSpeed);
        }
    }
}
