using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu( fileName = "SOStanceCoupDeBouclar", menuName = "SO/Stance/CoupDeBouclar")]
public class SOStanceCoupDeBouclar : SOStanceGeneral
{
    public override void CheckForUse(FightComonent fightComonent, out bool Weapon1, out bool Weapon2)
    {
        Weapon1 = false;
        Weapon2 = false;
        if (fightComonent.ItemData1.SoObject is SOShield)
        {
             Weapon1 = true;
        }
        if (fightComonent.ItemData2.SoObject is SOShield)
        {
             Weapon2 = true;
        }
        
    }

    public override void ExecutStance(FightComonent fightComonent, int weaponIndex)
    {
        SOShield shield;
        bool isTwoHanded=false;
        if (weaponIndex == 1)
        {
            shield= (SOShield) fightComonent.ItemData1.SoObject;
            isTwoHanded = shield.IsTwoHanded;
        }
        else
        { 
            shield = (SOShield) fightComonent.ItemData2.SoObject;
        }
        fightComonent.AddShield(shield.ShieldValue);
        Instantiate(shield.FX,fightComonent.CenterScreen.position, Quaternion.identity);
        SoundManager.PlaySound(shield.AudioClip,shield.volume);
        int damage = 5;
        if (isTwoHanded) damage = damage * 2;
        List<EnnemiCombatUIComponent> targets;
        targets = fightComonent.ChoseTarget(weaponIndex,FightComonent.AttackTarget.Front);
        foreach (var target in targets)
        {
            target.TakeDamage(damage);
        }
        fightComonent.SelectedStance.CoolDown = fightComonent.SelectedStance.SoStance.CoolDown;
    }
}
