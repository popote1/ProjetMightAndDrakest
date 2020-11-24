using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SOSpecialEffectBurn", menuName = "SO/Combat/SOSpecialEffect/BurnEffect")]
public class SOSpecialEffectBurn : SOSpecialEffectGeneral
{
    public SOSpecialStatBurn StatBurn;

    public override bool CheckForUse(FightComonent fightComonent, EnnemiCombatUIComponent ennemiCombatUIComponent, int weaponIndex)
    {
        
        if (ennemiCombatUIComponent.SpecialStat == null&&ennemiCombatUIComponent.IsAlive)
        {
            return true;
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

    public override void MakeSpecialEffect(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent, int weaponIndex)
    {
        ennemiCombatUIComponent.SpecialStat = StatBurn;
        ennemiCombatUIComponent.TempsSpecialEffet = StatBurn.Durer;
        Debug.Log(" l'arme Inflige l'état brulure");
    }

    public override void MakeSpecialEffectOnPlayer(FightComonent fightComonent)
    {
        fightComonent.PlayerInfoComponent.SpecialStat = StatBurn;
        Debug.Log("Ennemi inflige brulure");
    }
}
