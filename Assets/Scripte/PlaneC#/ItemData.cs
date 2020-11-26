using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData
{
    public int CurrantDurability;
    public SOObject SoObject;
    public ItemData(SOObject item)
    {
        SoObject = item;
        if (item is SOWeapon)
        {
            SOWeapon weapon = (SOWeapon) item;
            CurrantDurability = weapon.Durability;
        }
        if (item is SOShield)
        {
            SOShield shield= (SOShield) item;
            CurrantDurability = shield.Durability;
        }
    }
}
