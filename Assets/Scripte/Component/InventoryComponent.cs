using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

public class InventoryComponent : MonoBehaviour
{
    public GameObject NewWeaopnPanel;
    public GameObject NewUtilityPanel;
    public GameObject NewShealdPanel;
    public GameObject NewStancePanel;
    public PlayerInfoComponent playerInfoComponent;
    public float PanelScrollSpeed;
    public Button ItemButton;
    [Header("Panels")] 
    public GameObject PanelDescriptionWeapon;
    public GameObject PanelDescriptionShield;
    public GameObject PanelDescriptionUtility;
    public GameObject PanelDescriptionStance;
    public GameObject PanelDescriptionOption;
    public GameObject PanleButtonStandard;
    public GameObject PanleButtonOption;
    public GameObject PanleButtonIteam;
    public GameObject PanelSliderItems;
    public GameObject PanelSliderStances;
    [Header("PlayerPanel")] 
    public Slider SliderPlayerHP;
    public Image ImgPlayerSpecialStat;
    public TMP_Text TxtPlayerStrenght;
    public TMP_Text TxtPlayerDexterity;
    public TMP_Text TxtPlayerHP;
    public TMP_Text TxtPlayerSpecialStatName;
    public TMP_Text TxtPlayerSpecialStatDescription;
    [Header("DescriptionPanelWeapon")] 
    public TMP_Text TxtWeaponName;
    public TMP_Text TxtWeaponDamage;
    public TMP_Text TxtWeaponPrecision;
    public TMP_Text TxtWeaponDurability;
    public TMP_Text TxtWeaponTarget;
    public TMP_Text TxtWeaponCoolDescription;
    public TMP_Text TxtWeaponSpecialEffect;
    public TMP_Text TxtWeaponTwoHanded;
    public Image ImgWeaponIcon;
    [Header("DescriptionPanelShield")] 
    public TMP_Text TxtShieldName;
    public TMP_Text TxtShieldDurability;
    public TMP_Text TxtShieldShilding;
    public TMP_Text TxtShieldCoolDescription;
    public TMP_Text TxtShieldSpecialEffect;
    public TMP_Text TxtShieldTwoHanded;
    public Image ImgShieldIcone;
    [Header("DescriptionStancePanel")] 
    public TMP_Text TxtStanceName;
    public TMP_Text TxtStanceCoolDescription;
    public TMP_Text TxtStancePractialDescription;
    public TMP_Text TxtStanceCoolDown;
    public Image ImgStanceCoolImage;
    [Header("DescriptionUtilityQuestPanel")]
    public TMP_Text TxtUtilityName;
    public TMP_Text TxtUtilityCollDescription;
    public TMP_Text TxtUtilityPracticalDescription;
    public Image ImgUtilityIcone;
    [Header("ButtonOptionPanel")]
    public Slider SliderMousSencibility;
    public Slider SliderVolumeMusic;
    public Slider SliderVolumeSoundEffect;

    [Header("Sliders")]
    public ScrollRect ScrollRectItems;
    public ScrollRect ScrollRectStance;
    private enum InventorySlectorStat
    {
        Items,Stance,Option,nonne
    }
    private InventorySlectorStat _inventorySelectorStat;
    private bool _scrollPanel;
    private GameObject _preselectedPanel;
    private GameObject _temporalHolder;

    void Start() {
        LoadPlayerInfoPanel();
        LoadIteamBar();
        LoadStancePanel();
        _inventorySelectorStat = InventorySlectorStat.Items;
        ShowDesciptionPanel(1);
        _scrollPanel = true;
    }

    void Update() {
        PreSelectPanel();
    }

    public void UIItemButtonSelected() {
        if (_inventorySelectorStat != InventorySlectorStat.Items) {
            _inventorySelectorStat = InventorySlectorStat.Items;
            ShowDesciptionPanel(1);
            _scrollPanel = true;
        }
    }

    public void UIStanceButtonSelected() {
        if (_inventorySelectorStat != InventorySlectorStat.Stance) {
            _inventorySelectorStat = InventorySlectorStat.Stance;
            ShowDesciptionPanel(4);
            _scrollPanel = true;
        }
    }

    public void UIOptionButtonSelected() {
        if (_inventorySelectorStat != InventorySlectorStat.Option) {
            _inventorySelectorStat = InventorySlectorStat.Option;
            ShowDesciptionPanel(3);
            _scrollPanel = false;
        }
    }

