using InventorySystem;
using InventorySystem.ItemProperies;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class ItemReloadFill : MonoBehaviour
    {
        [SerializeField] private bool _reverse;
        [SerializeField] private Image _sprite;

        private ItemProperty _storedProperty;

        public void OnSetItem(ItemUnit item)
        {
            Subscribe(false);

            _storedProperty = item.Property;

            Subscribe(true);

            StartFilling();
        }

        private void StartFilling()
        {
            enabled = _sprite.enabled = true;

            _sprite.fillAmount = 0;
            _sprite.enabled = true;
        }

        private void Subscribe(bool value)
        {
            if (_storedProperty == null) return;

            _storedProperty.ListenUsing(StartFilling, value);
        }

        private void Filling()
        {
            float value = _storedProperty.ReloadStage;

            if (_reverse) value = 1 - value;

            _sprite.fillAmount = value;
        }

        private void FixedUpdate()
        {
            if (_storedProperty.ReloadReady) enabled = _sprite.enabled = false;

            Filling();
        }
    }
}
