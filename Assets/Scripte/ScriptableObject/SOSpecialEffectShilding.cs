using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SOSpecialEffectShielding", menuName = "SO/Combat/SOSpecialEffect/Shielding")]
public class SOSpecialEffectShilding : SOSpecialEffectGeneral
{
    public int ShieldValue;

    public override bool CheckForUse(FightComonent fightComonent, List<EnnemiCombatUIComponent> ennemiCombatUIComponent, int weaponIndex)
    {
        return base.CheckForUse(fightComonent, ennemiCombatUIComponent, weaponIndex);
    }

    public override bool CheckForUseOnPlayer(FightComonent fightComonent)
    {
        return true;
    }

    
    public override void MakeSpecialEffectOnPlayer(FightComonent fightComonent)
    {
        Debug.Log(" Ajoute un shield lors de la stat "+fightComonent.CombatStat);
        switch (fightComonent.CombatStat)
        { 
            case 19:
                fightComonent.Ennemi1CombatUIComponent.AddShield(ShieldValue);
                break;
            case 29:
                fightComonent.Ennemi2CombatUIComponent.AddShield(ShieldValue);
                break;
            case 49 :
                fightComonent.Ennemi3CombatUIComponent.AddShield(ShieldValue);
                break;
        }
    }
}
