using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SOStanceReverDuGauche", menuName = "SO/Stance/ReverDuGauche")]
public class SOStanceRevertDuGauche : SOStanceGeneral
{
    public override void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
    {
        Weapon1 = false;
        if (fightComonent.ItemData1.SoObject is SOWeapon)
        {
            Weapon1 = true;
        }
        Weapon2 = false;
    }

    public override void ExecutStance(FightComonent fightComonent, int weaponIndex)
    {
        if (weaponIndex == 1)
        {
            EnnemiCombatUIComponent target;
            SOWeapon weapon = (SOWeapon) fightComonent.ItemData1.SoObject;
            int damage = fightComonent.PlayerStandardAttack(weapon);
            target = fightComonent.ChoseTarget(1, weapon.Target);
            target.TakeDamage(damage+damage/2);
            target.TakeDamage(damage+damage/2); if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(fightComonent, target, weaponIndex))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(fightComonent, target , weaponIndex);
                }
            }
            fightComonent.ItemData1.CurrantDurability--;
            Debug.Log("La stance fait sont effet et fais"+damage+damage/2);
        }

        fightComonent.SelectedStance.CoolDown = fightComonent.SelectedStance.SoStance.CoolDown;
    }
}
