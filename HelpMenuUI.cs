using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpMenuUI : MonoBehaviour
{
    public GameObject helpmenuUI;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("HelpMenu")){ // We can set this in Unity's input settings
            helpmenuUI.SetActive(!helpmenuUI.activeSelf); // Checking whether the object is active, and then setting it to the inverse of that
        }
    }
}
