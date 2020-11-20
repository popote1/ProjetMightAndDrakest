using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;



public class FightComonent : MonoBehaviour
{
    [Header("Custom Parameters")] 
    public float PanelScrollSpeed;
    public float SelectorMoveSpeed = 0.5f;
    [SerializeField]public EnnemiGroupComponent ennemiGroupComponent;
    [Header("Prefabs")]
    public GameObject NewPrefabPanel;
    [Header("UI Selection")] 
    public GameObject Selector;
    public GameObject PanelObject;
    public GameObject PanelSlideObject;
    public GameObject PanelStance;
    public GameObject PanelSliderStance;
    public GameObject PanelSelectedObject1;
    public GameObject PanelSelectedObject2;
    public GameObject PanelSelectedStance;
    [Header(("UI Perso Info"))]
    public Slider SliderHP;
    public TMP_Text TxtHP;
    public Image ImgSpecialStat;
    public GameObject PanelInfoSpecialStat;
    public TMP_Text TxtSpecialStatDamage;
    public TMP_Text TxtSpecialStatCharge;
    public TMP_Text TxtSpecialStatDescription;
    public Image ImgSelector;
    [Header("UI Ennemi1")]
    public EnnemiCombatUIComponent Ennemi1CombatUIComponent;
    [Header("UI Ennemi2")]
    public EnnemiCombatUIComponent Ennemi2CombatUIComponent;
    [Header("UI Ennemi3")] public EnnemiCombatUIComponent Ennemi3CombatUIComponent;

    public enum AttackTarget { Front,Invest,Back,Splash }

    private ScrollRect _iteamScrollReck;
    private PlayerInfoComponent _playerInfoComponent;
    private int _selectorStat = 1;
    private GameObject _preSelectedPanel;
    private GameObject _temporalHolder;
    private int _combatStat = 1;
    
    //object séléctioner
    private GameObject _object1;
    private GameObject _object2;
    private GameObject _Stance;
    private ItemData _itemData1;
    private ItemData _itemData2;
    
    void Start()
    {
        _iteamScrollReck = PanelObject.GetComponent<ScrollRect>();
        _playerInfoComponent = GetComponent<PlayerInfoComponent>();
        Cursor.lockState = CursorLockMode.Locked;
        LoadIteamBar();
        LoadPlayerStats();
        SetEnnemis();
    }

    // Update is called once per frame
    void Update()
    {

        UpDateSelectorPos();
        PreSelectPanel();

        if (_object1 != null)
        {
            Debug.Log(" SaBoge le pannel");
            PanelToposition(_object1, PanelSelectedObject1.transform);
        }
        if (_object2 != null)
        {
            PanelToposition(_object2, PanelSelectedObject2.transform);
        }
    }
    
    
    
    
    
    //Methode qui permet de faire bouge le panel object
    public void ScrollPanel(InputAction.CallbackContext callbackContext)
    {
        Vector2 RawValue = new Vector2(callbackContext.ReadValue<float>(),0);
        _iteamScrollReck.velocity = RawValue * PanelScrollSpeed;
    }

    //click pour séléctionner les object et stance
    public void OnClick(InputAction.CallbackContext callbackContext)
    {
        if (callbackContext.started)
        {
            switch (_selectorStat)
            {
                case 1:
                    _preSelectedPanel.transform.SetParent( PanelSelectedObject1.transform);
                    _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
                    if (_object1 != null)
                    {
                        _object1.transform.SetParent(PanelSlideObject.transform);
                    }
                    

                    _object1 = _preSelectedPanel;
                    _preSelectedPanel = null;
                    PassToNextSelectorStat();
                    PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                    _itemData1 = _preSelectedPanel.GetComponent<ItemData>();
                    break;
                case 2:
                    _preSelectedPanel.transform.SetParent( PanelSelectedObject2.transform);
                    _preSelectedPanel.GetComponent<CombatPanelAnimationComponent>().Deselected();
                    if (_object2 != null)
                    {
                        _object2.transform.SetParent(PanelSlideObject.transform);
                    }

                    _object2 = _preSelectedPanel;
                    _preSelectedPanel = null;
                    PassToNextSelectorStat();
                    PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
                    _itemData2 = _preSelectedPanel.GetComponent<ItemData>();
                    break;
            }
        }
    }

