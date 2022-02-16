using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PredatorMissill : MonoBehaviour
{
    [SerializeField] private GameObject predatorMissil;


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            predatorMissil.SetActive(true);
        }
    }
}
