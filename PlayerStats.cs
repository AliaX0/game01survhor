using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : CharacterStats
{
    public GameObject DeadScreen;
    public AudioSource deathSound;
    public float volume = 1f;
    void Start()
    {
        EquipmentManager.instance.onEquipmentChanged += OnEquipmentChanged; // Access the callback method for whenever a new item was equiped in the EquipmentManager
                                                                            // We can create a new method and subscribe it to this class

    }

    void OnEquipmentChanged (Equipment newItem, Equipment currentItem){

        if (newItem != null){ // If we have a new item that we are adding
        armour.AddModifier(newItem.armourModifier); // Add armour modifier 
        damage.AddModifier(newItem.damageModifier); // Add damage modifier 
        }
        if (currentItem != null){ // If we have an old item that we are removing
        armour.RemoveModifier(currentItem.armourModifier); // Remove armour modifier 
        damage.RemoveModifier(currentItem.damageModifier); // Remove damage modifier 
        }
    }

    public override void Die(){
        base.Die(); // Standard death function 
        StartCoroutine(waiter());
    }

    IEnumerator waiter(){
        DeadScreen.SetActive(true);
        deathSound.PlayOneShot(deathSound.clip, volume);
        yield return new WaitForSeconds(2);
        DeadScreen.SetActive(false);
        PlayerManager.instance.KillPlayer(); // Call upon a method to kill the player in the Player Manager component, in this current instance of the game

    }


}
