using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem
{
    [CreateAssetMenu(fileName = "WeaponManager", menuName = "Inventory/WeaponManager")]
    public class WeaponManager : ScriptableObject
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private ItemUnit _choosedWeapon;

        [SerializeField, HideInInspector] private List<ItemUnit> weaponList = new();

        public static ItemUnit EquipedWeapon => I._choosedWeapon;
        private static WeaponManager I => InventoryManager.WeaponManager;

        public void ChooseWeapon(ItemUnit item)
        {
            _choosedWeapon = item;
            if (_choosedWeapon != null)
            {
                events.OnWeaponChanged?.Invoke(_choosedWeapon);
            }
        }
        public void OnWeaponAdded(ItemUnit item)
        {
            if (item.Type != ItemType.Weapon) return;

            weaponList.Add(item);

            events.OnWeaponAdded?.Invoke(item);

            if (_choosedWeapon == null)
            {
                ChooseWeapon(item);
                SelectItemManager.SelectItem(item);
            }
#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(I);
#endif
        }
        public void OnWeaponRemoved(ItemUnit item)
        {
            if (item.Type != ItemType.Weapon) return;

            weaponList.Remove(item);

            if (weaponList.Count == 0)
            {
                ChooseWeapon(null);
                events.OnNoWeapon?.Invoke(item);
            }
            else ChooseWeapon(weaponList[0]);

#if UNITY_EDITOR
            UnityEditor.EditorUtility.SetDirty(I);
#endif
        }

        #region Events
        [SerializeField] private Events events;

        public static void ListenWeaponAdded(UnityAction<ItemUnit> func, bool mode)
        {
            if (mode) I.events.OnWeaponAdded.AddListener(func);
            else I.events.OnWeaponAdded.RemoveListener(func);
        }
        public static void ListenWeaponChanged(UnityAction<ItemUnit> func, bool mode)
        {
            if (mode) I.events.OnWeaponChanged.AddListener(func);
            else I.events.OnWeaponChanged.RemoveListener(func);
        }
        public static void ListenNoWeapon(UnityAction<ItemUnit> func, bool mode)
        {
            if (mode) I.events.OnNoWeapon.AddListener(func);
            else I.events.OnNoWeapon.RemoveListener(func);
        }

        [Serializable]
        private class Events
        {
            public UnityEvent<ItemUnit> OnWeaponAdded;

            public UnityEvent<ItemUnit> OnWeaponChanged;

            public UnityEvent<ItemUnit> OnNoWeapon;
        }
        #endregion
    }
}
