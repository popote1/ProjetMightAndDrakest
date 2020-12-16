
using UnityEngine;

[CreateAssetMenu(fileName = "SOQuestItem", menuName = "SO/Objects/QuestItem")]
public class SOQuestItem : SOObject
{
    [TextArea] public string PracticalDescription;
    
    public virtual bool CheckWorldUse()
    {
        return false; }

    public virtual void WorldUse(InventoryComponent inventoryComponent)
    {
        
    }
}
