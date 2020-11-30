using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Object = System.Object;

public class InventoryComponent : MonoBehaviour
{
    public HUDComponent HudComponent;
    public GameObject NewWeaopnPanel;
    public GameObject NewUtilityPanel;
    public GameObject NewShealdPanel;
    public GameObject NewStancePanel;
    public PlayerInfoComponent playerInfoComponent;
    public float PanelScrollSpeed;
    public Button ItemButton;
    public Button UsButton;
    public Button TrowButton;
    public Button OptionButton;
    public Button QuiteButton;
    public Button QuiteButtonNon;
    [Header("Panels")] 
    public GameObject PanelDescriptionWeapon;
    public GameObject PanelDescriptionShield;
    public GameObject PanelDescriptionUtility;
    public GameObject PanelDescriptionStance;
    public GameObject PanelDescriptionOption;
    public GameObject PanelButtonStandard;
    public GameObject PanelButtonOption;
    public GameObject PanelButtonIteam;
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
    [Header("Quite")] 
    public GameObject PanelQuite;
    private enum InventorySlectorStat
    {
        Items,Stance,Option,nonne
    }
    private InventorySlectorStat _inventorySelectorStat;
    private bool _scrollPanel;
    private GameObject _preselectedPanel;
    private GameObject _temporalHolder;
    private ItemData _selectedItem;
    private bool _inventoryOn;

    public void LaunchInventory() {
        ItemButton.Select();
        LoadPlayerInfoPanel();
        LoadIteamBar();
        LoadStancePanel();
        _inventorySelectorStat = InventorySlectorStat.Items;
        ShowDesciptionPanel(1);
        ShowButtonPanel(1);
        _scrollPanel = true;
        _inventoryOn = true;
    }

