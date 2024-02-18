using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class ItemUnitWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private LineSectionWidget _line;

        private ItemUnit _storedItemUnit;
        public ItemUnit ItemTarget
        {
            set
            {
                Subscribe(false);

                _storedItemUnit = value;

                Subscribe(true);

                _icon.sprite = _storedItemUnit.Def.Icon;

                _line.SectionMaxCount = _storedItemUnit.MaxCount;

                _line.SectionCount = _storedItemUnit.Count;
            }
        }

        #region Subscribe
        private void Subscribe(bool value)
        {
            if (_storedItemUnit == null) return;

            _storedItemUnit.ListenCountEvent(OnChargesChanged, value);
            _storedItemUnit.ListenMaxCountEvent(OnMaxChargesChanged, value);
        }
        private void OnChargesChanged(int value, int oldValue)
        {
            if (value != _storedItemUnit.Count) return; // <= FUCKING CRUTCH, FIND WAY TO FIX IT

            _line.SectionCount = value;
        }
        private void OnMaxChargesChanged(int value, int oldValue)
        {
            _line.SectionMaxCount = value;
        }
        private void OnDestroy()
        {
            Subscribe(false);
        }
        #endregion
    }
}
