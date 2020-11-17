using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOObject : ScriptableObject
{
  public string Name;
  [TextArea]public string CoolDescription;
  public Sprite WorldSprite;
  public Sprite UISprite;
}
