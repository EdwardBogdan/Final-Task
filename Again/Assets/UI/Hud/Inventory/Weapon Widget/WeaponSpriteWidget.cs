using InventorySystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class WeaponSpriteWidget : MonoBehaviour
    {
        [SerializeField] private Image _spirte;
        private void Awake()
        {
            WeaponManager.ListenWeaponChanged(OnWeaponChanged, true);

            if (WeaponManager.EquipedWeapon != null) OnWeaponChanged(WeaponManager.EquipedWeapon);
        }

        private void OnDestroy()
        {
            WeaponManager.ListenWeaponChanged(OnWeaponChanged, false);
        }

        private void OnWeaponChanged(ItemUnit item)
        {
            _spirte.sprite = WeaponManager.EquipedWeapon.Def.Icon;
        }
    }
}
