using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StanceHolder : MonoBehaviour ,IInteracteble
{
    public SOStanceGeneral stance;
    public Sprite Icone;
    public GameObject UIImage;
    public AudioClip Clip;
    [Range(0, 1)] public float Volume=1;
    
    private GameObject _panel;
    public void SetPreselctable()
    {
        _panel.transform.localScale = Vector3.one*2;
    }

    public void SetSelectable()
    {
        Transform Canevas = GameObject.Find("Canvas").GetComponent<Canvas>().transform;
        _panel = Instantiate(UIImage, Canevas);
        _panel.AddComponent<FollowWorldUIComponent>();
        _panel.GetComponent<FollowWorldUIComponent>().lookAt = transform;
        _panel.GetComponent<Image>().sprite = Icone;
    }

    public void DePreselectable()
    {
        _panel.transform.localScale = Vector3.one;
    }

    public void DesetSelectable()
    {
        Destroy(_panel);
    }

    public bool USe(PlayerInfoComponent playerInfoComponent)
    {
        Destroy(_panel);
        playerInfoComponent.SOStance.Add(stance);
        Destroy(gameObject);
        
        SoundManager.PlaySound(Clip,Volume);
        return true;
    }

    public void SelfDestroy()
    {
        Destroy(this , 0.1f);
    }
    public void Intecract(InteractComponent.SelectStat selectStat)
    {
       
    }
}
