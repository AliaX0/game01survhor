using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable] // The fields inside this class will show up in the inspector because Unity serializes the class
public class Stat{

    [SerializeField] // So that it is editable in the inspector
    private int baseValue;

    private List<int> modifiers =  new List<int>(); // Create a new list for our modifiers that will be affected by our equipment

    public int GetValue(){
        int finalValue = baseValue;
        modifiers.ForEach(x => finalValue += x); // Look through all the modifiers in our modifiers list and then add them to our final value. X is just a placeholder for the element in that place on the list.
        return finalValue;
    }

    public void AddModifier (int modifier){
        if (modifier != 0){
            modifiers.Add(modifier);
        }
    }
    public void RemoveModifier (int modifier){
        if (modifier != 0){
            modifiers.Remove(modifier);
        }
    }

}
