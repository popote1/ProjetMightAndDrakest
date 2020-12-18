using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "SOStanceDemacia", menuName = "SO/Stance/Demacia")]
public class SOStanceDemacia : SOStanceGeneral
{
    public override void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
    {
        Weapon1 = false;
        Weapon2 = false;
        if (fightComonent.ItemData1.SoObject is SOWeapon)
        {
            SOWeapon weapon = (SOWeapon)fightComonent.ItemData1.SoObject;
            if (weapon.isTwoHand) Weapon1 = true;
        }
        
    }

    public override void ExecutStance(FightComonent fightComonent, int weaponIndex)
    {
        List<EnnemiCombatUIComponent> targets;
        SOWeapon weapon = (SOWeapon) fightComonent.ItemData1.SoObject;
        int damage = fightComonent.PlayerStandardAttack(weapon);
        targets = fightComonent.ChoseTarget(1, weapon.Target);
        foreach (var target in targets)
        {
            target.TakeDamage(damage*2);
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
        fightComonent.SelectedStance.CoolDown = fightComonent.SelectedStance.SoStance.CoolDown;
    }
    
}
