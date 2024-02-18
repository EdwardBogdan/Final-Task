using PlayerSystems;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "SwordProperty", menuName = "Inventory/Items/Property/Sword")]
    public class SwordProperty : WeaponProperty
    {
        protected override void OnUsedProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}