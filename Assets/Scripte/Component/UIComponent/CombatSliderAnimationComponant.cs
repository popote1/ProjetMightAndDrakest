using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CombatSliderAnimationComponant : MonoBehaviour
{
    public Vector3 NewPosition;
    public float AnimationTime;
    private bool _isInOriginalPos = true;
    private Vector3 _originalPosition;

    private void Start()
    {
        _originalPosition = transform.position;
    }

    public void ChangePosition()
    {
        if (_isInOriginalPos)
        {
            LeanTween.moveY(gameObject, NewPosition.y, AnimationTime);
            _isInOriginalPos= false;
        }
        else
        {
            LeanTween.moveY(gameObject, _originalPosition.y, AnimationTime);
            _isInOriginalPos= true;
        }
    }
}
