using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ice : MonoBehaviour
{
    private PlayerController player;

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            player = other.gameObject.GetComponent<PlayerController>();
            player.isOnIce = true;

        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.name == "Player")
        {
            player.OnUnSlow();
            player.isOnIce = false;
            player = null;
        }
    }
}
