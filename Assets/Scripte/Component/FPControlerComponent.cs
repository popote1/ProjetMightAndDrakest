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
    [Header("WalkEffects")]
    public float StepTime;
    public GameObject Camera;
    public AnimationCurve easeType;
    public AudioClip StepSound;
    [Range(0,1)]public float StepVolume=1;

    private float _Ypos;
    

    private float _stepTimer;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        _Ypos = transform.position.y;
    }
    
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
       transform.position = new Vector3(transform.position.x,_Ypos,transform.position.z);

       if (_stepTimer != 0)
       {
           _stepTimer += Time.deltaTime;
           if (_stepTimer >= StepTime)
           {
               if (_movement.magnitude != 0)
               {
                   SoundManager.PlaySound(StepSound, StepVolume);
                   
               }
               LeanTween.moveY(Camera, Camera.transform.position.y+0.1f, StepTime).setEase(easeType);
               _stepTimer = 0;
           }
       }
       else
       {
           if (_movement.magnitude != 0)
           {
               _stepTimer += Time.deltaTime;
           }
       }
   }
}
