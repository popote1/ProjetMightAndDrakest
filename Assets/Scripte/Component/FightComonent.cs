using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;



public class FightComonent : MonoBehaviour
{
    [Header("Custom Parameters")] [SerializeField]
    public EnnemiGroupComponent ennemiGroupComponent;

    public FightSelectorComponent FightSelectorComponent;


    [Header(("UI Perso Info"))] public Slider SliderHP;
    public TMP_Text TxtHP;
    public Image ImgSpecialStat;
    public GameObject PanelInfoSpecialStat;
    public TMP_Text TxtSpecialStatDamage;
    public TMP_Text TxtSpecialStatCharge;
    public TMP_Text TxtSpecialStatDescription;
    public Image ImgSelector;
    [Header("UI Ennemi")] public EnnemiCombatUIComponent Ennemi1CombatUIComponent;
    public EnnemiCombatUIComponent Ennemi2CombatUIComponent;
    public EnnemiCombatUIComponent Ennemi3CombatUIComponent;
    [Header("Temporal Elements")] public GameObject VictoryPanel;
    public GameObject DefeatPanel;

    public enum AttackTarget
    {
        Front,
        Invest,
        Back,
        Splash
    }

    [HideInInspector] public PlayerInfoComponent PlayerInfoComponent;
    [HideInInspector] public int CombatStat = 1;

    //object séléctioner
    private GameObject _Stance;
    [HideInInspector] public ItemData ItemData1;
    [HideInInspector] public ItemData ItemData2;
    [HideInInspector] public List<StanceInfo> Stances = new List<StanceInfo>();
    [HideInInspector] public StanceInfo SelectedStance;
    [HideInInspector] public bool IsStaneOnObject1;
    [HideInInspector] public bool IsStaneOnObject2;

    void Start()
    {
        PlayerInfoComponent = GetComponent<PlayerInfoComponent>();
        Cursor.lockState = CursorLockMode.Locked;
        LoadPlayerStats();
        SetEnnemis();
        FightSelectorComponent.LoadIteamBar();
        FightSelectorComponent.LoadStancePanel();
        CheckSpecialStat();
    }

    // Update is called once per frame
    void Update()
    {
        CheckSpecialStat();

        SliderHP.value = Mathf.Lerp(SliderHP.value, PlayerInfoComponent.CurrentHP / (float) PlayerInfoComponent.MaxHP,
            0.5f);
        // effectue les effets Speciaux
        if (CombatStat == 1)
        {
            if (PlayerInfoComponent.SpecialStat != null)
            {
                PlayerInfoComponent.SpecialStat.MakeEffect(PlayerInfoComponent);
            }
            CheckSpecialStat();
            CombatStat = 2;
        }

        //Choix des armes
        if (CombatStat == 2)
        {
            FightSelectorComponent.SelectTime = true;
        }

        //Analise de la stance
        if (CombatStat == 3)
        {
            CheckOfUse();
        }

        //Execution de l'object1
        if (CombatStat == 5)
        {
            PlayerAttack1();
        }

        //Execution de l'object2
        if (CombatStat == 7)
        {
            PlayerAttack2();
        }

        //Résolution des effets sur Ennemi1
        if (CombatStat == 10)
        {
            EnnemiSpecialStat(1);
        }

        //Attaque de l'ennemi 1
        if (CombatStat == 12)
        {
            EnnemiAttaque(1);
        }

        //Résolution des effets sur Ennemi2
        if (CombatStat == 20)
        {
            EnnemiSpecialStat(2);
        }

        //Attaque de l'ennemi 2
        if (CombatStat == 22)
        {
            EnnemiAttaque(2);
        }

        //Résolution des effets sur Ennemi3
        if (CombatStat == 30)
        {
            EnnemiSpecialStat(3);
        }

        //Attaque de l'ennemi 3
        if (CombatStat == 32)
        {
            EnnemiAttaque(3);
        }

        //reset des panels
        if (CombatStat == 50)
        {
            FightSelectorComponent.ResetPannels();
            CombatStat = 1;
        }

        if (CombatStat == 100)
        {
            SetPanelVictory();
        }

        if (CombatStat == 101)
        {
            SetPanelDefat();
        }
    }

