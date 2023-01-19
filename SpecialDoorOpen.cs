using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpecialDoorOpen : Interactable
{
    public bool doorOpened;
    public GameObject openedDoor; // Reference to opened door game object
    public GameObject closedDoor; // Reference to closed door game object
    public override void Interact(){ // Override the Interact() method to what we want to do with it, here open the door
        base.Interact(); // To ensure we are interacting with it
        doorOpened = true; // We have interacted with it now
        OpenDoor(); // Calls upon the Open Door funtion
    }

    void Start(){
        doorOpened = false;
        openedDoor.SetActive(false);
    }
    void OpenDoor(){
        Debug.Log("Opening door... ");
        if (doorOpened){  // If we interacted with the door
            closedDoor.SetActive(false); // Then we remove it from the scene
            openedDoor.SetActive(true); // Set the inactive gameobject to active
        }
    }
}
