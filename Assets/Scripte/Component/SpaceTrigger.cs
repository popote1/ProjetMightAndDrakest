using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SpaceTrigger : MonoBehaviour
{
    public bool DestroyOnUs =true;
    public UnityEvent OncollitionEnter=new UnityEvent();
    public void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("Player"))
        {
            OncollitionEnter.Invoke();
            if(DestroyOnUs)Destroy(gameObject);
        }
    }
    
}
