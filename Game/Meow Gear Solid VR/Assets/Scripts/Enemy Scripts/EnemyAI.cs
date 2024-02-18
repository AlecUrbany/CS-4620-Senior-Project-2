using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public enum EnemyAIType{
    leftRight,
    UpDown,
    Square,
    Stationary
}

public class EnemyAI : MonoBehaviour
{
    
    public Transform player;
    public float playerRange;
    public GameObject childObject;
    public Rigidbody rb;
    public float moveSpeed;
    public EnemyAIType aiType;
    public float patrolDistance = 10.0f;
    public Vector3 startPosition;
    public int rotationSpeed;
    public NavMeshAgent agent;
    private float currentPatrolDistance;
    private bool movingStage1 = false;
    private bool movingStage2 = false;
    private int phase = 1;
    private bool patrol = true;
    private bool chasing = false;
    public Quaternion startRotation;
    private AlertPhase alertPhaseScript; 

//These six lines are for the exclamation point upon noticing the player
    public Transform enemyMouth;
    [SerializeField] private GameObject floatingTextBox;
    public float nameLifeSpan = .5f;
    public AudioSource source;
    public AudioClip alertSound;
    public bool hasBeenAlerted = false;




    void Awake(){
        if(startPosition == Vector3.zero){
            startPosition = transform.position;  
            patrol = false;      
        }
        else{
            transform.position = startPosition;  
        }
        startRotation = transform.rotation;
        GameObject childObject = transform.Find("Enemy Sightline").gameObject;
        alertPhaseScript = GameObject.FindGameObjectWithTag("GameStateManager").GetComponent<AlertPhase>();
        hasBeenAlerted = alertPhaseScript.getInAlertPhase();
    }
    void Start(){
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    void Update(){
        hasBeenAlerted = alertPhaseScript.getInAlertPhase();
        if(EventBus.Instance.enemyCanMove == false)
        {
            return;
        }
        Vector3 distanceFromPlayer = player.position - transform.position;
        bool canSeePlayer = childObject.GetComponentInChildren<visionCone>().canSeePlayer;
        if(distanceFromPlayer.magnitude < playerRange && canSeePlayer)
        {
            if(!hasBeenAlerted)
            {
                ShowAlertSound();
                hasBeenAlerted = true;
            }
            chasing = true;
            patrol = false;
            alertPhaseScript.updateCanSeePlayer(canSeePlayer);
            FollowPlayer(player.position);
        }

        else if(chasing && canSeePlayer){
            alertPhaseScript.updateCanSeePlayer(canSeePlayer);
            FollowPlayer(player.position);
        }

        else if(chasing && !canSeePlayer){
            chasing = false;
            if(alertPhaseScript.getTimeRemaining() > 0){
                hasBeenAlerted = true;
            }
            else{
                hasBeenAlerted = false;
                patrol = true;
            }
        }

        else if(hasBeenAlerted && alertPhaseScript.getTimeRemaining() > 0){
            // Go to player's last known location
            Vector3 lastKnownPosition = alertPhaseScript.getLastKnownPosition();
            float threshold = 0.2f;
            if(Vector3.Distance(lastKnownPosition, transform.position) < threshold){
                rb.velocity = Vector3.zero;
            }
            else{
                FollowPlayer(lastKnownPosition);
            }
            
        }

        else if (patrol){
            patrol = true;
            switch(aiType){
                case EnemyAIType.leftRight:
                    PatrolHorizontal();
                    break;
                case EnemyAIType.UpDown:
                    PatrolVertical();
                    break;
                case EnemyAIType.Square:
                    PatrolSquare();
                    break;
                case EnemyAIType.Stationary:
                    break;
            }
        }
        else{
            // Go back to start in order to resume patrol
            if(!agent.pathPending){
                rb.velocity = Vector3.zero;
                agent.SetDestination(startPosition);
            }
            // Using distance included y which varied when enemies collided.
            if(startPosition.x == transform.position.x && startPosition.z == transform.position.z) {
                transform.rotation = startRotation;
                patrol = true;
                movingStage1 = false;
                movingStage2 = false;
                agent.ResetPath();
            }
        }
    }


    void FollowPlayer(Vector3 playerPosition){
        Vector3 distanceFromPlayer = playerPosition - transform.position;
        distanceFromPlayer.Normalize();
        rb.velocity = distanceFromPlayer * moveSpeed;

        if (rb.velocity != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * rotationSpeed);
        }
    }
    void PatrolHorizontal()
    {
        Vector3 horizontalMovementDirection = movingStage1 ? Vector3.right : Vector3.left;
        rb.velocity = horizontalMovementDirection * moveSpeed;


        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);


        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 8f);


        currentPatrolDistance += moveSpeed * Time.deltaTime;


        if (currentPatrolDistance >= patrolDistance){
            movingStage1 = !movingStage1;
            currentPatrolDistance = 0.0f;
        }
    }


    void PatrolVertical(){
        Vector3 verticalMovementDirection = movingStage1 ? Vector3.forward : Vector3.back;
        rb.velocity = verticalMovementDirection * moveSpeed;
        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);


        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 2f);


        currentPatrolDistance += moveSpeed * Time.deltaTime;


        if (currentPatrolDistance >= patrolDistance){
            movingStage1 = !movingStage1;
            currentPatrolDistance = 0.0f;
        }
    }


    void PatrolSquare(){
        Vector3 verticalMovementDirection = (movingStage1 && movingStage2) ? Vector3.back : movingStage1 ? Vector3.left : movingStage2 ? Vector3.right : Vector3.forward;
        rb.velocity = verticalMovementDirection * moveSpeed;
        Quaternion desiredRotation = Quaternion.LookRotation(rb.velocity);


        transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * 5f);


        currentPatrolDistance += moveSpeed * Time.deltaTime;


        if (currentPatrolDistance >= patrolDistance){
            switch(phase){
                case 1:
                    movingStage1 = !movingStage1;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 2:
                    movingStage2 = !movingStage2;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 3:
                    movingStage1 = !movingStage1;
                    currentPatrolDistance = 0.0f;
                    phase++;
                    break;
                case 4:
                    movingStage2 = !movingStage2;
                    currentPatrolDistance = 0.0f;
                    phase = 1;
                    break;
            }
        }
    }
    // IEnumerator ContinueChase(){
    //     yield return new WaitForSeconds(5f);
    //     if(!chasing){
    //         alert = false;
    //         // TODO: Figure out what to do with bellow line
    //         // alertPhaseScript.inAlertPhase = false;
    //         hasBeenAlerted = false;
    //         patrol = true;
    //     }
    //     chaseCoroutine = null;
    // }
    void ShowAlertSound()
    {
        if(floatingTextBox)
        {
            GameObject prefab = Instantiate(floatingTextBox, enemyMouth, false);
            source.PlayOneShot(alertSound);
            Destroy(prefab, nameLifeSpan);
        }
    }
}





