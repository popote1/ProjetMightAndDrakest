using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

public class EnnemiCombatUIComponent : MonoBehaviour
{
    public GameObject PanelEnnemi;
    public TMP_Text Name;
    public TMP_Text EnnemiHP;
    public Slider SilderHP;
    public Image ImgSpecialStat;
    public EnnemiInfo EnnemiInfo;
    [HideInInspector]public bool IsAlive;

    public void Awake()
    {
        PanelEnnemi.SetActive(false);
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
}
