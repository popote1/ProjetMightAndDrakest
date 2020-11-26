using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu( fileName = "SOStanceChanceDuDroit", menuName = "SO/Stance/ChaceDuDroite")]
public class SOStanceChaceDuDroite : SOStanceGeneral
{
    public override void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
    {
        Weapon2 = false;
        if (fightComonent.ItemData2.SoObject is SOWeapon)
        {
            Weapon2 = true;
        }
        Weapon1 = false;
    }

    public override void ExecutStance(FightComonent fightComonent, int weaponIndex)
    {
        if (weaponIndex == 2)
        {
            List<EnnemiCombatUIComponent> targets;
            SOWeapon weapon = (SOWeapon) fightComonent.ItemData2.SoObject;
            int damage = fightComonent.PlayerStandardAttack(weapon);
            targets = fightComonent.ChoseTarget(2, weapon.Target);
            foreach (var target in targets)
            {
                target.TakeDamage(damage);
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
