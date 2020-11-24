using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SOSpecialStatGeneral : ScriptableObject
{
    public string Name;
    public Sprite Sprite;
    [TextArea]public string Description;
    public int Durer;
    public int DamagePerTurn;
    
    public virtual void MakeEffect(PlayerInfoComponent playerInfoComponent){}
    public virtual void MakeEffect(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent){}
}
