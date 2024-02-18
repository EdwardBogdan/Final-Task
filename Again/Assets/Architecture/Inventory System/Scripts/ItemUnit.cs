using InventorySystem.ItemProperies;
using InventorySystem.Properties;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    [CreateAssetMenu(menuName = "Inventory/Items/Unit", fileName = "Item Unit")]
    public class ItemUnit : ScriptableObject
    {
        [SerializeField] private ItemType _type;
        [SerializeField] private ItemDef _def;
        [SerializeField] private ItemProperty _itemPropertry;

        public ItemType Type => _type;
        public ItemDef Def => _def;
        public ItemProperty Property => _itemPropertry;

        #region Count
        [SerializeField] private ItemCountProperty _count;
        public int Count => _count.Value;
        public int MaxCount => _count.ValueMax;

        internal bool TryAdd(int value)
        {
            int oldCount = Count;

            if (oldCount >= MaxCount) return false;

            if (value < 0) return false;

            _count.Value = oldCount + value;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif
            return true;
        }
        internal bool TryRemove(int count)
        {
            int oldCount = Count;

            if (count > oldCount) return false;

            _count.Value = oldCount - count;

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(this);
#endif

            return true;
        }
        #endregion

        #region Count Events
        public void ListenCountEvent(UnityAction<int, int> func, bool mode)
        {
            if (mode) _count.OnCountChanged.AddListener(func);
            else _count.OnCountChanged.RemoveListener(func);
        }
        public void ListenMaxCountEvent(UnityAction<int, int> func, bool mode)
        {
            if (mode) _count.OnMaxChanged.AddListener(func);
            else _count.OnMaxChanged.RemoveListener(func);
        }

        //For UnityEvent
        public void OnCountChanged(int value, int oldValue)
        {
            if (oldValue == 0) InventoryManager.AddItem(this);
            else if (value == 0) InventoryManager.RemoveItem(this);
        }
        #endregion

        #region GeneralEvents
        [SerializeField] private GeneralEvents _generalEvents;

        public void ListenProperty(GeneralPropertyType eventType, UnityAction<ItemUnit> action, bool subscribe)
        {
            switch (eventType)
            {
                case GeneralPropertyType.Taked:
                    if (subscribe) _generalEvents._onTakedEvent.AddListener(action);
                    else _generalEvents._onTakedEvent.RemoveListener(action);
                    break;
                case GeneralPropertyType.Removed:
                    if (subscribe) _generalEvents._onRemovedEvent.AddListener(action);
                    else _generalEvents._onRemovedEvent.RemoveListener(action);
                    break;
                case GeneralPropertyType.Selected:
                    if (subscribe) _generalEvents._onSelectedEvent.AddListener(action);
                    else _generalEvents._onSelectedEvent.RemoveListener(action);
                    break;
                case GeneralPropertyType.Deselected:
                    if (subscribe) _generalEvents._onDeselectedEvent.AddListener(action);
                    else _generalEvents._onDeselectedEvent.RemoveListener(action);
                    break;
            }
        }
        internal void GeneralProperty(GeneralPropertyType eventType)
        {
            switch (eventType)
            {
                case GeneralPropertyType.Taked:
                    _generalEvents._onTakedEvent?.Invoke(this);
                    break;

                case GeneralPropertyType.Removed:
                    _generalEvents._onRemovedEvent?.Invoke(this);
                    break;

                case GeneralPropertyType.Selected:
                    _generalEvents._onSelectedEvent?.Invoke(this);
                    break;

                case GeneralPropertyType.Deselected:
                    _generalEvents._onDeselectedEvent?.Invoke(this);
                    break;

                default:
                    break;
            }
        }

        [Serializable]
        private class GeneralEvents
        {
            [SerializeField] internal UnityEvent<ItemUnit> _onTakedEvent;
            [SerializeField] internal UnityEvent<ItemUnit> _onRemovedEvent;
            [SerializeField] internal UnityEvent<ItemUnit> _onSelectedEvent;
            [SerializeField] internal UnityEvent<ItemUnit> _onDeselectedEvent;
        }
        #endregion

        #region Editor
#if UNITY_EDITOR
        private void OnValidate()
        {
            _count.Validate();
        }
#endif
        #endregion
    }
}
