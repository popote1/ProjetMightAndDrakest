using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PlayerInfoComponent : MonoBehaviour
{
    public int InventoryLengths;
    public int Strengths;
    public int Dexerity;
    public int MaxHP;
    public int StartHP;
    public List<SOObject> SOInventory;
    public List<ItemData> Inventory=new List<ItemData>();
    public List<SOStanceGeneral> SOStance;
    public ShakeComponent shakeComponent;

    public int CurrentHP
    {
        get => currentHp;
        set
        {
            if (value < currentHp)
            {
                shakeComponent.StartShake();
            }
            currentHp = value;
            if (CurrentHP <= 0)
            {
                GetComponent<HUDComponent>().Defaite();
            }
        }
    }
    private int currentHp;


    private int tempsSpecialEffect;

    public int TempsSpecialEffect
    {
        get => tempsSpecialEffect;
        set
        {
            tempsSpecialEffect = value;
            if (tempsSpecialEffect <= 0)
            {
                tempsSpecialEffect = 0;
                SpecialStat = null;
            }
        }
    }
    public SOSpecialStatGeneral SpecialStat;
   
    
    
    void Awake()
    {
        foreach (SOObject item in SOInventory) {
            Inventory.Add(new ItemData(item));
            Debug.Log("ajout "+item.Name+" à l'aventaire");
        }
        
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        CurrentHP = StartHP;
    }
   
    
  
}
