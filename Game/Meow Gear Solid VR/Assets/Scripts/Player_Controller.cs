using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player_Controller : Controller{
    //public InputAction InputSystem;
    public Rigidbody RigidBody;
    Vector2 Direction = Vector2.zero;
    float MovementSpeed = 50.0f;

    private void Awake(){
        Player_Input PlayerInput = new Player_Input();
        PlayerInput.Player.Enable();
        PlayerInput.Player.Jump.performed += Jump;
        PlayerInput.Player.Move.performed += Move;
    }

    void Start(){
    }

    private void OnEnable(){
        //InputSystem.Enable();
    }

    private void OnDisable(){
        //InputSystem.Disable();
    }

    void Update(){
        //Direction = InputSystem.ReadValue<Vector2>();
    }

    private void FixedUpdate(){
        //RigidBody.velocity = new Vector2(Direction.x * MovementSpeed, Direction.y * MovementSpeed);
    }

    public void Jump(InputAction.CallbackContext Context){
        if (Context.performed){
            Debug.Log("Pressed");
            RigidBody.AddForce(Vector3.up * 5.0f, ForceMode.Impulse);
        }
    }

    public void Move(InputAction.CallbackContext Context){
        Vector2 Input = Context.ReadValue<Vector2>();
        RigidBody.AddForce(new Vector3(Input.y * -1.0f, 0.0f, Input.x) * MovementSpeed, ForceMode.Force);
    }
}
