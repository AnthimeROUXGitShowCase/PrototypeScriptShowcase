using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [SerializeField] private Transform raycastOrigin;
    [SerializeField] private float raycastDistance = 5f;


    void Update()
    {
        OnInteract();
    }

    private void OnInteract()
    {
        RaycastHit hit;
        if (Physics.Raycast(raycastOrigin.position,raycastOrigin.forward, out hit, raycastDistance)) 
        {
            if (hit.collider != null)
            {
                if (hit.collider.gameObject.GetComponent<IUsable>() != null)
                {
                    if (Input.GetKeyDown(KeyCode.E))
                    {
                        hit.collider.GetComponent<IUsable>().Use();
                    }
                }
            }
        }
    }
}
