using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Deplacement : MonoBehaviour
{
    public float Speed;
    public CharacterController CharacterController;
    private Vector3 movement;
    public float MouseSensibility;
    
    
   public void TestDeplace(InputAction.CallbackContext callbackContext)
    {
        Debug.Log(callbackContext.ReadValue<Vector2>());
        
         movement = new Vector3(callbackContext.ReadValue<Vector2>().x, 0, callbackContext.ReadValue<Vector2>().y);   
    }

    public void TestMouseCamera(InputAction.CallbackContext callbackContext)
    {
        this.gameObject.transform.Rotate(0f,callbackContext.ReadValue<Vector2>().x, 0f);
        Debug.Log(callbackContext.ReadValue<Vector2>() + " Test mouse ");



    }

   private void Update()
   {

       CharacterController.Move(movement * Speed * Time.deltaTime);
       
       
       
   }
}
