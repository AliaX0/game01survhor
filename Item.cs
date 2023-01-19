using UnityEngine;

[CreateAssetMenu(fileName = "New Item", menuName = "Inventory/Item")] // This will allow us to create scriptable objects in the Unity Editor
public class Item : ScriptableObject { // This script is not specified to a game object, this component is for all scriptable objects
    new public string name = "New Item"; // new overrides the old name with the current name we're setting for it
    public Sprite icon = null; // This will be changed later, just set to null by default.
    public bool isDefaultItem = false; // The item will not be the default item when initiated.

    public virtual void Use (){
        // Use the item
        Debug.Log("Using " + name);
    }

    public void RemoveFromInventory() {
        Inventory.instance.Remove(this);
    }
}
