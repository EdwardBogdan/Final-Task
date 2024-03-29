using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class ItemIconWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        public void OnSetItem(ItemUnit item)
        {
            _icon.sprite = item.Def.Icon;
        }
    }
}
