
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;



public class FightComonent : MonoBehaviour
{
    [Header("Custom Parameters")] [SerializeField]
    public EnnemiGroupComponent EnnemiGroupComponent;
    public PlayerInfoComponent PlayerInfoComponent;
    public FightSelectorComponent FightSelectorComponent;
    [SerializeField] public HUDComponent HudComponent;


    [Header(("UI Perso Info"))] public Slider SliderHP;
    public TMP_Text TxtHP;
    public Image ImgSpecialStat;
    public GameObject PanelInfoSpecialStat;
    public TMP_Text TxtSpecialStatDamage;
    public TMP_Text TxtSpecialStatCharge;
    public TMP_Text TxtSpecialStatDescription;
    public Image ImgSelector;
    public Image ImgShield;
    public TMP_Text TxtShield;
    [Header("UI Ennemi")]
    public EnnemiCombatUIComponent Ennemi1CombatUIComponent;
    public EnnemiCombatUIComponent Ennemi2CombatUIComponent;
    public EnnemiCombatUIComponent Ennemi3CombatUIComponent;
    [Header("EnnemiePos")] 
    public GameObject Ennemi1;
    public GameObject Ennemi2;
    public GameObject Ennemi3;
    [Header("Temporal Elements")] 
    public GameObject VictoryPanel;
    public GameObject DefeatPanel;
    public bool IsFighting;

    public enum AttackTarget
    {
        Front,
        Invest,
        Back,
        Splash
    }
    
    [HideInInspector] public int CombatStat = 1;

    //object séléctioner
    private GameObject _Stance;
    [HideInInspector] public ItemData ItemData1;
    [HideInInspector] public ItemData ItemData2;
    [HideInInspector] public List<StanceInfo> Stances = new List<StanceInfo>();
    [HideInInspector] public StanceInfo SelectedStance;
    [HideInInspector] public bool IsStaneOnObject1;
    [HideInInspector] public bool IsStaneOnObject2;
    [HideInInspector] public int PlayerShieldValue;

    public void StartFight()
    {
        SetEnnemis();
        FightSelectorComponent.LoadIteamBar();
        FightSelectorComponent.LoadStancePanel();
        CheckSpecialStat();
        IsFighting = true;
        CombatStat = 1;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsFighting)
        {
            //Debug.Log(CombatStat);
            CheckSpecialStat();
            LoadPlayerStats();

            if (Ennemi1CombatUIComponent.IsAlive) Ennemi1CombatUIComponent.shakeComponent.transform.position = Vector3.Lerp(Ennemi1CombatUIComponent.shakeComponent.transform.position, Ennemi1.transform.position, 0.05f);
            if (Ennemi2CombatUIComponent.IsAlive) Ennemi2CombatUIComponent.shakeComponent.transform.position = Vector3.Lerp(Ennemi2CombatUIComponent.shakeComponent.transform.position, Ennemi2.transform.position, 0.05f);
            if (Ennemi3CombatUIComponent.IsAlive) Ennemi3CombatUIComponent.shakeComponent.transform.position = Vector3.Lerp(Ennemi3CombatUIComponent.shakeComponent.transform.position, Ennemi3.transform.position, 0.05f);
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
                FightSelectorComponent.SetPanels();
                ResetShield();
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
    }

    public void StartCombat()
    {
        CombatStat = 3;
        FightSelectorComponent.SelectTime = false;
    }

    private void LoadPlayerStats()
    {
        TxtHP.text = PlayerInfoComponent.CurrentHP + "/" + PlayerInfoComponent.MaxHP;
        SliderHP.value = Mathf.Lerp(SliderHP.value, PlayerInfoComponent.CurrentHP / (float) PlayerInfoComponent.MaxHP,
            0.5f);
    }

