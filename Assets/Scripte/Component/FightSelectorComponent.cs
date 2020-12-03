using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

using UnityEngine.Serialization;

public class FightSelectorComponent : MonoBehaviour
{
    public float PanelScrollSpeed;
    public float SelectorMoveSpeed = 0.5f;
    public FightComonent FightComonent;
    [Header("Prefabs")]
    public GameObject NewPrefabPanel;
    public GameObject NewShieldPrefabPanel;
    public GameObject NewUtilityPanel;
    public GameObject NewStancePrefabsPanel;
    
    [Header("UI Selection")] 
    public GameObject Selector;
    public GameObject PanelObject;
    public GameObject PanelSlideObject;
    public GameObject PanelStance;
    public GameObject PanelSliderStance;
    public GameObject PanelSelectedObject1;
    public GameObject PanelSelectedObject2;
    public GameObject PanelSelectedStance;

    [HideInInspector]public bool SelectTime;
    private ScrollRect _iteamScrollRect;
    private ScrollRect _stanceScrollRect;
    private GameObject _preSelectedPanel;
    private GameObject _object1;
    private GameObject _object2;
    private GameObject _stancePanel;
    private int _selectorStat = 1;
    private GameObject _temporalHolder;
    private bool _selectModeobjects=true;
    

    private void Start()
    {
        _iteamScrollRect = PanelObject.GetComponent<ScrollRect>();
        _stanceScrollRect = PanelStance.GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (FightComonent.IsFighting)
        {
            if (_object1 != null) PanelToposition(_object1, PanelSelectedObject1.transform);
            if (_object2 != null) PanelToposition(_object2, PanelSelectedObject2.transform);
            if (_stancePanel != null) PanelToposition(_stancePanel, PanelSelectedStance.transform);
            UpDateSelectorPos();
            PreSelectPanel();
        }
    }

    public void ScrollPanel(InputAction.CallbackContext callbackContext)
    {
        if (SelectTime)
        {
            if (_selectModeobjects)
            {
                Vector2 RawValue = new Vector2(callbackContext.ReadValue<float>(), 0);
                _iteamScrollRect.velocity = RawValue * PanelScrollSpeed;
            }
            else
            {
                Vector2 RawValue = new Vector2(callbackContext.ReadValue<float>(), 0);
                _stanceScrollRect.velocity = RawValue * PanelScrollSpeed;
            }
        }
    }

