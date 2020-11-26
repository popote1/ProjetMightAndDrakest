using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOUtilityEffectGeneral : ScriptableObject
{
    public virtual void WorldUse(PlayerInfoComponent playerInfoComponent) {}
    public virtual void CombatUse(PlayerInfoComponent playerInfoComponent , FightComonent fightComonent, int itemIndex){}
}
