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