    //Methode qui instancie chaque object et entre les stats dans le slider Object
    private void LoadIteamBar()
    {
        foreach (ItemData item in _playerInfoComponent.Inventory)
        {
            GameObject newObjectPanel = Instantiate(NewPrefabPanel, PanelSlideObject.transform);
            EditPanelComponent editPanelComponent = newObjectPanel.GetComponent<EditPanelComponent>();
            editPanelComponent.TxtName.text = item.SoObject.Name;
            if (item.SoObject is SOWeapon)
            {
                SOWeapon soWeapon = (SOWeapon) item.SoObject;
                editPanelComponent.TxtDamage.text = ""+soWeapon.Damage;
                editPanelComponent.TxtCHT.text = "" + soWeapon.ChanceToHit;
                editPanelComponent.TxtDurability.text = item.CurrantDurability + "/" + soWeapon.Durability;
                switch (soWeapon.Target)
                {
                    case AttackTarget.Front :
                        editPanelComponent.TxtTarget.text = "Front";
                        break;
                    case AttackTarget.Invest:
                        editPanelComponent.TxtTarget.text = "Invert";
                        break;
                    case AttackTarget.Back:
                        editPanelComponent.TxtTarget.text = "Back";
                        break;
                    case AttackTarget.Splash:
                        editPanelComponent.TxtTarget.text = "Splash";
                        break;
                }
                editPanelComponent.TxtSpecialEffect.enabled = false;
                editPanelComponent.TxtTitleSpecialEffect.enabled = false;
                
               Vector2 size =PanelSlideObject.GetComponent<RectTransform>().sizeDelta;
               PanelSlideObject.GetComponent<SliderAutiSizerComponent>().ReSizePanel();
            }
            
        }
    }

    private void LoadPlayerStats()
    {
        TxtHP.text = _playerInfoComponent.CurrentHP + "/" + _playerInfoComponent.MaxHP;
        SliderHP.value = _playerInfoComponent.CurrentHP/(float)_playerInfoComponent.MaxHP;
    }
    //Methode du move de curseur
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
        }
    }

    private void PreSelectPanel()
    {
        float distanceMin = 1000;
        foreach (RectTransform item in PanelSlideObject.transform)
        {
            if (distanceMin > (item.position - Selector.transform.position).magnitude)
            {
                distanceMin=(item.position - Selector.transform.position).magnitude;
                _temporalHolder = item.gameObject;
            }
        }

        if (_preSelectedPanel != _temporalHolder)
        {
            _preSelectedPanel?.GetComponent<CombatPanelAnimationComponent>().Deselected();
            _temporalHolder.GetComponent<CombatPanelAnimationComponent>().Selected();
            _preSelectedPanel = _temporalHolder;
        }
    }

    private void PanelToposition(GameObject objectMove , Transform destination)
    {
        objectMove.transform.position = Vector3.Lerp(objectMove.transform.position, destination.position, 0.5f);
    }

    private void PassToNextSelectorStat()
    {
        switch (_selectorStat)
        {
            case 1:
                _selectorStat = 2;
            break;
            case 2:
                _selectorStat = 3; 
                break;
        }
    }

    private void SetEnnemis()
    {
        switch (ennemiGroupComponent.Ennemis.Count)
        {
            case 1:
                Ennemi2CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[0]);
                break;
            case 2:
                Ennemi1CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[0]);
                Ennemi3CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[1]);
                break;
            case 3:
                Ennemi1CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[0]);
                Ennemi2CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[1]);
                Ennemi3CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[2]);
                break;
        }
    }

   /* private void PlayerActionManager()
    {
        if (_itemData1 is SOWeapon)
        {
            SOWeapon weapon = (SOWeapon) _itemData1.SoObject;
            if (weapon.Target == AttackTarget.Front)
            {
                if (PannelEnnemi1.activeSelf)
                {

                }
                else if (PannelEnnemi2.activeSelf)
                {

                }
                else
                {

                }
            }

            PlayerStandardAttack((SOWeapon) _itemData1.SoObject);
        }
        
    }*/
    

    private int PlayerStandardAttack(SOWeapon weapon)
    {
        int chanceToHit = -weapon.ChanceToHit + _playerInfoComponent.Dexerity;
        float hitDice = Random.Range(0, 100);
        int damage = 0;
        if (chanceToHit > hitDice)
        {
            if (hitDice < 10)
            {
                Debug.Log("L'attaque touche en critique avec un jet de " + hitDice + "sur " + chanceToHit);
                 damage= (weapon.Damage + _playerInfoComponent.Strengths)*2;
            }
            else
            {
                Debug.Log("L'attaque touche  avec un jet de " + hitDice + "sur " + chanceToHit); 
                damage = weapon.Damage + _playerInfoComponent.Strengths;
            }
        }

        return damage;
    }
    
    
    

}