    public void StartCombat()
    {
        CombatStat = 3;
        FightSelectorComponent.SelectTime = false;
    }

    private void LoadPlayerStats()
    {
        TxtHP.text = PlayerInfoComponent.CurrentHP + "/" + PlayerInfoComponent.MaxHP;
        //SliderHP.value = _playerInfoComponent.CurrentHP/(float)_playerInfoComponent.MaxHP;
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
                Ennemi2CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[2]);
                Ennemi3CombatUIComponent.SetPanel(ennemiGroupComponent.Ennemis[1]);
                break;
        }
    }

    private void EnnemiSpecialStat(int ennemiIndex)
    {
        EnnemiCombatUIComponent ennemi = Ennemi1CombatUIComponent;
        switch (ennemiIndex)
        {
            case 1:
                ennemi = Ennemi1CombatUIComponent;
                CombatStat = 11;
                break;
            case 2:
                ennemi = Ennemi2CombatUIComponent;
                CombatStat = 21;
                break;
            case 3:
                ennemi = Ennemi3CombatUIComponent;
                CombatStat = 31;
                break;
        }

        if (ennemi.SpecialStat != null)
        {
            ennemi.SpecialStat.MakeEffect(this, ennemi);
            if (ennemi.SpecialStat is SOSpecialStatStun) return;
            Invoke("DelayChangeCombatStat", 0.5f);
            return;
        }

        DelayChangeCombatStat();
    }

    private void EnnemiAttaque(int ennemiIndex)
    {
        EnnemiCombatUIComponent ennemi = Ennemi1CombatUIComponent;
        switch (ennemiIndex)
        {
            case 1:
                ennemi = Ennemi1CombatUIComponent;
                CombatStat = 19;
                break;
            case 2:
                ennemi = Ennemi2CombatUIComponent;
                CombatStat = 29;
                break;
            case 3:
                ennemi = Ennemi3CombatUIComponent;
                CombatStat = 49;
                break;
        }

        if (ennemi.IsAlive)
        {
            PlayerInfoComponent.CurrentHP -= ennemi.MakeAttack();
            if (IsDefeat())
            {
                CombatStat = 101;
                return;
            }

            Invoke("DelayChangeCombatStat", 1f);
            return;
        }

        CombatStat++;
    }

    private void CheckOfUse()
    {
        SelectedStance.SoStance.CheckForUse(this, out IsStaneOnObject1, out IsStaneOnObject2);
        Debug.Log(" stance pour arme 1 =" + IsStaneOnObject1 + "       Stance pour arme 2=" + IsStaneOnObject2);
        CombatStat = 4;
        DelayChangeCombatStat();
    }

    private void PlayerAttack1()
    {
        if (IsStaneOnObject1)
        {
            SelectedStance.SoStance.ExecutStance(this, 1);
        }
        else
        {
            Debug.Log(SelectedStance.SoStance.Name);
            EnnemiCombatUIComponent target;
            SOWeapon weapon = (SOWeapon) ItemData1.SoObject;
            int damage = PlayerStandardAttack(weapon);
            target = ChoseTarget(1, weapon.Target);
            target.TakeDamage(damage);
            if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(this, target, 1))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(this, target , 1);
                }
            }
            ItemData1.CurrantDurability--;
        }

        if (IsVictory())
        {
            CombatStat = 100;
            return;
        }

        CombatStat = 6;
        Invoke("DelayChangeCombatStat", 2f);
    }

    private void PlayerAttack2()
    {
        if (IsStaneOnObject2)
        {
            SelectedStance.SoStance.ExecutStance(this, 2);
        }
        else
        {
            EnnemiCombatUIComponent target;
            SOWeapon weapon = (SOWeapon) ItemData2.SoObject;
            int damage = PlayerStandardAttack(weapon);
            target = ChoseTarget(2, weapon.Target);
            target.TakeDamage(damage);
            if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(this, target, 2))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(this, target , 2);
                }
            }
            ItemData2.CurrantDurability--;
        }

        if (IsVictory())
        {
            CombatStat = 100;
            return;
        }

        CombatStat = 9;
        Invoke("DelayChangeCombatStat", 2);
    }

    public int PlayerStandardAttack(SOWeapon weapon)
    {
        int chanceToHit = weapon.ChanceToHit + PlayerInfoComponent.Dexerity;
        float hitDice = Random.Range(0, 100);
        //Debug.Log (" la dex du player est de ")
        int damage = 0;
        if (chanceToHit > hitDice)
        {
            if (hitDice < 10)
            {
                Debug.Log("L'attaque touche en critique avec un jet de " + hitDice + "sur " + chanceToHit);
                damage = (weapon.Damage + PlayerInfoComponent.Strengths) * 2;
            }
            else
            {
                Debug.Log("L'attaque touche  avec un jet de " + hitDice + "sur " + chanceToHit);
                damage = weapon.Damage + PlayerInfoComponent.Strengths;
            }
        }

        Debug.Log(weapon.Name + " inflige " + damage + " domage");

        return damage;
    }

    //Choisie la cible dattaquer
    public EnnemiCombatUIComponent ChoseTarget(int itemNumb, AttackTarget targetOrder)
    {
        if (targetOrder == AttackTarget.Invest && itemNumb == 1)
        {
            itemNumb = 2;
            targetOrder = AttackTarget.Front;
        }

        if (targetOrder == AttackTarget.Invest && itemNumb == 2)
        {
            itemNumb = 1;
            targetOrder = AttackTarget.Front;
        }

        if (itemNumb == 1)
        {
            if (targetOrder == AttackTarget.Front)
            {
                if (Ennemi1CombatUIComponent.IsAlive) return Ennemi1CombatUIComponent;
                if (Ennemi2CombatUIComponent.IsAlive) return Ennemi2CombatUIComponent;
                if (Ennemi3CombatUIComponent.IsAlive) return Ennemi3CombatUIComponent;
            }

            if (targetOrder == AttackTarget.Back)
            {
                if (Ennemi2CombatUIComponent.IsAlive) return Ennemi2CombatUIComponent;
                if (Ennemi3CombatUIComponent.IsAlive) return Ennemi3CombatUIComponent;
                if (Ennemi1CombatUIComponent.IsAlive) return Ennemi1CombatUIComponent;
            }
        }

        if (itemNumb == 2)
        {
            if (targetOrder == AttackTarget.Front)
            {
                if (Ennemi3CombatUIComponent.IsAlive) return Ennemi3CombatUIComponent;
                if (Ennemi2CombatUIComponent.IsAlive) return Ennemi2CombatUIComponent;
                if (Ennemi1CombatUIComponent.IsAlive) return Ennemi1CombatUIComponent;
            }

            if (targetOrder == AttackTarget.Back)
            {
                if (Ennemi2CombatUIComponent.IsAlive) return Ennemi2CombatUIComponent;
                if (Ennemi1CombatUIComponent.IsAlive) return Ennemi1CombatUIComponent;
                if (Ennemi3CombatUIComponent.IsAlive) return Ennemi3CombatUIComponent;
            }
        }

        return null;
    }

    private void DelayChangeCombatStat()
    {
        CombatStat++;
    }

    private bool IsVictory()
    {
        if (Ennemi1CombatUIComponent.IsAlive) return false;
        if (Ennemi2CombatUIComponent.IsAlive) return false;
        if (Ennemi3CombatUIComponent.IsAlive) return false;
        return true;
    }

    private bool IsDefeat()
    {
        if (PlayerInfoComponent.CurrentHP <= 0)
        {
            return true;
        }

        return false;
    }

    private void SetPanelVictory()
    {
        VictoryPanel.SetActive(true);
    }

    private void SetPanelDefat()
    {
        DefeatPanel.SetActive(true);
    }

    private void CheckSpecialStat()
    {

        if (PlayerInfoComponent.SpecialStat != null)
        {
            ImgSpecialStat.enabled = true;
            ImgSpecialStat.sprite = PlayerInfoComponent.SpecialStat.Sprite;
        }
        else
        {
            ImgSpecialStat.enabled = false;
        }
    }

}
