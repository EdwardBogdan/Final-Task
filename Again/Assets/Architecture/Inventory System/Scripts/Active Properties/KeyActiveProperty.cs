using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "KeyActiveProperty", menuName = "Inventory/Items/ActiveProperty/Key")]
    public class KeyActiveProperty : ItemProperty
    {
        protected override void OnUsedProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}
