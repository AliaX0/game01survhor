using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Potion", menuName = "Inventory/Potion")]
public class Potion : Item
{
    public CharacterStats myStats; // Reference for my character's stats
    public int healthPotion; // Reference the amount of health that will be increased if we use the potion
    //public int currentHP; // Reference to player's current HP
    public GameObject player;

    public override void Use(){ // Override the Use method because we want something else to happen when we use an equipment - we want to equip it.
        base.Use();
        // Use the potion
        player = GameObject.Find("Player");
        myStats = player.GetComponent<CharacterStats>();
        myStats.GainHealth(healthPotion);
        RemoveFromInventory(); // After using item, it is removed from the inventory by accessing the Item component which our class uses.

    }
}

