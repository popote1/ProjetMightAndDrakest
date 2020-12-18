using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuComponent : MonoBehaviour
{
   public HUDComponent HudComponent;
   public GameObject CameraMenu;
   public CanvasGroup canvasGroup;
   public AudioClip ExploratingMusic;
   private bool MoveCam;

   public void UIPressPlay()
   {
      MoveCam = true;
      SoundManager.PlayMusic(ExploratingMusic,3);
   }

   private void Start()
   {
      SoundManager.ChangeMusicVolume(0.5f);
   }

   public void Update()
   {
      if (MoveCam)
      {
         CameraMenu.transform.position =Vector3.Lerp(CameraMenu.transform.position,HudComponent.Camera.transform.position , 0.05f );
         CameraMenu.transform.eulerAngles = Vector3.Slerp(CameraMenu.transform.eulerAngles,HudComponent.Camera.transform.eulerAngles , 0.05f);
         canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 0, 0.08f);
         Invoke("ChangeCam",3f);
         
      }
   }

   private void ChangeCam()
   {
      CameraMenu.SetActive(false);
      HudComponent.ExitMainMenu();
      MoveCam = false;
      
   }
}
