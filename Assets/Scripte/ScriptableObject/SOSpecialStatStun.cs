using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "SOSpecialStatStun", menuName = "SO/Combat/SpecialStatStun")]
public class SOSpecialStatStun : SOSpecialStatGeneral
{
   public override void MakeEffect(PlayerInfoComponent playerInfoComponent)
   {
     
   }

   public override void MakeEffect(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent)
   {
      if (fightComonent.CombatStat == 10)
      {
         fightComonent.CombatStat = 19;
      }
      if (fightComonent.CombatStat == 20)
      {
         fightComonent.CombatStat = 29;
      }
      if (fightComonent.CombatStat == 30)
      {
         fightComonent.CombatStat = 49;
      }
      ennemiCombatUIComponent.TempsSpecialEffet--;
   }
}
