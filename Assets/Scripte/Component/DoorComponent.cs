using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorComponent : MonoBehaviour , IInteracteble
{
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
        Debug.Log(" la porte est ouverte");

        transform.position += transform.forward;
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
