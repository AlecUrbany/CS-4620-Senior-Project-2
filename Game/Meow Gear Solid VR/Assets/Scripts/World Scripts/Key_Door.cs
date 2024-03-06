using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyDoor : MonoBehaviour
{
    public GameObject keyDoor;

    public float maximumOpenning = 10f;
    public float minimumClosing = 0f;

    public float doorSpeed = 10f;

    bool keyIsHere;
    // Start is called before the first frame update
    void Start()
    {
        keyIsHere = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (keyIsHere)
        {
            if (keyDoor.transform.position.x < maximumOpenning)
            {
                keyDoor.transform.Translate(doorSpeed * Time.deltaTime, 0f, 0f);
            }
            else
            {
                if(keyDoor.transform.position.x > minimumClosing)
                {
                    keyDoor.transform.Translate(-doorSpeed * Time.deltaTime, 0f, 0f);
                }
            }
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Key")
        {
            keyIsHere = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "Key")
        {
            keyIsHere = false;
        }
    }
}
