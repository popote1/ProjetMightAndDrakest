using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
   public static float volume=1;
   public static GameObject Holder;

   private void Start()
   {
       Holder = gameObject;
   }

   public static void PlaySound(AudioClip audioClip, float volume2)
   {
       AudioSource  audioSource = Holder.AddComponent<AudioSource>();
       audioSource.clip = audioClip;
       audioSource.volume = volume2 * volume;
       audioSource.Play();
       Destroy(audioSource,audioClip.length+1);
   }
}
