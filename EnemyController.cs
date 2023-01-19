using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
{
    // Field Of View
    public float lookRadius = 10f; // Enemy's line of sight in spherical sense

    [Range(0,360)]
    public float lookAngle; // Angle of eyesight range
    public AudioSource heartBeat; // Sound to signal player they have been spotted
    public float volume = 1f;
    public bool heartbeatPlaying; //

    public GameObject playerRef; // Reference to the Player
    

    public LayerMask targetMask;
    public LayerMask visualObstruction; // Reference to objects blocking line of site

    public bool canSeePlayer;

    Transform target; // Player's position
    NavMeshAgent agent; // Reference to the NavMeshAgent component
    CharacterCombat combat; // Reference to CharacterCombat component
    
    void Start(){
        //canSeePlayer = false;
        heartbeatPlaying = false;
        playerRef = PlayerManager.instance.player; // Using the PlayerManager to refer to the player game object
        agent = GetComponent<NavMeshAgent>(); // Get method for the NavMeshAgent component of the current game object
        target = PlayerManager.instance.player.transform; // Using the PlayerManager component, we find the player game object of this instance of PlayerManager, and convert it to a transform so that we can follow it's position.
        combat = GetComponent<CharacterCombat>(); // Get method for the CharacterCombat component of the enemy (current object)
        StartCoroutine(FOVRoutine());
        
    }
    private IEnumerator FOVRoutine(){
        float delay = 0.2f;
        WaitForSeconds wait = new WaitForSeconds(delay);
        while (true){
            yield return wait;
            FieldOfViewCheck();
        }
    }
    private void FieldOfViewCheck(){ // Method to check if Player is in FOV
        Collider[] rangeChecks = Physics.OverlapSphere(transform.position, lookRadius, targetMask); // Create a Collider array, to find any colliders at our current position but only on the layermask that the player will be on
        if(rangeChecks.Length != 0){
            Transform targetPos = rangeChecks[0].transform; // Since there is only our player in this layer, we will get the transform/position of the first element in that array
            Vector3 directionToTarget = (targetPos.position - transform.position).normalized;
            if(Vector3.Angle(transform.forward, directionToTarget) < lookAngle / 2){
                float distanceToTarget = Vector3.Distance(transform.position, targetPos.position);

                if (!Physics.Raycast(transform.position, directionToTarget, distanceToTarget, visualObstruction)){ // Start a ray cast from the centre of our enemy, aim towards the player, measuring at the length of the distance to our player, and stop if it hits anything in the visualObstruction layer mask
                    canSeePlayer = true; // Since we have the inverse of the if statement, that means there are no colliders on the visualObstruction layer, so we can see the player.
                    
                } else {
                    canSeePlayer = false;
                }
            } else {
                canSeePlayer = false;
            }
        } else if (canSeePlayer) { // If canSeePlayer was set to true, we set it to false now because it's no longer in range.
            canSeePlayer = false;
        }
    }
    void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);
        if ((distance <= lookRadius)&&(canSeePlayer)){         
            // if (!Physics.Raycast(transform.position, (target.position - transform.position), 100, visualObstruction)){}
            agent.SetDestination(target.position);
            if (heartbeatPlaying == false){
                heartBeat.Play();
                heartbeatPlaying = true;
            }

            if (distance <= agent.stoppingDistance){
                CharacterStats targetStats = target.GetComponent<CharacterStats>(); // Get method for the CharacterStats component of our enemy, which is in this case the player
                if (targetStats != null){
                    combat.Attack(targetStats); // Call upon the attack function passing the target's stats as the parameter for changing
                }
                FaceTarget(); // Call upon the method to update our current rotation/Quaternion so that we are always facing the target

            }
            
        } 
        if ((canSeePlayer == false)||(distance > lookRadius)){
            heartBeat.Pause();
            heartbeatPlaying = false;
        }
    }

    void FaceTarget(){
        Vector3 direction = (target.position - transform.position).normalized; // Create a direction towards the target and normalize it as we do not want its magnitude
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0f, direction.z)); // Create a rotation towards the target, ignoring the target's y position
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 4f); // Update the enemy's current rotation to be towards the target (the new Quaternion we created earlier) and to smooth it we use Slerp - so we are spherically interpolating between those two rotations.
    
    }

    void OnDrawGizmosSelected() { // So that we can see the enemy's line of sight in the editor
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }

    
}
