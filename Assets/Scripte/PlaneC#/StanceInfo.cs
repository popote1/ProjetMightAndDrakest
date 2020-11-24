using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StanceInfo 
{
   public SOStanceGeneral SoStance;
   public int CoolDown;

   public StanceInfo(SOStanceGeneral soStance)
   {
      SoStance = soStance;
      CoolDown = 0;
   }
}
