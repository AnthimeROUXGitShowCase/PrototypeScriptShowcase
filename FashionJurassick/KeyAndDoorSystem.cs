using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyAndDoorSystem : MonoBehaviour
{

    public GameObject door;
    public bool playerHasKey;



    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.name == "Player")
        {
            playerHasKey = true;
            gameObject.SetActive(false);
        }
     
    }



}
