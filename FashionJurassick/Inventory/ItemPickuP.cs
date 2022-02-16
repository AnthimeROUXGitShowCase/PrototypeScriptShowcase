using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ItemPickuP : MonoBehaviour
{
    public Item item;
    private Inventory inventory;

    public void start()
    {
        inventory = GameObject.Find("Player").GetComponent<Inventory>();
    }
    public void PickUp()
    {
        bool wasPickedUp = Inventory.instance.Add(item);

        if (wasPickedUp)
        {
            Destroy(gameObject);
        }
        
    }
}
