using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEngine : MonoBehaviour
{
    public Transform path;

    private List<Transform> myNodes;

    public float maxSteerAngle = 45f;

    public WheelCollider myWheelCollider;
    Vector3 nodePosition;
    public float moveSpeed;
    public float turnSpeed;
    public Rigidbody rigidBody;


    //keep track of current nodes
    private int currentNode = 0;
    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
        myNodes = new List<Transform>();

        foreach (Transform t in pathTransforms)
        {
            if (t != path.transform)
            {
                myNodes.Add(t);
            }
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        Transform nodePosition = myNodes.ElementAt(0);
        //ApplySteer();
        Drive(nodePosition.position);
    }

    /*private void ApplySteer(Vector3 nodePosition)
    {
        Vector3 relativeVector = transform.InverseTransformPoint(myNodes[currentNode].position);
        print(relativeVector);
        //relativeVector = relativeVector / relativeVector.magnitude;
        float newSteer = (relativeVector.x / relativeVector.magnitude) * maxSteerAngle;
        myWheelCollider.steerAngle = newSteer;
    }*/

    private void Drive(Vector3 nodePosition)
    {
        Vector3 distanceFromPlayer = nodePosition - transform.position;
        float distance = Vector3.Distance(nodePosition, transform.position);
        distanceFromPlayer.Normalize();
        rigidBody.velocity = distanceFromPlayer * moveSpeed;
        if (rigidBody.velocity != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(rigidBody.velocity);
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, Time.deltaTime * turnSpeed);
        }
    }
}
