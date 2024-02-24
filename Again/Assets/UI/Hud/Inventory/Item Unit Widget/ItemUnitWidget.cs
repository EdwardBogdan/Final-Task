using InventorySystem;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Hud.Inventory
{
    public class ItemUnitWidget : MonoBehaviour
    {
        [SerializeField] private UnityEvent<ItemUnit> OnSetItem;

        public ItemUnit ItemTarget
        {
            set => OnSetItem?.Invoke(value);
        }
    }
}
