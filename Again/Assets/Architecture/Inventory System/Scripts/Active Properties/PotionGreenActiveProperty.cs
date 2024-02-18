using UnityEngine;

namespace InventorySystem.ItemProperies
{
    [CreateAssetMenu(fileName = "PotionGreenActiveProperty", menuName = "Inventory/Items/ActiveProperty/PotionGreen")]
    public class PotionGreenActiveProperty : ItemProperty
    {
        protected override void OnUsedProperty()
        {
            throw new System.NotImplementedException();
        }
    }
}