    //click pour séléctionner les object et stance

    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started&&SelectTime)
        {
            if (SelectTime)
            {
                switch (_selectorStat)
                {
                    case 1:
                        _preSelectedPanel.transform.SetParent(PanelSelectedObject1.transform);
                        _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
                        if (_object1 != null)
                        {
                            
                            if (FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOWeapon)
                            {
                                SOWeapon weapon = (SOWeapon)FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                                if (weapon.isTwoHand)
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                    
                                    Destroy(_object2);
                                }
                                else
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                }
                            } else if (FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOShield)
                            {
                                SOShield weapon = (SOShield)FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                                if (weapon.IsTwoHanded)
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                    Destroy(_object2);
                                }
                                else
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                }
                            }
                            else
                            {
                                _object1.transform.SetParent(PanelSlideObject.transform);
                            }
                            
                        }
                        
                        if (FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOWeapon)
                        {
                            Debug.Log(" élément 1 avec l'index "+_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex +" et son nom est :"+FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject.Name);
                            SOWeapon weapon = (SOWeapon)FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                            if (weapon.isTwoHand)
                            {
                                _object1 = _preSelectedPanel;
                                _preSelectedPanel = null;
                                PassToNextSelectorStat(2);
                                PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                                FightComonent.ItemData1 = FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex];
                                if (_object2!= null)
                                {
                                    _object2.transform.SetParent(PanelSlideObject.transform);
                                    FightComonent.ItemData2 = null;
                                }
                                _object2= Instantiate(_object1, PanelSelectedObject2.transform);
                                return;
                            }
                        }
                        if (FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOShield)
                        {
                            SOShield shield = (SOShield)FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                            if (shield.IsTwoHanded)
                            {
                                _object1 = _preSelectedPanel;
                                _preSelectedPanel = null;
                                PassToNextSelectorStat(2);
                                PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                                FightComonent.ItemData1 = FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex];
                                if (_object2!= null)
                                {
                                    _object2.transform.SetParent(PanelObject.transform);
                                    FightComonent.ItemData2 = null;
                                    Instantiate(_object1, PanelSelectedObject2.transform);
                                }
                                return;
                            }
                        }
                        


                        _object1 = _preSelectedPanel;
                        _preSelectedPanel = null;
                        PassToNextSelectorStat(2);
                        PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                            FightComonent.ItemData1 = FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex];
                        break;
                    case 2:
                        _preSelectedPanel.transform.SetParent(PanelSelectedObject2.transform);
                        _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
                       if (_object2 != null)
                        {
                            if (FightComonent.PlayerInfoComponent.Inventory[_object2.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOWeapon)
                            {
                                SOWeapon weapon = (SOWeapon)FightComonent.PlayerInfoComponent.Inventory[_object2.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                                if (weapon.isTwoHand)
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                    _object1 = null;
                                    Destroy(_object2);
                                }
                                else
                                {
                                    _object2.transform.SetParent(PanelSlideObject.transform);
                                }
                            } else if (FightComonent.PlayerInfoComponent.Inventory[_object2.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOShield)
                            {
                                SOShield weapon = (SOShield)FightComonent.PlayerInfoComponent.Inventory[_object2.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                                if (weapon.IsTwoHanded)
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                    _object1 = null;
                                    Destroy(_object2);
                                }
                                else
                                {
                                    _object2.transform.SetParent(PanelSlideObject.transform);
                                }
                            }
                            else
                            {
                                _object2.transform.SetParent(PanelSlideObject.transform);
                            }
                        }
                         if (FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOWeapon)
                        {
                            SOWeapon weapon = (SOWeapon)FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                            if (weapon.isTwoHand)
                            {
                                Debug.Log(" c'est a deux main");
                                if (_object1 != null)
                                {
                                    _object1.transform.SetParent(PanelSlideObject.transform);
                                }
                                _object1 = _preSelectedPanel;
                                _object1.transform.SetParent(PanelSelectedObject1.transform);
                                _preSelectedPanel = null;
                                PassToNextSelectorStat(3);
                                PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                                FightComonent.ItemData1 = FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex];
                                if (_object2!= null)
                                {
                                    _object2.transform.SetParent(PanelObject.transform);
                                    FightComonent.ItemData2 = null;
                                }

                                
                                _object2 = Instantiate(_object1, PanelSelectedObject2.transform);
                                return;
                            }
                        }
                        if (FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject is SOShield)
                        {
                            SOShield shield = (SOShield)FightComonent.PlayerInfoComponent.Inventory[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
                            if (shield.IsTwoHanded)
                            {
                                _object1 = _preSelectedPanel;
                                _preSelectedPanel = null;
                                PassToNextSelectorStat(2);
                                PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                                FightComonent.ItemData1 = FightComonent.PlayerInfoComponent.Inventory[_object1.GetComponent<EditPanelComponent>().InvetoryIndex];
                                if (_object2!= null)
                                {
                                    _object2.transform.SetParent(PanelObject.transform);
                                    FightComonent.ItemData2 = null;
                                    Instantiate(_object1, PanelSelectedObject2.transform);
                                }
                                return;
                            }
                        }

                        _object2 = _preSelectedPanel;
                        _preSelectedPanel = null;
                        PassToNextSelectorStat(3);
                        PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                        FightComonent.ItemData2 =
                            FightComonent.PlayerInfoComponent.Inventory[_object2.GetComponent<EditPanelComponent>().InvetoryIndex];
                        break;
                    case 3 :
                        if (FightComonent.Stances[_preSelectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex]
                            .CoolDown == 0)
                        {
                            _preSelectedPanel.transform.SetParent(PanelSelectedStance.transform);
                            _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
                            if (_stancePanel != null)
                            {
                                _stancePanel.transform.SetParent(PanelSliderStance.transform);
                            }

                            _stancePanel = _preSelectedPanel;
                            _preSelectedPanel = null;
                            PassToNextSelectorStat(1);
                            PanelSliderStance.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                            FightComonent.SelectedStance =
                                FightComonent.Stances[_stancePanel.GetComponent<EditPanelComponent>().InvetoryIndex];
                            if (_object1 != null && _object2 != null)
                            {
                                FightComonent.StartCombat();
                            }
                            else
                            {
                                PassToNextSelectorStat(1);
                            }
                        }

                        break;
                }
            }
        }
    }

    public void MoveSelector(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started&&SelectTime)
        {
            switch (_selectorStat)
            {
                case 1:
                    if (callbackContext.ReadValue<Vector2>().x == 1) PassToNextSelectorStat(3);
                    break;
                case 2 :
                    if (callbackContext.ReadValue<Vector2>().x == -1) PassToNextSelectorStat(3);
                    if (callbackContext.ReadValue<Vector2>().x == 1&& FightComonent.ImgSpecialStat.IsActive()) PassToNextSelectorStat(4);
                    break;
                case 3:
                    if (callbackContext.ReadValue<Vector2>().x ==-1) PassToNextSelectorStat(1);
                    if (callbackContext.ReadValue<Vector2>().x == 1) PassToNextSelectorStat(2);
                    break;
                case 4:
                    if (callbackContext.ReadValue<Vector2>().x == -1) PassToNextSelectorStat(2);
                    break;
            }
            Debug.Log(callbackContext.ReadValue<Vector2>());
        }
    }

    public void LoadIteamBar()
    {
        foreach (ItemData item in FightComonent.PlayerInfoComponent.Inventory)
        {
            
            if (item.SoObject is SOWeapon)
            {
                GameObject newObjectPanel = Instantiate(NewPrefabPanel, PanelSlideObject.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = FightComonent.PlayerInfoComponent.Inventory.IndexOf(item);
                SOWeapon soWeapon = (SOWeapon) item.SoObject;
                editPanelComponent.TxtDamage.text = ""+soWeapon.Damage;
                editPanelComponent.TxtCHT.text = "" + soWeapon.ChanceToHit;
                editPanelComponent.TxtDurability.text = item.CurrantDurability + "/" + soWeapon.Durability;
                switch (soWeapon.Target)
                {
                    case FightComonent.AttackTarget.Front :
                        editPanelComponent.TxtTarget.text = "Front";
                        break;
                    case FightComonent.AttackTarget.Invest:
                        editPanelComponent.TxtTarget.text = "Invert";
                        break;
                    case FightComonent.AttackTarget.Back:
                        editPanelComponent.TxtTarget.text = "Back";
                        break;
                    case FightComonent.AttackTarget.Splash:
                        editPanelComponent.TxtTarget.text = "Splash";
                        break;
                }
                if (soWeapon.SpecialEffect != null)
                {
                }
                else
                {
                    editPanelComponent.TxtSpecialEffect.enabled = false;
                    editPanelComponent.TxtTitleSpecialEffect.enabled = false;
                }
                
                //Vector2 size =PanelSlideObject.GetComponent<RectTransform>().sizeDelta;
                
            }

            if (item.SoObject is SOShield)
            {
                GameObject newObjectPanel = Instantiate(NewShieldPrefabPanel, PanelSlideObject.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = FightComonent.PlayerInfoComponent.Inventory.IndexOf(item);
                SOShield soShield = (SOShield) item.SoObject;
                editPanelComponent.TxtDamage.text = "" + soShield.ShieldValue;
                editPanelComponent.TxtDurability.text = item.CurrantDurability + "/" + soShield.Durability;
                if (soShield.SpecialEffect != null)
                {
                }
                else
                {
                    editPanelComponent.TxtSpecialEffect.enabled = false;
                }
            }

            if (item.SoObject is SOUtilityItem)
            {
                GameObject newObjectPanel = Instantiate(NewUtilityPanel, PanelSlideObject.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = FightComonent.PlayerInfoComponent.Inventory.IndexOf(item);
                SOUtilityItem soUtilityItem = (SOUtilityItem) item.SoObject;
                editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            }
            PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
        }
    }

    public void LoadStancePanel()
    {
        DestroyStances();
        foreach (StanceInfo stance in FightComonent.Stances)
        {
            GameObject newObjectPanel = Instantiate(NewStancePrefabsPanel, PanelSliderStance.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = stance.SoStance.Name;
            editPanelComponent.TxtDurability.text = 0+ "";
            editPanelComponent.TxtSpecialEffect.text = stance.SoStance.PracticalDescription;
            editPanelComponent.InvetoryIndex = FightComonent.Stances.IndexOf(stance);
        }
       /* foreach (var stanceGeneral in FightComonent.PlayerInfoComponent.SOStance)
        {
            GameObject newObjectPanel = Instantiate(NewStancePrefabsPanel, PanelSliderStance.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = stanceGeneral.Name;
            editPanelComponent.TxtDurability.text = 0+ "";
            editPanelComponent.TxtSpecialEffect.text = stanceGeneral.PracticalDescription;
            editPanelComponent.InvetoryIndex = FightComonent.PlayerInfoComponent.SOStance.IndexOf(stanceGeneral);
            FightComonent.Stances.Add(new StanceInfo(stanceGeneral));
            
        }*/
        PanelSliderStance.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
    }

    private void ReloadStancePannel()
    {
        Debug.Log("Reset Panel "+FightComonent.Stances.Count);
        foreach (StanceInfo stance in FightComonent.Stances)
        {
            Debug.Log("nouveau panel stance");
            GameObject newObjectPanel = Instantiate(NewStancePrefabsPanel, PanelSliderStance.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = stance.SoStance.Name;
            editPanelComponent.TxtDurability.text = stance.CoolDown+ "";
            editPanelComponent.TxtSpecialEffect.text = stance.SoStance.PracticalDescription;
            editPanelComponent.InvetoryIndex = FightComonent.Stances.IndexOf(stance);
        }
        PanelSliderStance.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
       
    }

    private void UpDateSelectorPos()
    {
        switch (_selectorStat)
        {
            case 1:
                Selector.transform.position = Vector3.Lerp(Selector.transform.position,
                    PanelSelectedObject1.transform.position, SelectorMoveSpeed);
                break;
            case 2:
                Selector.transform.position = Vector3.Lerp(Selector.transform.position,
                    PanelSelectedObject2.transform.position, SelectorMoveSpeed);
                break;
            case 3:
                Selector.transform.position = Vector3.Lerp(Selector.transform.position,
                    PanelSelectedStance.transform.position, SelectorMoveSpeed);
                break;
            case 4:
                Selector.transform.position = Vector3.Lerp(Selector.transform.position,
                    FightComonent.PanelInfoSpecialStat.transform.position, SelectorMoveSpeed);
                break;
        }
    }

    private void PreSelectPanel()
    {
        if (_selectModeobjects)
        {
            float distanceMin = 1000;
            foreach (RectTransform item in PanelSlideObject.transform)
            {
                if (distanceMin > (item.position - Selector.transform.position).magnitude)
                {
                    distanceMin = (item.position - Selector.transform.position).magnitude;
                    _temporalHolder = item.gameObject;
                }
            }
        }
        else
        {
            float distanceMin = 1000;
            foreach (RectTransform item in PanelSliderStance.transform)
            {
                if (distanceMin > (item.position - Selector.transform.position).magnitude)
                {
                    distanceMin = (item.position - Selector.transform.position).magnitude;
                    _temporalHolder = item.gameObject;
                }
            }
        }

        if (_preSelectedPanel != _temporalHolder)
        {
            if (_preSelectedPanel != null)
            {
                _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
            }
            //_preSelectedPanel?.GetComponent<CombatPanelAnimationComponent>().Deselected();
                _temporalHolder.GetComponent<CombatPanelAnimationComponent>().Selected();
                _preSelectedPanel = _temporalHolder; 
        } 
        
    }

    public void ResetPannels()
    {
        foreach (Transform item in PanelSlideObject.transform)
        {
            Destroy(item.gameObject , 0.01f);
        }

        foreach (StanceInfo stance in FightComonent.Stances)
        {
            stance.CoolDown--;
            if (stance.CoolDown < 0)
            {
                stance.CoolDown = 0;
            }
        }

        foreach (Transform stance in PanelSliderStance.transform)
        {
            Destroy(stance.gameObject,0.01f);
        }
        Destroy(_object1);
        Destroy(_object2);
        Destroy(_stancePanel);
        
    }

    public void SetPanels()
    {
        LoadIteamBar();
        ReloadStancePannel();
    }

    private void PanelToposition(GameObject objectMove , Transform destination)
    {
        objectMove.transform.position = Vector3.Lerp(objectMove.transform.position, destination.position, 0.5f);
    }

    private void PassToNextSelectorStat(int newStat)
    {
        switch (_selectorStat)
        {
            case 1:
                if (newStat==2) _selectorStat = 2;
                if (newStat == 3)
                {
                    _selectorStat = 3;
                    ChangeSliderPanel();
                }
                break;
            case 2:
                if (newStat==4) _selectorStat = 4;
                if (newStat == 3)
                {
                    _selectorStat = 3;
                    ChangeSliderPanel();
                }
                break;
            case 3:
                if (newStat == 1)
                {
                    _selectorStat = 1;
                    ChangeSliderPanel();
                }
                if (newStat == 2)
                {
                    _selectorStat = 2;
                    ChangeSliderPanel();
                }
                break;
            case 4 :
                if (newStat== 2) _selectorStat = 2;
                break;
        }
    }

    private void ChangeSliderPanel()
    {
        PanelObject.GetComponent<CombatSliderAnimationComponant>().ChangePosition(); 
        PanelStance.GetComponent<CombatSliderAnimationComponant>().ChangePosition();
        _selectModeobjects = !_selectModeobjects;
    }

    public void DestroyObject(ItemData item)
    {
        Debug.Log("L'item "+item.SoObject.Name + " s'est casser");
        FightComonent.PlayerInfoComponent.Inventory.Remove(item);
    }

    public void DestroyStances()
    {
        foreach (SOStanceGeneral stance in FightComonent.PlayerInfoComponent.SOStance)
        {
            FightComonent.Stances.Add(new StanceInfo(stance));
        }
    }

    public void DestroySelecterdObject()
    {
        Destroy(_object1);
        Destroy(_object2);
        Destroy(_stancePanel);
    }
    
}
