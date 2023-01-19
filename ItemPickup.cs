using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : Interactable
{
    public Item item; // Creates a Item variable to reference
    //public AudioSource ding; // Reference for audio sound when picked up
    //public float volume = 1.0f;
    public override void Interact(){ // We override the Interact method in the Interactable component, as its virtual, so now we can specify what we want to happen when the item is interacted with.
        base.Interact(); // The usual method that occurs when interacting with the item.

        PickUp(); // Calls upon the PickUp() function
    }

    void PickUp(){
        //ding.PlayOneShot(ding.clip, volume);
        Debug.Log("Picking up " + item.name);
        bool wasPickedUp = Inventory.instance.Add(item); // Picks up the item if adding it to the inventory was successful
        if (wasPickedUp){ // If the item was added to the inventory
            Destroy(gameObject); // Removing the item from the scene
        }
    }
}
