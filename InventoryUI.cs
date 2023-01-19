using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    Inventory inventory; 
    public Transform Parent; // Create a reference to the parent of the inventory slots
    public GameObject inventoryUI;
    InventorySlot[] slots; // Create an array of Inventory slots, which is called slots
    void Start()
    {
        inventory = Inventory.instance;
        inventory.onItemChangedCallback += UpdateUI;
        slots = Parent.GetComponentsInChildren<InventorySlot>(); // Get the children of the parent of our inventory slots 
    }


    void Update()
    {
        if (Input.GetButtonDown("Inventory")){ // We can set this in Unity's input settings
            inventoryUI.SetActive(!inventoryUI.activeSelf); // Checking whether the UI is on or off, and then setting it to the inverse of that
        }
    }

    void UpdateUI() {   
        for (int i = 0; i < slots.Length; i++){ // Looping through all of our slots
            if (i < inventory.items.Count){ // Check if there are more items to add
                slots[i].AddItem(inventory.items[i]); // We want to add the item to our [i]th slot to our inventory items array
            } else {
                slots[i].ClearSlot();
            }
        }
    }
}
