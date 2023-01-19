using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Interactable : MonoBehaviour
{
    public float radius = 3f; // How far away the player has to be to interact with the object.
    public Transform interactionTransform; // Different objects will have different interactable areas.
    bool isFocus = false; // Create a boolean for when the current object is in focus.
    Transform player; // Create a transform variable to store the player's position in.
    bool hasInteracted = false; // Creates a boolean for when the player has interacted with the object.

    public virtual void Interact () // It is marked as virtual so that different objects will have different interactions even though the method is called, we can override it depending on what the object is.
    {
        // Testing Method
        //Debug.Log ("Interacting with " + transform.name);
    }

    void Update (){
        if (isFocus && !hasInteracted){ // If an object is in focus and we haven't already interacted with it.
            float distance = Vector3.Distance(player.position, interactionTransform.position); // calculates distance between player and the current object
            if (distance <= radius) // If our player is within the radius of the object
            {
                Interact();
                hasInteracted = true;
            }
        }
    }
    public void OnFocused(Transform playerTransform){
        isFocus = true; // Set to true, because our object is now in focus
        player = playerTransform;
        hasInteracted = false;
    }
    public void OnDefocused(){
        isFocus = false; // Set to false, because our object is not in focus
        player = null; // Set to null, to stop 
        hasInteracted = false;
    }

    void OnDrawGizmosSelected() { 
        if (interactionTransform == null){ // If there is no interaction area given
            interactionTransform = transform; // Then we set the interactionTransform, the area to interact with the object, as the object's transform
        }
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(interactionTransform.position, radius); // To visualize the range 
    }
}
