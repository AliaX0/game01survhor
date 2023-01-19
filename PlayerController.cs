using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

//lw517 - Lia Wilkinson

[RequireComponent(typeof(PlayerMovement))] // Unity will need a PlayerMovement whenever this class is accessed. 
public class PlayerController : MonoBehaviour
{
    public Interactable focus; // Create a public interactable variable to keep track of what the player is currently focused on.
    
    public LayerMask movementMask; // Creates a layer mask for the areas we want the player to be able to move in on the map.
    Camera cam; // Creates a camera variable called cam, to reference the camera.
    PlayerMovement motor; // Creating a PlayerMovement object so that we can use it for referencing and accessing the functions in the PlayerMovement script component.

    public GameObject helpmenuUI;

    void Start()
    {
        cam = Camera.main;   // To set the camera varialbe to our main camera.
        motor = GetComponent<PlayerMovement>(); // We want to get the component of type PlayerMovement.
    }

    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject()){
            return;
        }
        if (Input.GetMouseButtonDown(0)){ // When left click on the mouse is pressed.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Creates a new ray from our cam variable to the mouse
            RaycastHit hit; // This will store what the ray hit in a variable.
            // Casting ray
            if (Physics.Raycast(ray, out hit, 100, movementMask)) { // We are shooting out the ray, which we defined earlier, and we're storing information in our hit variable from earlier; the third argument is the range given and the last is for the layer mask we defined earlier.
                //Debug.Log (hit.collider.name + " was hit. at" + hit.point); // Testing, to see what is being clicked
                // Move the player to what was hit
                motor.MoveToPoint(hit.point);
                // Stop current action
                RemoveFocus();
            }
        }
        if (Input.GetMouseButtonDown(1)){ // When right click on the mouse is pressed.
            Ray ray = cam.ScreenPointToRay(Input.mousePosition); // Creates a new ray from our cam variable to the mouse
            RaycastHit hit; // This will store what the ray hit in a variable.
            
            if (Physics.Raycast(ray, out hit, 100)) { 
                Interactable interactable = hit.collider.GetComponent<Interactable>(); // Checking for interactable component and storing it in a variable.
                if (interactable != null){ // If there is an interactable in the range.
                    SetFocus(interactable); // The interactable becomes our focus.
                }
            }
        }
        if (Input.GetButtonDown("HelpMenu")){ // We can set this in Unity's input settings
            helpmenuUI.SetActive(!helpmenuUI.activeSelf); // Checking whether the object is active, and then setting it to the inverse of that
        }
    }
    void SetFocus(Interactable newFocus){ 
        if (newFocus != focus) // Only if the current focus has changed
        {
            if (focus != null){ // Only if we have a focus do we need to defocus it 
                focus.OnDefocused(); }
            focus = newFocus; // Setting our new focus variable, which is a temporary Interactable variable, as the value of the public focus variable.
            motor.FollowTarget(newFocus); // Calls upon the FollowTarget function in PlayerMovement and passes our new interactable, so that the player will move to that position.
        }
        newFocus.OnFocused(transform); // Calls upon the OnFocused function so that our interactable knows that it is in focus.
        
    }
    void RemoveFocus(){
        if (focus != null){
            focus.OnDefocused();
        }
        focus = null;
        motor.StopFollowingTarget(); // Calls upon the StopFollowingTarget function to stop the player from following the target.
    }
}
