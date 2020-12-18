using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "SOSpecialEffectAddWeapon", menuName = "SO/Combat/SOSpecialEffect/AddWeapon")]
public class SOSpecialEffectAddWeapon : SOSpecialEffectGeneral
{
    public List<SOObject> PossibleWeapons;
    
    public override bool CheckForUse(FightComonent fightComonent, List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        return true;
    }
    public override void MakeSpecialEffect(FightComonent fightComonent,List<EnnemiCombatUIComponent> ennemiCombatUIComponents, int weaponIndex)
    {
        int index = Random.Range(0, PossibleWeapons.Count - 1);
      fightComonent.PlayerInfoComponent.Inventory.Add(new ItemData(PossibleWeapons[index]));
    }
}
