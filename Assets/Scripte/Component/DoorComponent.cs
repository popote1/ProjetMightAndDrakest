using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoorComponent : MonoBehaviour , IInteracteble
{
    public Sprite Icone;
    public GameObject UIImage;
    public Vector3 NewPos;

    private GameObject _panel;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Intecract(InteractComponent.SelectStat selectStat)
    {
       
    }

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
        transform.position = NewPos;
        transform.Rotate(0,-90,0);
        Destroy(_panel);
        Destroy(this);
        return true;
    }
}
