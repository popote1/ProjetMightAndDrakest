using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UIElements;

public class FightComonent : MonoBehaviour
{
    [Header ("UI Selection")]
    public GameObject PanelObject;
    public GameObject PanelSlideObject;
    public GameObject PanelStance;
    public GameObject PanelSliderStance;
    public GameObject PanelSelectedObject1;
    public GameObject PanelSelectedObject2;
    public GameObject PanelSelectedStance;
    [Header("UI Info Perso")] 
    public Slider SliderHP;
    public TMP_Text TxtHP;
    public Image ImgSpecialStat;
    public GameObject PanelInfoSpecialStat;
    public TMP_Text TxtSpecialStatDamage;
    public TMP_Text TxtSpecialStatCharge;
    public TMP_Text TxtSpecialStatDescription;
    public Image ImgSelector;
    [Header("UI Ennemi1")]
    public GameObject PannelEnnemi1;
    public TMP_Text TxtEnnemi1Name;
    public TMP_Text TxtEnnemi1HP;
    public Slider SliderEnnemi1HP;
    public Image ImgEnnemi1Stat;
    [Header("UI Ennemi2")]
    public GameObject PannelEnnemi2;
    public TMP_Text TxtEnnemi2Name;
    public TMP_Text TxtEnnemi2HP;
    public Slider SliderEnnemi2HP;
    public Image ImgEnnemi2Stat;
    [Header("UI Ennemi3")]
    public GameObject PannelEnnemi3;
    public TMP_Text TxtEnnemi3Name;
    public TMP_Text TxtEnnemi3HP;
    public Slider SliderEnnemi3HP;
    public Image ImgEnnemi3Stat;

    public enum AttackTarget { Front,Invest,Back,Splash }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
