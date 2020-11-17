using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "weaponInfo",menuName = "SO/Objects/WeaponInfo") ]
public class SOWeapon : SOObject
{
    [Header("Info Weapon")] 
    public int Damage;
    public int ChanceToHit;
    public FightComonent.AttackTarget Target;
    public int Durability;
    public bool isTwoHand;
    

}
