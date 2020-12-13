using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemComponent : MonoBehaviour , IInteracteble
{
    public SOObject SoObject;
    public ItemData ItemData;
    public AudioClip audioClip;
    [Range(0,1)]public float volume=1;
    [Header("Prefabs Panel")]
    public GameObject WeaponPanel;
    public GameObject ShieldPanel;
    public GameObject UtilityPanel;
    public Canvas Canvas;
    public UnityEvent EventOnPicking = new UnityEvent(); 

    private MeshRenderer _meshRenderer;
    private LineRenderer _lineRenderer;
    private GameObject _infoPanel;


    // Start is called before the first frame update
    void Start()
    {
        Canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
        _meshRenderer = GetComponentInChildren<MeshRenderer>();
        _lineRenderer = GetComponent<LineRenderer>();
        if (ItemData == null)
        {
            ItemData= new ItemData(SoObject);
            _meshRenderer.material = SoObject.WorldSprite;
        }
        else
        {
            SoObject = ItemData.SoObject;
        }
        _infoPanel = CreatPanel();
        _infoPanel.AddComponent<FollowWorldUIComponent>();
        _infoPanel.GetComponent<FollowWorldUIComponent>().lookAt = transform;
        _infoPanel.GetComponent<FollowWorldUIComponent>().OffSet = transform.up;
        
        _infoPanel.SetActive(false);
    }
    

    public void Intecract(InteractComponent.SelectStat selectStat)
    {
        switch (selectStat)
        {
            case InteractComponent.SelectStat.none:
                DesetSelectable();
                break;
            case InteractComponent.SelectStat.selectable :
                SetSelectable();
                break;
            case InteractComponent.SelectStat.Preselected:
                break;
            case InteractComponent.SelectStat.Selec:
                break;
        }
    }
    
    public void SetPreselctable()
    {
        Debug.Log("itemPreselected");
        _infoPanel.SetActive(true);
    }

    public void SetSelectable()
    {
        Debug.Log("Objet en selectable");
        _lineRenderer.enabled = true;
    }

    public void DePreselectable()
    {
        Debug.Log("item DePreselected");
        _infoPanel.SetActive(false);
    }

    public void DesetSelectable()
    {
        Debug.Log("Objet Deséléctioner");
        _lineRenderer.enabled=false;
    }

    public bool USe(PlayerInfoComponent playerInfoComponent)
    {
       
        if (playerInfoComponent.Inventory.Count < playerInfoComponent.InventoryLengths)
        {
            SoundManager.PlaySound(audioClip,volume);
            playerInfoComponent.Inventory.Add(ItemData);
            Destroy(_infoPanel);
            Debug.Log(" je prend de l'object");
            EventOnPicking.Invoke();
            Destroy(gameObject);
            return true;
        }
        return false;
    }

    private GameObject CreatPanel()
    {
        GameObject newObjectPanel = null;
        if (ItemData.SoObject is SOWeapon) { 
            newObjectPanel = Instantiate(WeaponPanel, Canvas.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = ItemData.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = ItemData;
            SOWeapon soWeapon = (SOWeapon) ItemData.SoObject;
            editPanelComponent.TxtDamage.text = "" + soWeapon.Damage;
            editPanelComponent.TxtCHT.text = "" + soWeapon.ChanceToHit;
            editPanelComponent.TxtDurability.text = ItemData.CurrantDurability + "/" + soWeapon.Durability;
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

        if (ItemData.SoObject is SOShield)
        {
            newObjectPanel = Instantiate(ShieldPanel, Canvas.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = ItemData.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = ItemData;
            SOShield soShield = (SOShield) ItemData.SoObject;
            editPanelComponent.TxtDamage.text = "" + soShield.ShieldValue;
            editPanelComponent.TxtDurability.text = ItemData.CurrantDurability + "/" + soShield.Durability;
            if (soShield.SpecialEffect != null)
            {
            }
            else
            {
                editPanelComponent.TxtSpecialEffect.enabled = false;
            }
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }

        if (ItemData.SoObject is SOUtilityItem)
        {
            newObjectPanel = Instantiate(UtilityPanel,Canvas.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = ItemData.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = ItemData;
            SOUtilityItem soUtilityItem = (SOUtilityItem) ItemData.SoObject;
            editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }
        if (ItemData.SoObject is SOQuestItem)
        {
            newObjectPanel = Instantiate(UtilityPanel,Canvas.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = ItemData.SoObject.Name;
            //editPanelComponent.InvetoryIndex = playerInfoComponent.Inventory.IndexOf(item);
            editPanelComponent.ItemData = ItemData;
            SOQuestItem soUtilityItem = (SOQuestItem) ItemData.SoObject;
            editPanelComponent.TxtSpecialEffect.text = soUtilityItem.PracticalDescription;
            newObjectPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
        }

        return newObjectPanel;
    }
}
