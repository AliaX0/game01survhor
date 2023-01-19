using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public Image icon; // Reference to our icon
    Item item; // Create a reference for the item
    public Button removeButton; // Reference to our button
    
    public void AddItem (Item newItem){
        item = newItem; // Set the item as the new item
        icon.sprite = item.icon;
        icon.enabled = true; // So that our image icon is now visible and active  
        removeButton.interactable = true; // When an item is added, we want the removeButton to be interactable because there is now an item to remove.
    }

    public void ClearSlot() { // For when the player wants to remove an item in the inventory
        item = null;

        icon.sprite = null;
        icon.enabled = false;
        removeButton.interactable = false;
    }

    public void OnRemoveButton (){ // For when the button is pressed
        Inventory.instance.Remove(item);
    }

    public void UseItem() {
        if (item != null){
            item.Use();
        }
    }
}
