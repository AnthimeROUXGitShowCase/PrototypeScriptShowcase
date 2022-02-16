using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Paddle : MonoBehaviour
{

    [SerializeField] private Vector3 mousePos;
    [SerializeField] private Vector3 mouseWorldPos;
    [SerializeField] private Camera cam;



    private void FixedUpdate()
    {
        mousePos = new Vector3(Input.mousePosition.x, 0, 0 - cam.transform.position.z);
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePos);
        transform.position = new Vector3(mouseWorldPos.x, transform.position.y, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<IBonus>() != null)
        {
            other.gameObject.GetComponent<IBonus>().PickedUp();
            Destroy(other);
        }
    }
}
