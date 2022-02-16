using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon;
    [SerializeField] private Item item;
    private Inventory inventory;

    public void Awake()
    {
        inventory = transform.GetComponentInParent<Inventory>();
    }

    public void AddItem (Item newItem)
    {
        item = newItem;

        icon.sprite = item.icon;
        icon.enabled = true;
    }

    public void clearSlot()
    {
        item = null;
        icon.sprite = null;
        icon.enabled = false; 
    }

    public void GiveItem()
    {
        if (item != null)
        {
            inventory.choosedItem = item;
        }
       
    }
}
