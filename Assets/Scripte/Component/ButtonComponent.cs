using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonComponent : MonoBehaviour ,IInteracteble
{
    public UnityEvent Event;
    
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
        Debug.Log(" Le bouton est apuiller");
        Event.Invoke();
    }
    public void SetPreselctable()
    {
        throw new System.NotImplementedException();
    }

    public void SetSelectable()
    {
        throw new System.NotImplementedException();
    }

    public void DePreselectable()
    {
        throw new System.NotImplementedException();
    }

    public void DesetSelectable()
    {
        throw new System.NotImplementedException();
    }

    public bool USe(PlayerInfoComponent playerInfoComponent)
    {
        throw new System.NotImplementedException();
    }
    
}
