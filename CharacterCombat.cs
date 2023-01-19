using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterStats))] // Requirement to have character stats componenet for this to be active
public class CharacterCombat : MonoBehaviour
{
    public float attackSpeed = 1f; // Creates a float for the attack speed of the character
    private float attackCooldown = 0f; // A float for the cooldown of the attack the character does
    public float attackDelay = .5f; // Float for delay before attacking
    const float combatCooldown = 4; // If the character hasn't performed an attack in the last 4s, they will no longer be in combat
    float lastAttackTime;

    public AudioSource grunt;
    public float volume = 1f;

    public bool InCombat { get; private set; } // Bool for when the character is in combat
    public event System.Action OnAttack; // Callback for when the character attacks something
    
    CharacterStats myStats;
    
    void Start(){
        myStats = GetComponent<CharacterStats>(); // Get method to retrieve the CharacterStats component of the current game object
        grunt = GetComponent<AudioSource>(); // For when we attack
    }

    void Update(){
        attackCooldown -= Time.deltaTime; // Reduce the cooldown in accordance to time

        if (Time.time - lastAttackTime > combatCooldown){
            InCombat = false;
        }
    }
    
    public void Attack(CharacterStats targetStats){ // Function to attack the target character, take the target's stats because that is what we are changing
        if (targetStats.currentHealth > 0){ // Do not attack target if they are already dead
            if (attackCooldown <= 0f){ // If our attack cooldown is less than 0, we can attack
            // if (!grunt==null){
            grunt.PlayOneShot(grunt.clip, volume);
            //}
            StartCoroutine(DoDamage(targetStats, attackDelay)); // Go to our enum passing the target stats as the parameter and the attack delay we defined earlier
            if (OnAttack != null){
                OnAttack();
            }
            
            attackCooldown = 1f / attackSpeed;
            InCombat = true;
            lastAttackTime = Time.time;
            }
        }
    }

    IEnumerator DoDamage (CharacterStats stats, float delay){ // To not attack instantly
        yield return new WaitForSeconds(delay); // Wait (delay) amount of seconds
        stats.TakeDamage(myStats.damage.GetValue()); // Change the target character's stats in relation to the current character's damage stat, and the target's armour.
        if (stats.currentHealth <= 0){
            InCombat = false;
        }
    }

    
}
