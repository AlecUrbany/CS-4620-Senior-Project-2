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
    public Vector3 startPosition;
    public int rotationSpeed;
    public NavMeshAgent agent;
    private float currentPatrolDistance;
    private bool movingStage1;
    private bool movingStage2;
    private bool chasing;
    public Quaternion startRotation;
    private AlertPhase alertPhaseScript;
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
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update()
    {
        playerCurrentPosition = player.position;
        playerLastKnownPosition = EventBus.Instance.playerLastSeenLocation;
        Vector3 distanceFromPlayer = player.position - transform.position;
        canSeePlayer = fieldOfView.canSeeTarget;
        hasBeenAlerted = EventBus.Instance.inAlertPhase;
        //if(EventBus.Instance.enemyCanMove == false)
        //{
            //return;
        //}

        if(hasBeenAlerted == true)
        {
            if(canSeePlayer == true)
            {
                FollowPlayer(playerCurrentPosition);
            }
            else
            {
                FollowPlayer(playerLastKnownPosition);
            }

        }
        else
        {
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
    void FollowPlayer(Vector3 playerPosition)
    {
        Vector3 distanceFromPlayer = playerPosition - transform.position;
        distanceFromPlayer.Normalize();
        rigidBody.velocity = distanceFromPlayer * moveSpeed;

        if (rigidBody.velocity != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
    }
}