using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; // To access the NavMesh capabilities of Unity

    [RequireComponent(typeof(NavMeshAgent))] // This means that Unity will add a NavMeshAgent whenever this component is accessed
public class PlayerMovement : MonoBehaviour
{
    Transform target; // Creates a Transform variable to reference our target.
    NavMeshAgent agent; // Create a NavMeshAgent variable to reference our agent.

    void Start()
    {
        agent = GetComponent<NavMeshAgent>(); // To retrieve our NavMeshAgent
    }

    void Update(){
        if (target != null){ // If we have a target
            agent.SetDestination(target.position); // Telling our agent (player's model) to follow the target as the targets position is the new destination.
            FaceTarget();
        }
    }

    public void MoveToPoint (Vector3 point) // A function for player movement, it is public so that it can be accessed by other components.
    {
        agent.SetDestination(point); // Sets our agents current destination as the mouse point.
    }

    public void FollowTarget (Interactable newTarget){ // A function to follow the current focus and keep following it
        agent.stoppingDistance = newTarget.radius * .8f; // Sets the stopping distance from our target, multiply by .8 so that its near the centre of the target and not on the outskirts
        agent.updateRotation = false;
        target = newTarget.interactionTransform;
    }

    public void StopFollowingTarget(){
        target = null; // If there is no target the player stops moving towards it.
        agent.updateRotation = true;
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized; // Creates a direction towards the target. We normalizing it lets us just use the direction from it and not the magnitude.
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); // Creates a new Quaternion to know how to rotate to look at that target. To stop the player from looking up and down, we create a new vector, and set our y value as 0f to avoid any changes to the y value.
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f); // We want to interpolate between the two rotations, to do this smoothly we can use Quaternion.Slerp to spherically interpolate between the original quaternion our player was at and the quaternion we need to be in to face our target.
    }
}