    void Update()
    {
        if (_inventoryOn)
        {
            PreSelectPanel();
            SliderPlayerHP.value = Mathf.Lerp(SliderPlayerHP.value,
                (float) playerInfoComponent.CurrentHP / playerInfoComponent.MaxHP, 0.5f);
            TxtPlayerHP.text = playerInfoComponent.CurrentHP + "/" + playerInfoComponent.MaxHP;
        }
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

    public void UIButtonItem() {
        ShowButtonPanel(2);
        _selectedItem = _preselectedPanel.GetComponent<EditPanelComponent>().ItemData;
        _scrollPanel = false;
        if (_selectedItem.SoObject is SOWeapon || _selectedItem.SoObject is SOShield) UsButton.gameObject.SetActive(false);
        if (_selectedItem.SoObject is SOUtilityItem)
        {
            UsButton.gameObject.SetActive(true);
            UsButton.onClick.AddListener(delegate
            {
                ((SOUtilityItem)_selectedItem.SoObject).SoUtilityEffectGeneral.WorldUse(this);
                UIButtonIteamReturn();
                UsButton.onClick.RemoveAllListeners();
            });
        }
        if (_selectedItem.SoObject is SOQuestItem){
            SOQuestItem item = (SOQuestItem) _selectedItem.SoObject;
            if (item.CheckWorldUse())
            {
                UsButton.gameObject.SetActive(true);
                UsButton.onClick.AddListener(delegate
                {
                    ((SOQuestItem)_selectedItem.SoObject).WorldUse(this);
                    UIButtonIteamReturn();
                    UsButton.onClick.RemoveAllListeners();
                });
            }
            else
            {
                UsButton.gameObject.SetActive(false);
            }
        }
        if (UsButton.gameObject.activeSelf) UsButton.Select();else TrowButton.Select();
        ScrollRectItems.velocity =Vector2.zero;
    }

    public void UIButtonIteamReturn() {
        ShowButtonPanel(1);
        _selectedItem = null;
        _scrollPanel = true;
        ItemButton.Select();
    }
    public void UIButtonOption()
    {
        ShowButtonPanel(3);
        SliderMousSencibility.Select();
    }
    public void UIButtonOptionReturn()
    {
        ShowButtonPanel(1);
        OptionButton.Select();
    }
    public void UIButtonQuite()
    {
        PanelQuite.SetActive(true);
        QuiteButtonNon.Select();
    }

    public void UIButtonReprendre()
    {
        foreach (Transform item in PanelSliderItems.transform) Destroy(item.gameObject,0.1f);
        foreach (Transform stance in PanelSliderStances.transform) Destroy(stance.gameObject,0.1f);
        HudComponent.CloseInventory();
        _inventoryOn = false;
    }

    public void UIButtonQuiteYes()
    {
        Application.Quit();
    }
    public void UIButtonQuiteNon()
    {
        PanelQuite.SetActive(false);
        QuiteButton.Select();
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
        foreach (ItemData item in playerInfoComponent.Inventory) LoadNewItemPanel(item);
        PanelSliderItems.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
    }
    public void LoadStancePanel() {
        foreach (SOStanceGeneral stance in playerInfoComponent.SOStance) {
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
            if(_inventorySelectorStat==InventorySlectorStat.Stance)LoadStanceDescriptionPanel();
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

    private void ShowButtonPanel(int index) {
        PanelButtonStandard.SetActive(false);
        PanelButtonIteam.SetActive(false);
        PanelButtonOption.SetActive(false);
        switch (index) {
            case 1: PanelButtonStandard.SetActive(true); break;
            case 2 :PanelButtonIteam.SetActive(true); break;
            case 3 :PanelButtonOption.SetActive(true); break;
        }
    }
    private void LoadItemPanel() {
        SOObject item = _preselectedPanel.GetComponent<EditPanelComponent>().ItemData.SoObject;
        if (item is SOWeapon) {
            ShowDesciptionPanel(1);
            LoadWeaponDescriptionPanel(_preselectedPanel.GetComponent<EditPanelComponent>().ItemData,(SOWeapon)item);
        }
        if (item is SOShield) {
            ShowDesciptionPanel(5);
            LoadShildDescriptionPanel(_preselectedPanel.GetComponent<EditPanelComponent>().ItemData,(SOShield)item);
        }
        if (item is SOUtilityItem) {
            ShowDesciptionPanel(2);
            LoadUilityDescriptionPanel((SOUtilityItem)item);
        }
        if (item is SOQuestItem) {
            ShowDesciptionPanel(2);
            LoadQuestDescriptionPanel((SOQuestItem)item);
            
        }
    }

    private void LoadWeaponDescriptionPanel(ItemData weaponInfo,SOWeapon weapon) {
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

    private void LoadShildDescriptionPanel(ItemData shieldInfo, SOShield shield)
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

    private void LoadUilityDescriptionPanel(SOUtilityItem item)
    {
        TxtUtilityName.text = item.Name;
        TxtUtilityCollDescription.text = item.CoolDescription;
        TxtUtilityPracticalDescription.text = item.PracticalDescription;
        ImgShieldIcone.sprite = item.UISprite;
    }

    private void LoadQuestDescriptionPanel(SOQuestItem item)
    {
        TxtUtilityName.text = item.Name;
        TxtUtilityCollDescription.text = item.CoolDescription;
        TxtUtilityPracticalDescription.text = item.PracticalDescription;
        ImgShieldIcone.sprite = item.UISprite;
    }

    private void LoadStanceDescriptionPanel()
    {
        SOStanceGeneral stance = playerInfoComponent.SOStance[_preselectedPanel.GetComponent<EditPanelComponent>().InvetoryIndex];
        ShowDesciptionPanel(4);
        TxtStanceName.text = stance.Name;
        TxtStanceCoolDescription.text = stance.CoolDescription;
        TxtStancePractialDescription.text = stance.PracticalDescription;
        TxtStanceCoolDown.text = stance.CoolDown+"";
        ImgStanceCoolImage.sprite = stance.CoolImage;
    }

    public void DestroySelectedItem(){
        Destroy((PanelSliderItems.transform.GetChild(playerInfoComponent.Inventory.IndexOf(_selectedItem))).gameObject);
        PanelSliderItems.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
        playerInfoComponent.Inventory.Remove(_selectedItem);
        _selectedItem = null;
    }

    public void AddItemsToInventory(SOObject item){
        ItemData newItem = new ItemData(item);
        playerInfoComponent.Inventory.Add(newItem);
        LoadNewItemPanel(newItem);
        PanelSliderItems.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
    }

    public void LoadNewItemPanel(ItemData item)
    {
        if (item.SoObject is SOWeapon) {
            GameObject newObjectPanel = Instantiate(NewWeaopnPanel, PanelSliderItems.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = item.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = item;
            SOWeapon soWeapon = (SOWeapon) item.SoObject;
            editPanelComponent.TxtDamage.text = "" + soWeapon.Damage;
            editPanelComponent.TxtCHT.text = "" + soWeapon.ChanceToHit;
            editPanelComponent.TxtDurability.text = item.CurrantDurability + "/" + soWeapon.Durability;
            switch (soWeapon.Target) {
                case FightComonent.AttackTarget.Front:
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
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }

        if (item.SoObject is SOShield)
        {
            GameObject newObjectPanel = Instantiate(NewShealdPanel, PanelSliderItems.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = item.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = item;
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
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }

        if (item.SoObject is SOUtilityItem)
        {
            GameObject newObjectPanel = Instantiate(NewUtilityPanel, PanelSliderItems.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = item.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = item;
            SOUtilityItem soUtilityItem = (SOUtilityItem) item.SoObject;
            editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }
        if (item.SoObject is SOQuestItem)
        {
            GameObject newObjectPanel = Instantiate(NewUtilityPanel, PanelSliderItems.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = item.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = item;
            SOQuestItem soUtilityItem = (SOQuestItem) item.SoObject;
            editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }
    }
}
    
    

