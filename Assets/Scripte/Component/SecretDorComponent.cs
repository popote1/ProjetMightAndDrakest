using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecretDorComponent : MonoBehaviour
{
    public AudioClip OpenSoundClip;
    [Range(0, 1)] public float OpenSoundVolume = 1; 
    public void OpenDoor()
    {
        SoundManager.PlaySound(OpenSoundClip,OpenSoundVolume);
        LeanTween.moveY(gameObject, 2.8f, 0.5f);
    }
}
