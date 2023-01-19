using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class DeathScreen : MonoBehaviour
{

    public GameObject DeadScreen;
    bool deadPlayer;
    void Start()
    {
        DeadScreen.SetActive(false);
        deadPlayer = PlayerManager.instance.playerDied;
    }

    void Update()
    {
        deadPlayer = PlayerManager.instance.playerDied;
        if (Input.GetButtonDown("Death")){ // We can set this in Unity's input settings
            DeadScreen.SetActive(!DeadScreen.activeSelf); // Checking whether the UI is on or off, and then setting it to the inverse of that
        }
        if (deadPlayer){
            DeadScreen.SetActive(true);
            float delay = 3f;
            WaitForSeconds wait = new WaitForSeconds(delay);
            deadPlayer = false;
        } 
    }
}
