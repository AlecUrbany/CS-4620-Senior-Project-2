using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
    

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public Vector3 playerLastKnownPosition;
    public Vector3 playerCurrentPosition;
    public bool canSeePlayer;
    public float playerRange;
    public FieldOfView fieldOfView;
    public Rigidbody rigidBody;
    public float moveSpeed;
    public float mininumDistanceFromPlayer = 4f;
    public Vector3 startPosition;
    public int rotationSpeed;
    public NavMeshAgent agent;
    public Animator animator;
    private float currentPatrolDistance;
    private bool movingStage1;
    private bool movingStage2;
    private bool chasing;
    public Quaternion startRotation;
    public bool hasBeenAlerted;
    void Awake()
    {
        if(startPosition == Vector3.zero)
        {
            startPosition = transform.position;       
        }
        
        else
        {
            transform.position = startPosition;  
        }
        fieldOfView = GetComponent<FieldOfView>();
        startRotation = transform.rotation;
        rigidBody = GetComponent<Rigidbody>();
    }
    void Start()
    {
        animator.SetBool("IsMoving", true);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        playerCurrentPosition = player.position;
        Vector3 distanceFromPlayer = player.position - transform.position;
        canSeePlayer = fieldOfView.canSeeTarget;
        hasBeenAlerted = EventBus.Instance.inAlertPhase;
        
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }

        //If the player has been spotted, chase them
        if(hasBeenAlerted == true)
        {
            playerLastKnownPosition = EventBus.Instance.playerLastKnownPosition;
            animator.SetBool("IsAttacking", true);
            if(canSeePlayer == true)
            {
                
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);
            }
            else
            {
                FollowPlayer(playerCurrentPosition, playerLastKnownPosition, canSeePlayer);;
            }

        }
        //Otherwise, return to their position
        else
        {
            animator.SetBool("IsAttacking", false);
            if(!agent.pathPending)
            {
                rigidBody.velocity = Vector3.zero;
                agent.SetDestination(startPosition);
            }
            // Using distance included y which varied when enemies collided.
            if(startPosition.x == transform.position.x && startPosition.z == transform.position.z)
            {
                transform.rotation = startRotation;
                agent.ResetPath();
            }
        }
    }

    void FollowPlayer(Vector3 playerPosition, Vector3 lastKnownPosition, bool canSeePlayer)
    {
        if(canSeePlayer == true)
        {
            Vector3 distanceFromPlayer = playerPosition - transform.position;
            float distance = Vector3.Distance(playerPosition,transform.position);
            distanceFromPlayer.Normalize();
            Debug.Log("Here's the Distance: " + distance);
            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                transform.LookAt(player.transform);
            }
            if(distance > mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * moveSpeed;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                if (rigidBody.velocity != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }            
        }
        else
        {
            Vector3 distanceFromPlayer = playerLastKnownPosition - transform.position;
            float distance = Vector3.Distance(playerLastKnownPosition,transform.position);
            distanceFromPlayer.Normalize();
            Debug.Log("Here's the Distance: " + distance);
            if(distance <= mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * 0;
                animator.SetBool("IsMoving", false);
                animator.SetBool("IsAttacking", true);
                transform.LookAt(playerLastKnownPosition);
            }
            if(distance > mininumDistanceFromPlayer)
            {
                rigidBody.velocity = distanceFromPlayer * moveSpeed;
                animator.SetBool("IsMoving", true);
                animator.SetBool("IsAttacking", false);
                if (rigidBody.velocity != Vector3.zero)
                {
                    Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
                    transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
                }
            }             
        }

    }
    
}