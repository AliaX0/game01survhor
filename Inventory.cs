using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    // To avoid having multiple instances of the Inventory...
    public static Inventory instance; // To create a point of reference to the Inventory, that is shared by all the instances of a class
    void Awake(){ // On start up of the game
        if (instance != null){
            Debug.Log("More than one Inventory found..."); 
            return;
        }
        instance = this; // We set the static variable of type Inventory to this particular Inventory.
    }
    
    // Update the Inventory UI whenever there is a change in items
    public delegate void OnItemChanged();
    public OnItemChanged onItemChangedCallback;


    public int space = 20; // How many spaces are available in the bag

    public List<Item> items = new List<Item>(); // Creates a new list of type Item
    
    public bool Add (Item item){ // bool return type depending on if the item is picked up or not
        if (!item.isDefaultItem){ // If the item isn't a default item
            if (items.Count >= space){
                Debug.Log("Not enough space in Inventory");
                return false;
            }
            items.Add(item); // Adds an item to the current list of items, which will be in the inventory

            if (onItemChangedCallback != null){ // Checking if there are methods relating to this callback
                onItemChangedCallback.Invoke(); // Triggers event for the UI to updated
            }
        }
        return true;
    }
    
    public void Remove (Item item){
        items.Remove(item); // Removes the item from the current list of items.
        if (onItemChangedCallback != null){ 
                onItemChangedCallback.Invoke(); 
        }
    }
}
