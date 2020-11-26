using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu (fileName = "ShiealdWeapon" , menuName = "SO/Objects/ShiedInfo")]
public class SOShield : SOObject
{
    public int ShieldValue;
    public int Durability;
    public bool IsTwoHanded;
    public SOSpecialEffectGeneral SpecialEffect;
}
