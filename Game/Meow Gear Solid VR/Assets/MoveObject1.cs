using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject1 : MonoBehaviour
{
    public float speed = 2;
    Vector3 moveForward = new Vector3(0, 0, 5);
    Vector3 moveBackward = new Vector3(0, 0, -5);

    // Start is called before the first frame update
    void Start()
    {
        transform.position = new Vector3(3, 0, 5);
        //Invoke("GoForward", 3);
        //Invoke("GoBackward", 3);

    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);
        //Vector3 movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        //transform.Translate(movementDirection * speed * Time.deltaTime);
        //GoForward();
        //GoBackward();
    }
    void GoForward()
    {
        transform.Translate(moveForward * speed * Time.deltaTime);
    }
    
    void GoBackward()
    {
        transform.Translate(moveBackward * speed * Time.deltaTime);
    }
}
