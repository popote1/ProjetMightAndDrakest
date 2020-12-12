using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class EnnemiCombatUIComponent : MonoBehaviour
{
    public GameObject PanelEnnemi;
    public TMP_Text Name;
    public TMP_Text EnnemiHP;
    public Slider SilderHP;
    public Image ImgSpecialStat;
    public Image ImgShield;
    public TMP_Text TxtShield;
    public EnnemiInfo EnnemiInfo;
    public ShakeComponent shakeComponent;
    [HideInInspector]public bool IsAlive=false;
    private int tempsSpecialEffet;
    public int TempsSpecialEffet
    {
        get => tempsSpecialEffet;
        set
        {
            if (value <= 0)
            {
                value = 0;
                HideSpecialStat();
            }

            if (value > 0)
            {
                ShowSpecialStat();
            }
            tempsSpecialEffet = value;
            
        }
    }
    public SOSpecialStatGeneral SpecialStat;
    public int ShieldValue;

    public void Awake()
    {
        PanelEnnemi.SetActive(false);
        HideSpecialStat();
        ResetShild();
    }

    public void Update()
    {
        if (IsAlive) SilderHP.value = Mathf.Lerp(SilderHP.value, (float) EnnemiInfo.CurrentHP / EnnemiInfo.SoEnnemi.MaxHP, 0.5f);
        
    }


    public void SetPanel(EnnemiInfo ennemiInfo)
    {
        PanelEnnemi.SetActive(true);
        EnnemiInfo = ennemiInfo;
        Name.text = ennemiInfo.Name;
        EnnemiHP.text = ennemiInfo.CurrentHP + "/" + EnnemiInfo.SoEnnemi.MaxHP;
        SilderHP.value = (float)ennemiInfo.CurrentHP / EnnemiInfo.SoEnnemi.MaxHP;
        IsAlive = true;
        
    }

    public void TakeDamage(int damage)
    {
        EnnemiInfo.CurrentHP -= Mathf.Clamp((damage-ShieldValue),0,damage);
        if (EnnemiInfo.CurrentHP <= 0)
        {
            EnnemiInfo.CurrentHP = 0;
            HideSpecialStat();
            ResetShild();
            shakeComponent.GetComponent<MeshRenderer>().enabled=(false);
            IsAlive = false;
            
            Invoke("ClosePanel", 1.5f);
        }
        EnnemiHP.text = EnnemiInfo.CurrentHP + "/" + EnnemiInfo.SoEnnemi.MaxHP;
        shakeComponent.StartShake();
    }
    private void ClosePanel()
    {
     PanelEnnemi.SetActive(false);   
    }

    public int MakeAttack(FightComonent fightComonent)
    {
        
        return MakeAttack(ChoseAttack(),fightComonent);
    }

    private EnnemiAttack ChoseAttack()
    {
        float attackIndex = Random.Range(0, 100);
        float inerAttackIndex = 0;
        foreach (EnnemiAttack choseAttack in EnnemiInfo.SoEnnemi.Attacks)
        {
            inerAttackIndex += choseAttack.Probability;
            if ( attackIndex<=inerAttackIndex )
            {
                return choseAttack;
                
            }
        }
        Debug.Log("indexd'attaque est de " + attackIndex + " et le inerAttack est de " + inerAttackIndex);
        return null;
    }

    private int MakeAttack(EnnemiAttack attack,FightComonent fightComonent)
    {
        float generalChanceToHit = attack.SoAttack.ChanceToHit + EnnemiInfo.SoEnnemi.Dexterity;
        float hitRollDice = Random.Range(0, 100);
        if (generalChanceToHit >= hitRollDice)
        {
            if (attack.SoAttack.SpecialEffect != null)
            {
                if (attack.SoAttack.SpecialEffect.CheckForUseOnPlayer(fightComonent))
                {
                    attack.SoAttack.SpecialEffect.MakeSpecialEffectOnPlayer(fightComonent);
                }
            }
            if (hitRollDice <= 10)
            {
                Debug.Log(transform.name+"  "+attack.SoAttack.Name+" touche avec un "+hitRollDice+" sur "+generalChanceToHit+" en critique");
                return MakeDamage(true, attack);
            }
            Debug.Log(transform.name+"  "+attack.SoAttack.Name+" touche avec un "+hitRollDice+" sur "+generalChanceToHit);
            Instantiate(attack.SoAttack.FX,fightComonent.CenterScreen);
            SoundManager.PlaySound(attack.SoAttack.AudioClip,attack.SoAttack.volume);
            return MakeDamage(false, attack);
        }
        Debug.Log(transform.name+"  "+attack.SoAttack.Name+" rate avec un "+hitRollDice+" sur "+generalChanceToHit);
        return 0;
    }

    private int MakeDamage(bool isCritique, EnnemiAttack attack)
    {
        int damage = attack.SoAttack.Damage + EnnemiInfo.SoEnnemi.Strenght;
        if (isCritique) return damage * 2;
        return damage;
    }

    private void ShowSpecialStat()
    {
        ImgSpecialStat.enabled = true;
        ImgSpecialStat.sprite = SpecialStat.Sprite;
        Debug.Log("Affiche les statut");
    }

    private void HideSpecialStat()
    {
        ImgSpecialStat.enabled = false;
        SpecialStat = null;
    }

    public void AddShield(int value)
    {
        ShieldValue += value;
        TxtShield.enabled = true;
        ImgShield.enabled = true;
        TxtShield.text = "" + ShieldValue;
    }

    public void ResetShild()
    {
        TxtShield.enabled = false;
        ImgShield.enabled = false;
        ShieldValue = 0;
    }
}
