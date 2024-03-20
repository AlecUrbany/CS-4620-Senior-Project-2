using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity_Player : MonoBehaviour{
    public Camera CameraReference;
    public Entity_Statistics EntityStatistics;
    public Transform Transform;
    public GameObject Cat;
    // Start is called before the first frame update

    void Awake(){
        EntityStatistics = new Entity_Statistics();

        CameraReference = Camera.main;

        EntityStatistics.Jumps = 1;
        EntityStatistics.MovementSpeed = 5.0f;

        Transform = Cat.GetComponent<Transform>();
    }

    void Start(){
        
    }

    // Update is called once per frame
    void Update(){
        Quaternion Rotation = Quaternion.Euler(0.0f, CameraReference.transform.localEulerAngles.y, 0.0f);

        Transform.rotation = Rotation;
    }
}
