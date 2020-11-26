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
            List<EnnemiCombatUIComponent> targets;
            SOWeapon weapon = (SOWeapon) fightComonent.ItemData1.SoObject;
            int damage = fightComonent.PlayerStandardAttack(weapon);
            targets = fightComonent.ChoseTarget(1, weapon.Target);
            foreach (var target in targets)
            {
                target.TakeDamage(damage/2);
            }
            if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(fightComonent, targets, weaponIndex))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(fightComonent, targets , weaponIndex);
                }
            }
           
            Debug.Log("La stance fait sont effet et fais"+damage+damage/2);
        }

        fightComonent.SelectedStance.CoolDown = fightComonent.SelectedStance.SoStance.CoolDown;
    }
}
