using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOObject : ScriptableObject
{
  public string Name;
  [TextArea]public string CoolDescription;
  public Material WorldSprite;
  public Sprite UISprite;
  public GameObject FX;
  public AudioClip AudioClip;
  public float volume;
  
}
