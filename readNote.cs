using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class readNote : Interactable
{
    public GameObject note;
    public override void Interact(){ // We override the Interact method in the Interactable component, as its virtual, so now we can specify what we want to happen when this object is interacted with.
    base.Interact(); // The usual method that occurs when interacting with an interactable.

    Read(); // Calls upon the PickUp() function
    }

    void Read(){
        Debug.Log("Reading " + note.name);
        StartCoroutine(waiter());
        //bool currentlyReading = true;
        //if (currentlyReading){ 
        //    note.SetActive(true);
        //}
    }

    IEnumerator waiter(){
        note.SetActive(true);
        yield return new WaitForSeconds(3);
        note.SetActive(false);
    }
}
