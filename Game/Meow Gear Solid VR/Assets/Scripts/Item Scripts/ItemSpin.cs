using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpin : MonoBehaviour
{
 public float spinSpeed = 50f; // Adjust the speed of the spin in the Inspector

    void Update()
    {
        // Rotate the GameObject around the Y axis
        transform.Rotate(0, spinSpeed * Time.deltaTime, 0);
    }
}
