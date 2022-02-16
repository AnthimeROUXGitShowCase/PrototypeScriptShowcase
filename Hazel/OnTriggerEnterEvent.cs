using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

   
    public class OnTriggerEnterEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent actionToDo;
        private void OnTriggerEnter2D(Collider2D other)
        {
            actionToDo.Invoke();
        }

    }
