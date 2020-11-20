using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatPanelAnimationComponent : MonoBehaviour
{
    public Vector3 SelectSize = new Vector3(1.1f,1.1f,1.1f);
    public float AnimationSpeed=0.2f;
    public void Selected()
    {
        LeanTween.scale(gameObject, SelectSize, AnimationSpeed);
    }

    public void Deselected()
    {
        LeanTween.scale(gameObject, new Vector3(0.7f, 0.7f,1), AnimationSpeed);
    }
}
