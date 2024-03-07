using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Path : MonoBehaviour
{
    //INSTANCE VARIABLES
    public Color lineColor;
    private List<Transform> myNodes;

    //FUNCTIONS
    public void OnDrawGizmos()
    {
        Gizmos.color = lineColor;
        Transform[] pathTransforms = GetComponentsInChildren<Transform>();
        myNodes = new List<Transform>(); 

        foreach (Transform t in pathTransforms) 
        {
            if(t != transform)
            {
                myNodes.Add(t);
            }
        }

        for(int i = 0; i < myNodes.Count; i++)
        {
            Vector3 currentNode = myNodes[i].position;
            Vector3 previousNode = Vector3.zero;
            if(i>0)
            {
                previousNode = myNodes[i-1].position;
            }
            else if(i==0 && myNodes.Count > 1)
            {
                previousNode = myNodes[myNodes.Count - 1].position;
            }

            Gizmos.DrawLine(previousNode, currentNode);
        }
    }


}
