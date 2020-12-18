using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "SOStanceFeinte", menuName = "SO/Stance/Feinte")]
public class SOStanceFeinte : SOStanceGeneral
{
    
    public override void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
    {
        Weapon1 = false;
        Weapon2 = false;
        if (fightComonent.ItemData1.SoObject is SOWeapon)
        {
            Weapon1 = true;
        }
        if (fightComonent.ItemData2.SoObject is SOWeapon)
        {
            Weapon2 = true;
        }
        
    }

    public override void ExecutStance(FightComonent fightComonent, int weaponIndex)
    {
        SOWeapon weapon;
        List<EnnemiCombatUIComponent> targets;
        if (weaponIndex == 1)
        {
            weapon = (SOWeapon) fightComonent.ItemData1.SoObject;
        }
        else
        {
             weapon = (SOWeapon) fightComonent.ItemData2.SoObject;
        }

        int damage = fightComonent.PlayerStandardAttack(weapon);
        targets = fightComonent.ChoseTarget(weaponIndex, weapon.Target);
        foreach (var target in targets)
        {
            target.TakeDamage(damage-2);
            Instantiate(weapon.FX, target.shakeComponent.transform.position, Quaternion.identity);
               
        }
        if (weapon.SpecialEffect != null)
        {
            if (weapon.SpecialEffect.CheckForUse(fightComonent, targets, 1))
            {
                weapon.SpecialEffect.MakeSpecialEffect(fightComonent, targets, 1);
            }
        }
        SoundManager.PlaySound(weapon.AudioClip,weapon.volume);
        fightComonent.AddShield(3);
        fightComonent.SelectedStance.CoolDown = fightComonent.SelectedStance.SoStance.CoolDown;
    }
}
