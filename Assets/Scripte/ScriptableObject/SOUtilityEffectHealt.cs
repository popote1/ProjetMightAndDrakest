using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityEffect", menuName = "SO/Objects/UtilityEffects/Healt") ]
public class SOUtilityEffectHealt : SOUtilityEffectGeneral
{
    public int HealtValue;
    
    public override void CombatUse(PlayerInfoComponent playerInfoComponent, FightComonent fightComonent, int itemIndex)
    {
        playerInfoComponent.CurrentHP =
            Mathf.Clamp(playerInfoComponent.CurrentHP + HealtValue, 0, playerInfoComponent.MaxHP);
        if (itemIndex == 1)
        {
            fightComonent.PlayerInfoComponent.Inventory.Remove(fightComonent.ItemData1);
        }

        if (itemIndex == 2)
        {
            fightComonent.PlayerInfoComponent.Inventory.Remove(fightComonent.ItemData2);
        }
    }

    public override void WorldUse(PlayerInfoComponent playerInfoComponent)
    {
        playerInfoComponent.CurrentHP +=
            Mathf.Clamp(playerInfoComponent.CurrentHP + HealtValue, 0, playerInfoComponent.MaxHP);
    }
}
