using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiMaterialAnimationComponent : MonoBehaviour
{
    public MeshRenderer MeshRenderer;
    public SOEnnemiAnimation EnnemiAnimation;
    [HideInInspector] public UnityEventQueueSystem EndAnimationEvent;
    public enum AnimationOrientation
    {
        Front,Left,Right,Back
    }
    public enum AnimationType
    {
        Idel,Walk,Attack,Die
    }

    private AnimationOrientation _animationOrientation = AnimationOrientation.Front;
    private AnimationType _animationType = AnimationType.Idel;
    private List<Material> _actualAnimation;
    public float _actualFrameRate;
    private int _indexAnimation=0;
    private float _timer;
    private bool _isLooping=true;

    private void Start()
    {
        if (EnnemiAnimation.FrontIdelAnimations != null)
        {
            _actualAnimation = EnnemiAnimation.FrontIdelAnimations;
            _actualFrameRate = EnnemiAnimation.FrontIdelFrameRate;
            MeshRenderer.material = _actualAnimation[_indexAnimation];
            _indexAnimation++;
        }
    }
    
    void Update()
    {
        if (_actualAnimation != null)
        {
            _timer += Time.deltaTime;
            if (_timer >= _actualFrameRate)
            {
                MeshRenderer.material = _actualAnimation[_indexAnimation];
                _indexAnimation++;
                if (_indexAnimation >= _actualAnimation.Count)
                {
                    if (_isLooping)
                    {
                        _indexAnimation = 0;    
                    }
                    else
                    {
                        _actualAnimation = null;
                    }
                    
                }
                _timer = 0;
            }
        }
    }

    public void ChangeOriantation(AnimationOrientation animationOrientation)
    {
        _animationOrientation = animationOrientation;
        ChangeAniamation();
    }

    public void ChangeAnimationtype(AnimationType animationType)
    {
        if (_animationType != animationType)
        {
            _animationType = animationType;
            ChangeAniamation();
        }
    }

    private void ChangeAniamation()
    {
        switch (_animationType)
        {
            case AnimationType.Idel :
                switch (_animationOrientation)
                {
                    case AnimationOrientation.Front :
                        _actualAnimation = EnnemiAnimation.FrontIdelAnimations;
                        _actualFrameRate = EnnemiAnimation.FrontIdelFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Left:
                        _actualAnimation = EnnemiAnimation.LeftIdelAnimations;
                        _actualFrameRate = EnnemiAnimation.LeftIdelFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Right:
                        _actualAnimation = EnnemiAnimation.RightIdelAnimations;
                        _actualFrameRate = EnnemiAnimation.RightIdelFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Back:
                        _actualAnimation = EnnemiAnimation.BackIdelAnimations;
                        _actualFrameRate = EnnemiAnimation.BackIdelFrameRate;
                        _isLooping = true;
                        break;
                }
                break;
            case AnimationType.Walk:
                switch (_animationOrientation)
                {
                    case AnimationOrientation.Front :
                        _actualAnimation = EnnemiAnimation.FrontWalkAnimations;
                        _actualFrameRate = EnnemiAnimation.FrontWalkFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Left:
                        _actualAnimation = EnnemiAnimation.LeftWalkAnimations;
                        _actualFrameRate = EnnemiAnimation.LeftIdelFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Right:
                        _actualAnimation = EnnemiAnimation.RightWalkAnimations;
                        _actualFrameRate = EnnemiAnimation.RightWalkFrameRate;
                        _isLooping = true;
                        break;
                    case AnimationOrientation.Back:
                        _actualAnimation = EnnemiAnimation.BackWalkAnimations;
                        _actualFrameRate = EnnemiAnimation.BackWalkFrameRate;
                        _isLooping = true;
                        break;
                }
                break;
            case AnimationType.Attack :
                _actualAnimation = EnnemiAnimation.AttackAnimations;
                _actualFrameRate = EnnemiAnimation.AttackFrameFate;
                _isLooping = false;
                break;
            case AnimationType.Die :
                _actualAnimation = EnnemiAnimation.DieAnimation;
                _actualFrameRate = EnnemiAnimation.DieFrameRate;
                _isLooping = false;
                break;
            
        }
        _indexAnimation = 0;
    }

    public void SetFrameRate(float newFrameRate)
    {
        _actualFrameRate = newFrameRate;
    }

    public float GetFrameRate()
    {
        return _actualFrameRate;
    }
}
