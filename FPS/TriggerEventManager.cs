using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerEventManager : MonoBehaviour
{
    public UnityEvent onTriggerEnterEvent;
    public UnityEvent onTriggerExitEvent;
    public void OnTriggerEnter(Collider other)
    {
        onTriggerEnterEvent.Invoke();
    }

    public void OnTriggerExit(Collider other)
    {
        onTriggerExitEvent.Invoke();
    }
}
