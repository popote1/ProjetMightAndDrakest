using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiInfo 
{
   public string Name;
   public SOEnnemi SoEnnemi;
   public int CurrentHP;

   public EnnemiInfo(SOEnnemi soEnnemi)
   {
      Name = soEnnemi.Name;
      SoEnnemi = soEnnemi;
      CurrentHP = soEnnemi.MaxHP;
   }
}
