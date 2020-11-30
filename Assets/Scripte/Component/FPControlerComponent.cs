using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class FPControlerComponent: MonoBehaviour
{
    public float Speed;
    public CharacterController CharacterController;
    private Vector2 _movement;
    private Vector3 _mouseRotation;
    public float MouseSensibility;
    
    
   public void TestDeplace(InputAction.CallbackContext callbackContext)
    {
       // Debug.Log(callbackContext.ReadValue<Vector2>());

        _movement = callbackContext.ReadValue<Vector2>();
        //_movement = transform.forward * callbackContext.ReadValue<Vector2>().y +transform.right * callbackContext.ReadValue<Vector2>().x;
    }

    public void TestMouseCamera(InputAction.CallbackContext callbackContext)
    {
        transform.Rotate(0f,callbackContext.ReadValue<Vector2>().x*Time.deltaTime*MouseSensibility, 0f);
      //  Debug.Log(callbackContext.ReadValue<Vector2>() + " Test mouse ");
    }

   private void Update()
   {
       CharacterController.Move((transform.forward*_movement.y+transform.right*_movement.x)*Speed*Time.deltaTime);
   }
}
