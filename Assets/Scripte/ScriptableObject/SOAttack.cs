using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SOAttack" , menuName = "SO/Ennemi/SOAttack")]
public class SOAttack : ScriptableObject
{
   public string name;
   public int Damage;
   public int ChanceToHit;
}
