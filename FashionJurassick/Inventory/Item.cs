using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New_Item", menuName = "Inventory/Item")]
public class Item : ScriptableObject
{

    new public string name = "Item";
    public Sprite icon = null;
    public float relax;
    public float sexy;
    public float classe; 
    public float cute;
    public float rich;

  
}
