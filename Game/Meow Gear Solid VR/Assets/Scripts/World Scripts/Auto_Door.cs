using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDoor : MonoBehaviour
{

    public GameObject autoDoor;

    public float maximumOpenning = 10f;
    public float minimumClosing = 0f;

    public float doorSpeed = 5f;

    public bool playerIsHere;
    // Start is called before the first frame update
    void Start()
    {
        playerIsHere = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (playerIsHere)
        {
            if(autoDoor.transform.position.x < maximumOpenning)
            {
                autoDoor.transform.Translate(doorSpeed * Time.deltaTime, 0f, 0f);
            }

        }

        else
        {
            if (autoDoor.transform.position.x > minimumClosing)
            {
                autoDoor.transform.Translate(-doorSpeed * Time.deltaTime, 0f, 0f);
            }
        }
            
    }

    private void OnTriggerEnter(Collider col)
    {
        if((col.gameObject.tag == "Player")||(col.gameObject.tag == "Enemy"))
        {
            playerIsHere = true;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if ((col.gameObject.tag == "Player")||(col.gameObject.tag == "Enemy"))
        {
            playerIsHere = false;
        }
    }
}
