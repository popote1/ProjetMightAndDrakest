using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface IInteracteble
{
     void Intecract(InteractComponent.SelectStat selectStat);
     void DesetSelectable();
     void SetSelectable();

     void SetPreselctable();
     void DePreselectable();

     bool USe(PlayerInfoComponent playerInfoComponent);
}
