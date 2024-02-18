using InventorySystem.ItemProperies;
using UnityEngine;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "ActiveItemController", menuName = "Inventory/ActiveItemController")]
    public class SelectItemManager : ScriptableObject
    {
        [EditorAttributes.EditorReadOnly, SerializeField] private ItemUnit _selectedItem;

        public static ItemUnit SelectedItem => I._selectedItem;
        private static SelectItemManager I => InventoryManager.ItemManager;

        public static void SelectItem(int index)
        {
            // Проверка на валидность индекса
            int itemCount = InventoryManager.Items.Count;
            if (index < 0 || index >= itemCount) return;

            var newItem = InventoryManager.Items[index];
            SelectItem(newItem);
        }
        public static void SelectItem(ItemUnit item)
        {
            // Если уже есть выбранный элемент, снимаем с него выделение
            var currentItem = SelectedItem;
            if (currentItem != null)
            {
                currentItem.GeneralProperty(GeneralPropertyType.Deselected);
            }

            I._selectedItem = item;
            item.GeneralProperty(GeneralPropertyType.Selected);
        }

        public void OnItemAdded(ItemUnit item)
        {
            item.GeneralProperty(GeneralPropertyType.Taked);

            // Взять новый единственный предмет, если ещё нет выбранного
            if (SelectedItem == null) SelectItem(item);
        }
        public void OnItemRemoved(ItemUnit item)
        {
            item.GeneralProperty(GeneralPropertyType.Removed);

            if (SelectedItem == item)
            {
                int itemCount = InventoryManager.Items.Count;
                if (itemCount > 0) SelectItem(0);
                else I._selectedItem = null;
            }
        }
    }
}