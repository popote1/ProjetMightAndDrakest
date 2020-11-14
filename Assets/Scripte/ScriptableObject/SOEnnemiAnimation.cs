using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = "EnnemieAnimation",menuName = "SO/SOEnnemiAnnimation")]
public class SOEnnemiAnimation : ScriptableObject
{

    [Header("Idel Animations")]
    public List<Material> FrontIdelAnimations;
    public float FrontIdelFrameRate;
    public List<Material> LeftIdelAnimations;
    public float LeftIdelFrameRate;
    public List<Material> RightIdelAnimations;
    public float RightIdelFrameRate;
    public List<Material> BackIdelAnimations;
    public float BackIdelFrameRate;
    [Header("Walk Animations")] 
    public List<Material> FrontWalkAnimations;
    public float FrontWalkFrameRate;
    public List<Material> LeftWalkAnimations;
    public float LeftWalkFrameRate;
    public List<Material> RightWalkAnimations;
    public float RightWalkFrameRate;
    public List<Material> BackWalkAnimations;
    public float BackWalkFrameRate;
    [Header("Combat Animations")] 
    public List<Material> AttackAnimations;
    public float AttackFrameFate;
    public List<Material> DieAnimation;
    public float DieFrameRate;
}
