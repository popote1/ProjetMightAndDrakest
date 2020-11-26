using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UtilityItem", menuName = "SO/Objects/UtilityItem") ]
public class SOUtilityItem : SOObject
{ 
    [TextArea]public string PracticalDescription;
    public SOUtilityEffectGeneral SoUtilityEffectGeneral;
}
