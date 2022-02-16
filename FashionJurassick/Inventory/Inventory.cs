using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Yarn.Unity;


public class Inventory : MonoBehaviour
{
    public ItemPickuP isOver; 
    public static Inventory instance;
    public int space = 30;

    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;
    public GameObject inventoryCanvas;
    public bool InventoryIsOpen;
    public Item choosedItem; 

    public Movement mvm;
    public DialogueRunner dialogueRunner;
    public InMemoryVariableStorage variableStorage;
    public float score;

    public InventoryUi inventoryUi;

    private void Start()
    {
        instance = this;
        dialogueRunner.AddCommandHandler("ClothGiving", ClothGiving);

    }

    public void Awake()
    {
        
        mvm = gameObject.transform.GetComponentInParent<Movement>();
        
    }

    public List<Item> items = new List<Item>();

    public bool Add (Item item)
    {
        if (items.Count >= space)
        {
            return false;
        }
        else
        {
            items.Add(item);
            if (onItemChangedCallback!= null)
            {
               
                onItemChangedCallback.Invoke();
            }
            
        }
        return true;
    }

    public void Remove (Item item)
    {
        items.Remove(item);
        onItemChangedCallback.Invoke();
        inventoryUi.UpdateUI();
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemPickuP>())
        {
           isOver = collision.GetComponent<ItemPickuP>();
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<ItemPickuP>())
        {
            isOver = null;
        }
    }

    public void PickingUp(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {
            if (isOver != null)
            {
                isOver.PickUp();
            }
        }
       
    }
    public void PressedStart(InputAction.CallbackContext ctx)
    {
        if (ctx.performed)
        {

            OpenCloseInventory();
        }
    }
   
    public void OpenCloseInventory()
    {
        if (InventoryIsOpen == false)
        {
            inventoryCanvas.SetActive(true);
            InventoryIsOpen = true;
        }
        else if (InventoryIsOpen == true)
        {
            inventoryCanvas.SetActive(false);
            InventoryIsOpen = false;
        }
    }

    public void OpenInventory()
    {
        inventoryCanvas.SetActive(true);
    }
   

    
    public void ClothGiving(string[] parameters, System.Action onComplete)
    {
        OpenInventory();
        StartCoroutine(waitForPlayerChoice(onComplete));

        
    }

    private IEnumerator waitForPlayerChoice(System.Action onComplete)
    {
        yield return new WaitUntil(() => (choosedItem != null));
        mvm.dinoAtRange.dino.Compare(choosedItem);
        score = mvm.dinoAtRange.dino.score;
        variableStorage.SetValue("$Score", score);
        Debug.Log(mvm.dinoAtRange.dino.score);
        onComplete();

    }
}
