using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "SOSpecialStatBurn", menuName = "SO/Combat/SpecialStatBurn")]
public class SOSpecialStatBurn : SOSpecialStatGeneral
{
    public override void MakeEffect(PlayerInfoComponent playerInfoComponent)
    {
        playerInfoComponent.CurrentHP -= DamagePerTurn;
        playerInfoComponent.TempsSpecialEffect--;
        Debug.Log("Play subie " + DamagePerTurn + " par sa brulure");
    }

    public override void MakeEffect(FightComonent fightComonent,EnnemiCombatUIComponent ennemiCombatUIComponent)
    {
        ennemiCombatUIComponent.TakeDamage(DamagePerTurn);
        ennemiCombatUIComponent.TempsSpecialEffet--;
    }

    
}
