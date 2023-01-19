using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStats : CharacterStats // Deriving from the CharacterStats component
{
    public override void Die(){ // Overrides the Die function in CharacterStats, as we want a different way of dying compared to other characters
        base.Die();
        // Ragdoll, Death animation
        Destroy(gameObject);
    }
}
