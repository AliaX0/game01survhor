using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Equipment")]
public class Equipment : Item
{
    public EquipmentSlot equipSlot; // References to the enum created
    public int armourModifier; // References the armour modifier, how much less damage the player can take
    public int damageModifier; // References the damage modifier, how much more damage the player outputs

    public override void Use(){ // Override the Use method because we want something else to happen when we use an equipment - we want to equip it.
        base.Use();
        // Equip item
        EquipmentManager.instance.Equip(this); // Calls upon the equip function in the EquipmentManager script component.
        RemoveFromInventory(); // After equiping item, it is removed from the inventory by accessing the Item component which our class uses.

    }
}

// To avoid encapsulation by the equipment class
public enum EquipmentSlot { Head, Chest, Legs, Feet, Weapon } // Associates the equipment to a particular slot in the EquipmentSlot enum