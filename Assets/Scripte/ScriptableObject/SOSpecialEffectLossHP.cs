using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "SOSpecialEffectCoupPoint", menuName = "SO/Combat/SOSpecialEffect/CoupPoint")]
public class SOSpecialEffectLossHP : SOSpecialEffectGeneral
{
    public override bool CheckForUse(FightComonent fightComonent, List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        return true;
    }
    public override void MakeSpecialEffect(FightComonent fightComonent,List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        fightComonent.PlayerInfoComponent.CurrentHP -= 1;
    }
}
