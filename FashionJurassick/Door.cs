using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public KeyAndDoorSystem key;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.name == "Player") && (key.playerHasKey))
        {
            this.gameObject.SetActive(false);
        }
    }
}
