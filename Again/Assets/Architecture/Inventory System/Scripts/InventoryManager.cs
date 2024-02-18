using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.Events;
using GameSystem.General;
using System;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "InventoryManager", menuName = "Inventory/InventoryManager")]
    public class InventoryManager : SystemOrigin<InventoryManager>
    {
        #region Controller Refs
        [SerializeField] private SelectItemManager _itemController;
        [SerializeField] private WeaponManager _weaponController;
        internal static SelectItemManager ItemManager => I._itemController;
        internal static WeaponManager WeaponManager => I._weaponController;
        #endregion

        #region Item List
        [SerializeField, HideInInspector] private List<ItemUnit> items = new();
        public static IReadOnlyList<ItemUnit> Items => I.items;

        internal static bool AddItem(ItemUnit item)
        {
            bool added = !I.items.Contains(item);

            if (added)
            {
                I.items.Add(item);

                I.items = I.items.OrderBy(x => x.Def.Slot).ToList();

                I.events.OnUnitAdded?.Invoke(item);

                I.events.OnUnitCountChanged?.Invoke(Items);
                
#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(I);
#endif
            }

            return added;
        }
        internal static bool RemoveItem(ItemUnit item)
        {
            bool removed = I.items.Remove(item);

            if (removed)
            {
                I.events.OnUnitRemoved?.Invoke(item);

                I.events.OnUnitCountChanged?.Invoke(Items);

#if UNITY_EDITOR
                UnityEditor.EditorUtility.SetDirty(I);
#endif
            }

            return removed;
        }
        #endregion

        #region Events
        [SerializeField] private Events events;

        public static void ListenItemAdded(UnityAction<ItemUnit> func, bool mode)
        {
            if (mode) I.events.OnUnitAdded.AddListener(func);
            else I.events.OnUnitAdded.RemoveListener(func);
        }
        public static void ListenItemRemoved(UnityAction<ItemUnit> func, bool mode)
        {
            if (mode) I.events.OnUnitRemoved.AddListener(func);
            else I.events.OnUnitRemoved.RemoveListener(func);
        }
        public static void ListenUnitCountChanged(UnityAction<IReadOnlyList<ItemUnit>> func, bool mode)
        {
            if (mode) I.events.OnUnitCountChanged.AddListener(func);
            else I.events.OnUnitCountChanged.RemoveListener(func);
        }

        [Serializable]
        private class Events
        {
            public UnityEvent<ItemUnit> OnUnitAdded;

            public UnityEvent<ItemUnit> OnUnitRemoved;

            public UnityEvent<IReadOnlyList<ItemUnit>> OnUnitCountChanged;
        }
        #endregion

        #region Init
        private const string AddressableKey = "InventoryManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
