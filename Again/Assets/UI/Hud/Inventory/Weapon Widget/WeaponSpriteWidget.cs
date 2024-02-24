using Creature.Player.Arming;
using InventorySystem;
using InventorySystem.ItemProperies;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Inventory
{
    public class WeaponSpriteWidget : MonoBehaviour
    {
        private void Awake()
        {
            WeaponManager.ListenArming(OnArming, true, true);

            if (WeaponManager.Armed) OnArming();
        }

        private void OnDestroy()
        {
            //OldWeaponManager.ListenWeaponChanged(OnWeaponChanged, false);
        }

        private void OnArming()
        {
            //_spirte.sprite = OldWeaponManager.EquipedWeapon.Def.Icon;
        }
    }
}
