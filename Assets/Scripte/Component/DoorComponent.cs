using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DoorComponent : MonoBehaviour , IInteracteble
{
    public Sprite Icone;
    public GameObject UIImage;
    public Vector3 NewPos;
    public AudioClip audioClip;
    [Range(0,1)]public float Volume;
    public Transform UIPoint;
    [Header("KeyObject")] public SOObject Key;
    public UnityEvent succesToOpen = new UnityEvent();
    public UnityEvent FailToOpen = new UnityEvent();

    private GameObject _panel;
    
    // Start is called before the first frame update
    void Start()
    {
        if (UIPoint == null)
        {
            UIPoint = transform;
        }
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
        _panel.GetComponent<FollowWorldUIComponent>().lookAt = UIPoint;
        _panel.GetComponent<Image>().sprite = Icone;
    }

    public void DePreselectable()
    {
        if (_panel != null)
        {
            _panel.transform.localScale = Vector3.one;
        }
    }

    public void DesetSelectable()
    {
        Destroy(_panel);
    }

    public bool USe(PlayerInfoComponent playerInfoComponent)
    {
        if (Key != null)
        {
            bool canOpen = false;
            foreach (ItemData item in playerInfoComponent.Inventory)
            {
                if (item.SoObject == Key)
                {
                    canOpen = true;
                    break;
                }
            }

            if (canOpen)
            {
                succesToOpen.Invoke();
                transform.localPosition = NewPos;
                transform.Rotate(0, -90, 0);
                Destroy(_panel);
                SoundManager.PlaySound(audioClip, Volume);
                Destroy(this);
                return true;
            }
            else
            {
                FailToOpen.Invoke();
                return false;
            }
        }
        else
        {
            succesToOpen.Invoke();
            transform.localPosition = NewPos;
            transform.Rotate(0, -90, 0);
            Destroy(_panel);
            SoundManager.PlaySound(audioClip, Volume);
            Destroy(this);
            return true;
        }
    }
}
