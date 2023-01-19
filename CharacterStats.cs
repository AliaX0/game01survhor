using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth { get; private set; } // Other classes can get the value but cannot set the value
    public Stat damage; // Reference to our damage stats, how much this character can attack
    public Stat armour; // Reference to our armour stats, how much this character can take
    public GameObject bloodSplat; // Reference for our blood particle system that is stored as a game object
    public float tDelay = 1.0f; // Delay before destroy object

    public event System.Action<int,int> OnHealthChanged; // Create new event taking two parameters for their current health and their max health

    void Awake (){
        currentHealth = maxHealth;
    }
    void Update (){

        // Testing
        if (Input.GetKeyDown(KeyCode.T)){
            TakeDamage(10);
        }
        if (Input.GetKeyDown(KeyCode.P)){
           GainHealth(5);
        }
    }

    public void TakeDamage (int damage){
        damage -= armour.GetValue();
        damage = Mathf.Clamp(damage, 0, int.MaxValue); // do not want our damage to be negative or else it will heal the target or cause an error
        //bloodSplat.SetActive(true); 
        Instantiate(bloodSplat, new Vector3((transform.position.x), (transform.position.y+1), (transform.position.z)), transform.rotation); // Spawn the clone of the blood splatter, and cast it to a gameObject, so that we can remove this specific bloodsplatter. - Create a game object of the game object we defined earlier.
        currentHealth -= damage; // Our current health is equal to our current health minus the damage we have taken;
        Debug.Log(transform.name + " takes " + damage + " amount of damage."); // Testing purposes to know how much damage we have taken
        if (OnHealthChanged != null){
            OnHealthChanged(maxHealth, currentHealth);
        }

        if (currentHealth <= 0){
            Die();
        }
        //StartCoroutine(waiter());
        //bloodSplat.SetActive(false);
    }
    public void GainHealth (int hpboost){
        if (currentHealth + hpboost >= maxHealth){
            currentHealth = maxHealth;
        }
        else {
        hpboost = Mathf.Clamp(hpboost, 0, maxHealth); // The hp boost can't exceed the maximum health the player has
        currentHealth += hpboost;
        }
        Debug.Log(transform.name + " gains " + hpboost + " amount of HP!");
        if (OnHealthChanged != null){
            OnHealthChanged(maxHealth, currentHealth);
        }
    }

    

    public virtual void Die () { // Different characters will have different things happen after they die, so we want this method to be able to be overwritten
    Debug.Log(transform.name + " died.");

    }
    //IEnumerator waiter(){
    //yield return new WaitForSeconds(2);
    //}

}