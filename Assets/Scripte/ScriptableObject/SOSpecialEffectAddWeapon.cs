using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "SOSpecialEffectAddWeapon", menuName = "SO/Combat/SOSpecialEffect/AddWeapon")]
public class SOSpecialEffectAddWeapon : SOSpecialEffectGeneral
{
    public List<SOWeapon> PossibleWeapons;
    
    public override bool CheckForUse(FightComonent fightComonent, List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        return true;
    }
    public override void MakeSpecialEffect(FightComonent fightComonent,List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
      // fightComonent.PlayerInfoComponent.
    }
}
