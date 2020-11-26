using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOSpecialEffectStun", menuName = "SO/Combat/SOSpecialEffect/StunEffect")]
public class SOSpecialEffetStun : SOSpecialEffectGeneral
{
    public SOSpecialStatStun StatStun;

    public override bool CheckForUse(FightComonent fightComonent,
        List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        foreach (var ennemiCombatUIComponent in ennemiCombatUIComponents)
        {
            if (ennemiCombatUIComponent.SpecialStat == null && ennemiCombatUIComponent.IsAlive)
            {
                if (ennemiCombatUIComponent.SpecialStat == null && ennemiCombatUIComponent.IsAlive)
                {
                    return true;
                }
            }
        }

        return false;
        
    }


public override bool CheckForUseOnPlayer(FightComonent fightComonent)
    {
        if (fightComonent.PlayerInfoComponent.SpecialStat==null)
        {
            Debug.Log("L'ennemi n'a pas de statueSpecial");
            return true;
        }
        return false;
    }

    public override void MakeSpecialEffect(FightComonent fightComonent, List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        foreach (var ennemiCombatUIComponent in ennemiCombatUIComponents)
        {
            if (ennemiCombatUIComponent.SpecialStat == null && ennemiCombatUIComponent.IsAlive)
            {
                ennemiCombatUIComponent.SpecialStat = StatStun;
                ennemiCombatUIComponent.TempsSpecialEffet = StatStun.Durer;
                Debug.Log(" l'arme Inflige l'état brulure");
            }
        }
    }
}
