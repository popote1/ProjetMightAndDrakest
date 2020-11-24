using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInfoComponent : MonoBehaviour
{
    public int InventoryLengths;
    public int Strengths;
    public int Dexerity;
    public int MaxHP;
    public int CurrentHP;
    public List<SOObject> SOInventory;
    public List<ItemData> Inventory=new List<ItemData>();
    public List<SOStanceGeneral> SOStance;
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

    // Update is called once per frame
    void Update()
    {
        
    }

  
}
