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

    public void Intecract()
    {
        Debug.Log(" Le bouton est apuiller");
        Event.Invoke();
    }
}
