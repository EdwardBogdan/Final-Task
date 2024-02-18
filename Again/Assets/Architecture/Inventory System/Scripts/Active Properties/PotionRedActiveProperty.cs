using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "PotionRedActiveProperty", menuName = "Inventory/Items/ActiveProperty/PotionRed")]
    public class PotionRedActiveProperty : ItemProperty
    {
        protected override void OnUsedProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}
