using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOStanceGeneral : ScriptableObject
{
   public string Name;
   public int CoolDown;
   [TextArea]public string CoolDescription;
   [TextArea]public string PracticalDescription;
   public Sprite CoolImage;

   public virtual void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
   {
      Weapon1 = false;
      Weapon2 = false;
   }
   
   public virtual void ExecutStance(FightComonent fightComonent ,int weapon){}
}
