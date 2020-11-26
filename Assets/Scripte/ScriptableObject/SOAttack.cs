using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "SOAttack" , menuName = "SO/Ennemi/SOAttack")]
public class SOAttack : ScriptableObject
{
   public string Name;
   public int Damage;
   public int ChanceToHit;
   public SOSpecialEffectGeneral SpecialEffect;
}
