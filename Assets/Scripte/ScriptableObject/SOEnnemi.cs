using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "SoEnnemi", menuName = "SO/Ennemi/SOEnnemi")]
public class SOEnnemi :ScriptableObject
{
    public string Name;
    public int MaxHP;
    public int Strenght;
    public int Dexterity;
    public List<EnnemiAttack> Attacks;
    public SOEnnemiAnimation SoEnnemiAnimation;

}