    public void UIElseButtonSelected() {
        if (_inventorySelectorStat != InventorySlectorStat.nonne) {
            _inventorySelectorStat = InventorySlectorStat.nonne;
           ShowDesciptionPanel(0);
            _scrollPanel = false;
        }
    }
    public void ScrollPanel(InputAction.CallbackContext callbackContext) {
        if (_scrollPanel) {
            if (_inventorySelectorStat==InventorySlectorStat.Items) {
                Vector2 RawValue = new Vector2(callbackContext.ReadValue<float>(), 0);
                ScrollRectItems.velocity = RawValue * PanelScrollSpeed;
            }
            if (_inventorySelectorStat==InventorySlectorStat.Stance) {
                Vector2 RawValue = new Vector2(callbackContext.ReadValue<float>(), 0);
                ScrollRectStance.velocity = RawValue * PanelScrollSpeed;
            }
        }
    }
    private void LoadPlayerInfoPanel() {
        TxtPlayerStrenght.text = playerInfoComponent.Strengths + "";
        TxtPlayerDexterity.text = playerInfoComponent.Dexerity + "";
        TxtPlayerHP.text = playerInfoComponent.CurrentHP + "/" + playerInfoComponent.MaxHP;
        SliderPlayerHP.value =  (float)playerInfoComponent.CurrentHP/playerInfoComponent.MaxHP;
        if (playerInfoComponent.SpecialStat != null) {
            TxtPlayerSpecialStatName.text = playerInfoComponent.SpecialStat.Name;
            TxtPlayerSpecialStatDescription.text = playerInfoComponent.SpecialStat.Description;
            ImgPlayerSpecialStat.sprite = playerInfoComponent.SpecialStat.Sprite;
            TxtPlayerSpecialStatName.enabled = true;
            TxtPlayerSpecialStatDescription.enabled = true;
            ImgPlayerSpecialStat.enabled = true;
        }
        else {
            TxtPlayerSpecialStatName.enabled = false;
            TxtPlayerSpecialStatDescription.enabled = false;
            ImgPlayerSpecialStat.enabled = false;
        }
    }
    public void LoadIteamBar()
    {
        foreach (ItemData item in playerInfoComponent.Inventory)
        {
            if (item.SoObject is SOWeapon)
            {
                GameObject newObjectPanel = Instantiate(NewWeaopnPanel, PanelSliderItems.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
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
            }

            if (item.SoObject is SOShield)
            {
                GameObject newObjectPanel = Instantiate(NewShealdPanel, PanelSliderItems.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
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
                GameObject newObjectPanel = Instantiate(NewUtilityPanel, PanelSliderItems.transform);
                EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
                editPanelComponent.TxtName.text = item.SoObject.Name;
                editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
                SOUtilityItem soUtilityItem = (SOUtilityItem) item.SoObject;
                editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            }
            PanelSliderItems.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
        }
    }
    public void LoadStancePanel()
    {
        foreach (SOStanceGeneral stance in playerInfoComponent.SOStance)
        {
            GameObject newObjectPanel = Instantiate(NewStancePanel, PanelSliderStances.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = stance.Name;
            editPanelComponent.TxtDurability.text = stance.CoolDown+ "";
            editPanelComponent.TxtSpecialEffect.text = stance.PracticalDescription;
            editPanelComponent.InvetoryIndex = playerInfoComponent.SOStance.IndexOf(stance);
        }
        PanelSliderStances.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
    }
    private void PreSelectPanel() {
        if (_inventorySelectorStat == InventorySlectorStat.Items) {
            float distanceMin = 1000;
            foreach (RectTransform item in PanelSliderItems.transform) {
                if (distanceMin > (item.position - ScrollRectItems.transform.position).magnitude) {
                    distanceMin = (item.position - ScrollRectItems.transform.position).magnitude;
                    _temporalHolder = item.gameObject;
                }
            }
        }
        if (_inventorySelectorStat==InventorySlectorStat.Stance) {
            float distanceMin = 1000;
            foreach (RectTransform item in PanelSliderStances.transform) {
                if (distanceMin > (item.position - ScrollRectStance.transform.position).magnitude) {
                    distanceMin = (item.position - ScrollRectStance.transform.position).magnitude;
                    _temporalHolder = item.gameObject;
                }
            }
        }
        if (_inventorySelectorStat == InventorySlectorStat.nonne ||
            _inventorySelectorStat == InventorySlectorStat.Option) {
            _temporalHolder = null;
            _preselectedPanel?.GetComponent<CombatPanelAnimationComponent>().Deselected();
            _preselectedPanel = null;
        }
        if (_preselectedPanel != _temporalHolder) {
            if (_preselectedPanel != null) {
                _preselectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
            }
            _temporalHolder.GetComponent<CombatPanelAnimationComponent>().Selected();
            _preselectedPanel = _temporalHolder; 
            if(_inventorySelectorStat==InventorySlectorStat.Items)LoadItemPanel();
            if(_inventorySelectorStat==InventorySlectorStat.Stance)LoadStanceInfoPanel();
        }
    }

    private void ShowDesciptionPanel(int index) {
        PanelDescriptionStance.SetActive(false);
        PanelDescriptionUtility.SetActive(false);
        PanelDescriptionWeapon.SetActive(false);
        PanelDescriptionOption.SetActive(false);
        PanelDescriptionShield.SetActive(false);
        switch (index) {
            case 1: PanelDescriptionWeapon.SetActive(true); break;
            case 2: PanelDescriptionUtility.SetActive(true); break;
            case 3: PanelDescriptionOption.SetActive(true); break;
            case 4: PanelDescriptionStance.SetActive(true); break;
            case 5: PanelDescriptionShield.SetActive(true); break;
        }
    }
    private void LoadItemPanel() {
        SOObject item = playerInfoComponent.Inventory[_preselectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex].SoObject;
        if (item is SOWeapon) {
            ShowDesciptionPanel(1);
            LoadWeaponPanel(playerInfoComponent.Inventory[_preselectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex],(SOWeapon)item);
        }
        if (item is SOShield) {
            ShowDesciptionPanel(5);
            LoadShildPanel(playerInfoComponent.Inventory[_preselectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex],(SOShield)item);
        }
        if (item is SOUtilityItem) {
            ShowDesciptionPanel(1);
            LoadUilityPanel((SOUtilityItem)item);
        }
        if (item is SOQuestItem) {
            ShowDesciptionPanel(1);
            LoadQuestPanel((SOQuestItem)item);
            
        }
    }

    private void LoadWeaponPanel(ItemData weaponInfo,SOWeapon weapon) {
        TxtWeaponName.text = weaponInfo.SoObject.Name;
        TxtWeaponDamage.text = weapon.Damage + "";
        TxtWeaponPrecision.text = weapon.ChanceToHit + "";
        TxtWeaponDurability.text = weaponInfo.CurrantDurability + "/" + weapon.Durability;
        TxtWeaponCoolDescription.text = weapon.CoolDescription;
        ImgWeaponIcon.sprite = weapon.UISprite;
        if (weapon.SpecialEffect != null) {
            TxtWeaponSpecialEffect.text = weapon.SpecialEffect.Description;
            TxtWeaponSpecialEffect.enabled = true;
        }
        else {
            TxtWeaponSpecialEffect.enabled = false;
        }
        switch(weapon.Target) {
         case FightComonent.AttackTarget.Front: TxtWeaponTarget.text = "Front"; break;
         case FightComonent.AttackTarget.Invest: TxtWeaponTarget.text = "Invert"; break;
         case FightComonent.AttackTarget.Back: TxtWeaponTarget.text = "Back"; break;
         case FightComonent.AttackTarget.Splash: TxtWeaponTarget.text = "Splash"; break;
        }
        if (weapon.isTwoHand) TxtWeaponTwoHanded.enabled = true;else TxtWeaponTwoHanded.enabled = false;
    }

    private void LoadShildPanel(ItemData shieldInfo, SOShield shield)
    {
        TxtShieldName.text = shield.Name;
        TxtShieldDurability.text = shieldInfo.CurrantDurability + "/" + shield.Durability;
        TxtShieldShilding.text = shield.ShieldValue + "";
        ImgShieldIcone.sprite = shield.UISprite;
        if (shield.SpecialEffect != null) {
            TxtShieldSpecialEffect.text = shield.SpecialEffect.Description;
            TxtShieldSpecialEffect.enabled = true;
        }
        else {
            TxtShieldSpecialEffect.enabled = false;
        }
        if (shield.IsTwoHanded) TxtShieldTwoHanded.enabled = true;else TxtShieldTwoHanded.enabled = false;
    }

    private void LoadUilityPanel(SOUtilityItem item)
    {
        TxtUtilityName.text = item.Name;
        TxtUtilityCollDescription.text = item.CoolDescription;
        TxtUtilityPracticalDescription.text = item.PracticalDescription;
        ImgShieldIcone.sprite = item.UISprite;
    }

    private void LoadQuestPanel(SOQuestItem item)
    {
        TxtUtilityName.text = item.Name;
        TxtUtilityCollDescription.text = item.CoolDescription;
        TxtUtilityPracticalDescription.text = item.PracticalDescription;
        ImgShieldIcone.sprite = item.UISprite;
    }

    private void LoadStanceInfoPanel()
    {
        SOStanceGeneral stance = playerInfoComponent.SOStance[_preselectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex];
        ShowDesciptionPanel(4);
        TxtStanceName.text = stance.Name;
        TxtStanceCoolDescription.text = stance.CoolDescription;
        TxtStancePractialDescription.text = stance.PracticalDescription;
        TxtStanceCoolDown.text = stance.CoolDown+"";
        ImgStanceCoolImage.sprite = stance.CoolImage;
    }
}
