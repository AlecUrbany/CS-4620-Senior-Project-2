using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyEngine : MonoBehaviour
{
    public Transform path;
    public  List<Transform> myNodes;
    public float maxSteerAngle = 45f;

    public WheelCollider myWheelCollider;
    Vector3 nodePosition;
    public float moveSpeed;
    public float turnSpeed;
    public Rigidbody rigidBody;

    public Transform myCurrentNode;
    public int index;


    //keep track of current nodes
    private int currentNode = 0;

    // Start is called before the first frame update
    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        myNodes = new List<Transform>();
        Transform[] pathTransforms = path.GetComponentsInChildren<Transform>();
     
        foreach (Transform t in pathTransforms)
        {
            if (t != path.transform)
            {
                myNodes.Add(t);
            }
        }
        index = 0;
        myCurrentNode = myNodes.ElementAt(index);
    }

    // Update is called once per frame
    
    private void Update()
    {
        Vector3 distance = transform.position - myCurrentNode.position;
        distance.y = 0;

        if((distance).magnitude > 0.01f)
        {
            Drive(myCurrentNode.position);
        }
        else
        {
            //update current node
            ++index;
            if(index == myNodes.Count)
            {
                index = 0;
                myCurrentNode = myNodes.ElementAt(index);
            }
            else
            {
                myCurrentNode = myNodes.ElementAt(index);
            }
        }
    }
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
