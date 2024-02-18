using InventorySystem;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class WeaponFillWidget : MonoBehaviour
    {
        private void Awake()
        {
            //WeaponManager.ListenWeaponAdded(OnWeaponAdded, true);
            //WeaponManager.ListenNoWeapon(OnNoWeapon, true);
        }
        private void Start()
        {
            //if (WeaponManager.EquipedWeapon == null) gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            //WeaponManager.ListenWeaponAdded(OnWeaponAdded, false);
            //WeaponManager.ListenNoWeapon(OnNoWeapon, false);
        }

        private void OnWeaponUsed(ItemUnit item)
        {
            gameObject.SetActive(true);
        }
    }
}
