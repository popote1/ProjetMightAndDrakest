using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int CurrantDurability;
    public ItemData(SOObject item)
    {
        if (item = new SOWeapon())
        {
            SOWeapon weapon = (SOWeapon) item;
            CurrantDurability = weapon.Durability;
        }
    }
}
