using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class LookAtMe : MonoBehaviour
{
    public Transform lookAtThis;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(lookAtThis.GetComponent<Transform>().position);
    }
}
