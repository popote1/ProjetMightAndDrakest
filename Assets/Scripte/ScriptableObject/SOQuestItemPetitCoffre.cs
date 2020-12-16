using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "SOQuestItemPetitCoffre",menuName = "SO/Objects/QuestItemPetitCoffre")]
public class SOQuestItemPetitCoffre : SOQuestItem
{

    
    public SOObject NewItem;
    public override bool CheckWorldUse()
    {
        return true;
    }

    public override void WorldUse(InventoryComponent inventoryComponent)
    {
        inventoryComponent.AddItemsToInventory(NewItem);
        inventoryComponent.DestroySelectedItem();
    }
}
