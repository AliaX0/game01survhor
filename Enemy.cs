using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(CharacterStats))] // Make sure that we have the CharacterStats component on this object
public class Enemy : Interactable
{
    PlayerManager playerManager; // Reference to find the player
    CharacterStats myStats; // Reference to the current character's stats, in this case - the Enemy.
    bool playerDied;

    void Start(){
        playerManager = PlayerManager.instance; // Retrieving the current instance of the PlayerManager, that is specific to this instance of the game that has been loaded.
        myStats = GetComponent<CharacterStats>(); // Retrieving the CharacterStats component on the current game object, which is the Enemy character.
        playerDied = PlayerManager.instance.playerDied; // Retrieving the boolean from the current instance of PlayerManager
    }

    public override void Interact(){
        base.Interact(); // Standard interaction following the Interact() in Interactable class
        // Attack player
        CharacterCombat playerCombat = playerManager.player.GetComponent<CharacterCombat>(); // Get CharacterCombat component from our player and store it in another CharacterCombat variable
        if (playerCombat != null){
            playerCombat.Attack(myStats);
        }
    }
}
