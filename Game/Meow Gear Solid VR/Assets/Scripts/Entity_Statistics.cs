using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Entity_Statistics{
    public Entity_Statistics(){
        Jumps = 0;
        MovementSpeed = 0.0f;
    }

    public int Jumps;
    public float MovementSpeed;
}
