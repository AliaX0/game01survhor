using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;
    
    void Awake(){
        instance = this;
    }
    public GameObject player;
    public bool playerDied;


    public void KillPlayer(){
        playerDied = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        playerDied = false;
    }

    public void MovePlayer(){
        // Load up the next scene
        Debug.Log("Level Completed! Moving to next scene...");
    }
}
