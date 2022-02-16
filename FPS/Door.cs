using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour, IUsable
{
    [SerializeField] private Animation anim;
    

    public void OpenDoor()
    {
        anim.Play();
    }



    public void Use()
    {
        OpenDoor();
    }
}
