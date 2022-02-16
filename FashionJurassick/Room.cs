using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float Width;
    public float Height;
    public bool playerIsInRoom;

    public float camSpeed = 1000f;
    public Vector2 cameraMax;
    public Vector2 cameraMin;
    public Vector3 playerChange;
    private CameraController cam;
    public bool tinyRoom;
    public bool playerSpotted;
    public EnemyAi[] enemyInRoom;
    public RoomAlarmManager alarmManager;
 

  
    public void Start()
    {
        this.gameObject.AddComponent<BoxCollider2D>();
        gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
        gameObject.GetComponent<BoxCollider2D>().size = new Vector2(Width, Height);

        cam = Camera.main.GetComponent<CameraController>();
    }
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, new Vector3(Width, Height, 0));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name == "Player") && (!tinyRoom))
        {
 
            cam.minPosition = cameraMin;
            cam.maxPosition = cameraMax;
            cam.target = collision.transform;
            collision.transform.position += playerChange;
  
        }

        if ((collision.name == "Player") && (tinyRoom))
        {
          
            cam.target = this.gameObject.transform;
            
        }


    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerIsInRoom = true;
            //cam.transform.position = Vector3.MoveTowards(cam.transform.position, transform.position, Time.deltaTime * camSpeed);
        }
 
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "Player")
        {
            playerIsInRoom = false;
        }
    }

}
