using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class testFPSControler : MonoBehaviour
{

    public float MoveSpeed;
    public float CameraSpeed; 
    private CharacterController cc;
    private Vector2 MoveVector = new Vector2();
    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 moveVector = (transform.forward * MoveVector.y + transform.right * MoveVector.x) * Time.deltaTime *
                             MoveSpeed;
        cc.Move(moveVector);
    }

    public void Move(InputAction.CallbackContext callbackContext)
    {
        MoveVector = callbackContext.ReadValue<Vector2>();
    }

    public void Rotation(InputAction.CallbackContext callbackContext)
    {
        //transform.rotation = new Quaternion();
        //transform.Rotate(0,callbackContext.ReadValue<Vector2>().x*CameraSpeed*Time.deltaTime,0);
        Vector2 RawMovementInput = callbackContext.ReadValue<Vector2>();
        transform.Rotate(Vector3.up*RawMovementInput.x*CameraSpeed);
        
    }
}
