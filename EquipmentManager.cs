using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentManager : MonoBehaviour
{
    public static EquipmentManager instance; // Create a point of reference to the Equipment Manager for this specific version of the game.
    void Awake() { // When the game is loaded
        instance = this;
    }

    Equipment[] currentEquipment;
    Inventory inventory; // Create a reference for our Inventory
    // Callback method, for when our equipment changes, this is needed for stat changes or if we were to add graphical changes
    public delegate void OnEquipmentChanged(Equipment newItem, Equipment currentItem);
    public OnEquipmentChanged onEquipmentChanged;


    void Start() {
        inventory = Inventory.instance; // Set the inventory to the current instance of Inventory

        int numSlots = System.Enum.GetNames(typeof(EquipmentSlot)).Length; // Set the number of slots equal to the equipment slots available. This is a function for us to get a string array of all the elements of EquipmentSlot type, and then we find the length. 
        currentEquipment = new Equipment[numSlots];
    }

    public void Equip (Equipment newItem){ // Equip a new item, by adding it to the array
        int slotIndex = (int)newItem.equipSlot; // Create a new integer for where the equipment will be in the currentEquipment array
        Equipment currentItem = null; // Create a variable of type Equipment, by default will be null
        if (currentEquipment[slotIndex] != null){ // If there is something already in the place we want to add the item to
            currentItem = currentEquipment[slotIndex]; // The currentItem variable is then set to the current item in the place
            inventory.Add(currentItem); // Then the current item is added back into the inventory
        }
        if (onEquipmentChanged != null){ // If we have any methods to notify
            onEquipmentChanged.Invoke(newItem, currentItem);
        }
        currentEquipment[slotIndex] = newItem; // We set the index of our currentEquipment to our new item. eg. If the new item has an equipSlot of 1, the new item is equiped to the chest equipment slot.
    }

    public void Unequip (int slotIndex){
        Equipment currentItem = null;
        if (currentEquipment[slotIndex] != null){
            currentItem = currentEquipment[slotIndex];
            inventory.Add(currentItem); // Adding the item back to the inventory
            currentEquipment[slotIndex] = null; // Removing the item from the slot
        }

        if (onEquipmentChanged != null){ // If we have any methods to notify
            onEquipmentChanged.Invoke(null, currentItem);
        }
    }

    public void UnequipAll () {
        for (int i = 0; i < currentEquipment.Length; i++){ // Loop through the all the elements in the current equipment slots
            Unequip(i); // Call upon the unequip method to remove it from that slot
        }
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.U)){
            UnequipAll();
        }
    }
}