    private void SetEnnemis()
    {
        switch (EnnemiGroupComponent.Ennemis.Count)
        {
            case 1:
                Ennemi2CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[0]);
                Ennemi2CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi2.GetComponent<ShakeComponent>();
                break;
            case 2:
                Ennemi1CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[0]);
                Ennemi1CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi1.GetComponent<ShakeComponent>();
                Ennemi3CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[1]);
                Ennemi3CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi3.GetComponent<ShakeComponent>();
                break;
            case 3:
                Ennemi1CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[0]);
                Ennemi1CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi1.GetComponent<ShakeComponent>();
                Ennemi2CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[2]);
                Ennemi2CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi2.GetComponent<ShakeComponent>();
                Ennemi3CombatUIComponent.SetPanel(EnnemiGroupComponent.Ennemis[1]);
                Ennemi3CombatUIComponent.shakeComponent = EnnemiGroupComponent.Ennemi3.GetComponent<ShakeComponent>();
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

        if (ennemi.IsAlive)
        {
            ennemi.ResetShild();
            if (ennemi.SpecialStat != null)
            {
               
                if (ennemi.SpecialStat is SOSpecialStatStun)
                {
                    switch (ennemiIndex)
                    {
                        case 1: CombatStat = 19; break;
                        case 2: CombatStat = 29; break;
                        case 3: CombatStat = 49; break;
                    }
                    Invoke("DelayChangeCombatStat", 0.5f);
                    return;
                }
                ennemi.SpecialStat.MakeEffect(this, ennemi);
                Invoke("DelayChangeCombatStat", 0.5f);
                return;
            }
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
            
                PlayerTakeDamage( ennemi.MakeAttack(this));
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
        bool isTwoHanded  =false;
        if (IsStaneOnObject1)
        {
            SelectedStance.SoStance.ExecutStance(this, 1);
        }
        else if (ItemData1.SoObject is SOWeapon)
        {
            List<EnnemiCombatUIComponent> targets;
            SOWeapon weapon = (SOWeapon) ItemData1.SoObject;
            int damage = PlayerStandardAttack(weapon);
            isTwoHanded = weapon.isTwoHand;
            targets = ChoseTarget(1, weapon.Target);
            foreach (var target in targets)
            {
                target.TakeDamage(damage);
            }
            if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(this, targets, 1))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(this, targets, 1);
                }
            }

        }
        else if (ItemData1.SoObject is SOShield)
        {
            SOShield shield = (SOShield) ItemData1.SoObject;
            AddShield(shield.ShieldValue);
        }
        else if (ItemData1.SoObject is SOUtilityItem)
        {
            SOUtilityItem soUtilityItem = (SOUtilityItem) ItemData1.SoObject;
            soUtilityItem.SoUtilityEffectGeneral.CombatUse(PlayerInfoComponent, this, 1);
        }

        if (!(ItemData1.SoObject is SOUtilityItem))
        {
            ItemData1.CurrantDurability--;
            if (ItemData1.CurrantDurability == 0){
                FightSelectorComponent.DestroyObject(ItemData1);
            }
        }
    

    if (IsVictory())
        {
            CombatStat = 100;
            return;
        }

    if (isTwoHanded)
    {
        CombatStat = 9;
        Invoke("DelayChangeCombatStat", 2f);
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
        else if (ItemData2. SoObject is SOWeapon)
        {
            List<EnnemiCombatUIComponent> targets;
            SOWeapon weapon = (SOWeapon) ItemData2.SoObject;
            int damage = PlayerStandardAttack(weapon);
            targets = ChoseTarget(2, weapon.Target);
            foreach (var target in targets)
            {
                target.TakeDamage(damage);
            }
            if (weapon.SpecialEffect != null)
            {
                if (weapon.SpecialEffect.CheckForUse(this, targets, 2))
                {
                    weapon.SpecialEffect.MakeSpecialEffect(this, targets , 2);
                }
            }
        }
        else if (ItemData2.SoObject is SOShield)
        {
            SOShield shield = (SOShield) ItemData2.SoObject;
            AddShield(shield.ShieldValue);
        }
        else if (ItemData2.SoObject is SOUtilityItem)
        {
            SOUtilityItem soUtilityItem = (SOUtilityItem)ItemData2.SoObject;
            soUtilityItem.SoUtilityEffectGeneral.CombatUse(PlayerInfoComponent,this,2);
        }
        if (!(ItemData2.SoObject is SOUtilityItem))
        {
            ItemData2.CurrantDurability--;
            if (ItemData2.CurrantDurability == 0){
                FightSelectorComponent.DestroyObject(ItemData2);
            }
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
    public List<EnnemiCombatUIComponent> ChoseTarget(int itemNumb, AttackTarget targetOrder)
    {
        List<EnnemiCombatUIComponent> ennemiCombatUIComponents=new List<EnnemiCombatUIComponent>();
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
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); return ennemiCombatUIComponents;} 
            }

            if (targetOrder == AttackTarget.Back)
            {
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); return ennemiCombatUIComponents;}
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); return ennemiCombatUIComponents;} 
            }

            if (targetOrder == AttackTarget.Splash)
            {
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); } 
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); } 
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); }
                return ennemiCombatUIComponents;
            }
        }

        if (itemNumb == 2)
        {
            if (targetOrder == AttackTarget.Front)
            {
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); return ennemiCombatUIComponents;} 
            }

            if (targetOrder == AttackTarget.Back)
            {
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); return ennemiCombatUIComponents;} 
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); return ennemiCombatUIComponents;} 
            }
            if (targetOrder == AttackTarget.Splash)
            {
                if (Ennemi1CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi1CombatUIComponent); } 
                if (Ennemi2CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi2CombatUIComponent); } 
                if (Ennemi3CombatUIComponent.IsAlive) {ennemiCombatUIComponents.Add(Ennemi3CombatUIComponent); }
                return ennemiCombatUIComponents;
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
        Debug.Log("Victoire");
        IsFighting = false;
        EnnemiGroupComponent.Die();
        HudComponent.EndFight();
        FightSelectorComponent.ResetPannels();
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

    public void AddShield(int add)
    {
        PlayerShieldValue += add;
        ImgShield.enabled = true;
        TxtShield.enabled = true;
        TxtShield.text = "" + PlayerShieldValue;
    }

    public void ResetShield()
    {
        ImgShield.enabled = false;
        TxtShield.enabled = false;
        PlayerShieldValue = 0;
    }

    public void PlayerTakeDamage(int damage)
    {
        PlayerInfoComponent.CurrentHP -= Mathf.Clamp((damage - PlayerShieldValue), 0, damage);
    }

}
