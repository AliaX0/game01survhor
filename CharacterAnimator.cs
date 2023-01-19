using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterAnimator : MonoBehaviour
{
    NavMeshAgent agent; // References the navmeshagent component
    protected Animator animator; // References the animator component
    protected CharacterCombat combat;
    protected virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>(); // Use get method on the children of the parent object as the animator is on the graphic. Search for type animator.
        combat = GetComponent<CharacterCombat>();
        combat.OnAttack += OnAttack;
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        float speedPercentage = agent.velocity.magnitude / agent.speed; // Player s current velocity divided by their maximum speed
        animator.SetFloat("speedPercentage", speedPercentage, .2f, Time.deltaTime); // Setting the speedPercentage float that is in our animator controller for the player, to make the transitions more smooth we use damp time.
        animator.SetBool("inCombat", combat.InCombat);
    }

    protected virtual void OnAttack(){
        animator.SetTrigger("attack");
    }
}
