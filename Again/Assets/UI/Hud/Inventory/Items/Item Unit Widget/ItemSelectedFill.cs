using InventorySystem;
using InventorySystem.ItemProperies;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class ItemSelectedFill : MonoBehaviour
    {
        private ItemUnit storedItem;

        public void OnSetItem(ItemUnit item)
        {
            Subscribe(false);

            storedItem = item;

            Subscribe(true);

            if (SelectItemManager.SelectedItem == storedItem) OnSelected(storedItem);
            else OnDeselected(storedItem);
        }

        private void Subscribe(bool value)
        {
            if (storedItem == null) return;

            storedItem.ListenProperty(GeneralPropertyType.Selected, OnSelected, value);
            storedItem.ListenProperty(GeneralPropertyType.Deselected, OnDeselected, value);
        }

        private void OnSelected(ItemUnit item)
        {
            gameObject.SetActive(true);
        }
        private void OnDeselected(ItemUnit item)
        {
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            Subscribe(false);
        }
    }
}
