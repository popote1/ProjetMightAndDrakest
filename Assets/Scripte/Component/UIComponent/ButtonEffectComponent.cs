using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonEffectComponent : MonoBehaviour
{
    [Header("Sounds")] 
    public AudioClip SelectedClip;
    [Range(0, 1)] public float SelectedVolume;
    public AudioClip UseClip;
    [Range(0, 1)] public float UseVolume;

    [Header("Annimation")] 
    public AnimationCurve AnimationCurve;
    public float ScaleFactor = 0.2f;
    public float AnimationTime=0.2f;

    private Vector3 _originalScale=Vector3.zero;

    private void OnEnable()
    {
        if(_originalScale==Vector3.zero){ _originalScale = transform.localScale;}
    }


    public void OnSelected()
    {
        SoundManager.PlaySound(SelectedClip,SelectedVolume);
        LeanTween.scale(gameObject, _originalScale+Vector3.one * ScaleFactor, AnimationTime).setEase(AnimationCurve);
    }

    public void OnDeselect()
    {
        LeanTween.scale(gameObject, _originalScale, AnimationTime).setEase(AnimationCurve);
    }

    public void OnUse()
    {
        SoundManager.PlaySound(UseClip,UseVolume);
    }
}
