using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AnimationTester : MonoBehaviour
{
    public EnnemiMaterialAnimationComponent EnnemiMaterialAnimationComponent;

    [Header("UI")] 
    public TMP_Text SOAnimationName;
    public TMP_Dropdown DropdownOriantation;
    public TMP_InputField InputFieldFrameRate;
    private SOEnnemiAnimation _sOEnnemiAnimation;
    private float _saveFrameRate;
    void Start()
    {
        if (EnnemiMaterialAnimationComponent.EnnemiAnimation != null)
        {
            SetNewSOAnimation();
        }
    }
    
    void Update()
    {
        if (EnnemiMaterialAnimationComponent.EnnemiAnimation != _sOEnnemiAnimation)
        {
            SetNewSOAnimation();
        }
        
    }

    public void SetNewSOAnimation()
    {
        SOAnimationName.text = EnnemiMaterialAnimationComponent.EnnemiAnimation.name;
        _sOEnnemiAnimation = EnnemiMaterialAnimationComponent.EnnemiAnimation;
        UIAnimationIdel();
        DropdownOriantation.value = 0;
        UIChangeOriantation();
        NewFrameRate();
    }

    public void UIAnimationIdel()
    {
        EnnemiMaterialAnimationComponent.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Idel);
        NewFrameRate();
    }

    public void UIAnimationWalk()
    {
        EnnemiMaterialAnimationComponent.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Walk);
        NewFrameRate();
    }
    public void UIAnimationAttack()
    {
        EnnemiMaterialAnimationComponent.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Attack);
        NewFrameRate();
    }
    public void UIAnimationDie()
    {
        EnnemiMaterialAnimationComponent.ChangeAnimationtype(EnnemiMaterialAnimationComponent.AnimationType.Die);
        NewFrameRate();
    }

    public void UIChangeOriantation()
    {
        switch (DropdownOriantation.value)
        {case 0:
            EnnemiMaterialAnimationComponent.ChangeOriantation(EnnemiMaterialAnimationComponent.AnimationOrientation.Front);
            NewFrameRate();
            break;
        case 1 :
            EnnemiMaterialAnimationComponent.ChangeOriantation(EnnemiMaterialAnimationComponent.AnimationOrientation.Left);
            NewFrameRate();
            break;
        case 2:
            EnnemiMaterialAnimationComponent.ChangeOriantation(EnnemiMaterialAnimationComponent.AnimationOrientation.Right);
            NewFrameRate();
            break;
        case 3 :
            EnnemiMaterialAnimationComponent.ChangeOriantation(EnnemiMaterialAnimationComponent.AnimationOrientation.Back);
            NewFrameRate();
            break;
            
        }
    }

    public void UIChangeFrameRate()
    {
        try
        {
            float newFrameRate = float.Parse(InputFieldFrameRate.text);
            EnnemiMaterialAnimationComponent._actualFrameRate = newFrameRate;
        }
        catch
        {
            InputFieldFrameRate.text = ""+_saveFrameRate;
        }
    }

    private void NewFrameRate()
    {
        InputFieldFrameRate.text = ""+EnnemiMaterialAnimationComponent.GetFrameRate();
        _saveFrameRate =EnnemiMaterialAnimationComponent.GetFrameRate();
    }

    public void UISaveFrameRate()
    {
        EnnemiMaterialAnimationComponent.EnnemiAnimation.FrontIdelFrameRate= float.Parse(InputFieldFrameRate.text);
    }
}
