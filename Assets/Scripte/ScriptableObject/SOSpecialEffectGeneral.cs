using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOSpecialEffectGeneral :ScriptableObject
{

    public virtual bool CheckForUse(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent, int weaponIndex)
    {
        return false;
    }
    public virtual bool CheckForUseOnPlayer(FightComonent fightComonent)
    {
        return false;
    }

    public virtual void MakeSpecialEffect(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent, int weaponIndex)
    {
        
    }
    public virtual void MakeSpecialEffectOnPlayer(FightComonent fightComonent)
    {
        
    }

}
