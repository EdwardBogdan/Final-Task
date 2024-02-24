using Creature.Player.Arming;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class BorderWeapon : MonoBehaviour
    {
        private void Awake()
        {
            WeaponManager.ListenChange(OnChange, true);
        }
        private void Start()
        {
            OnChange(WeaponManager.Armed);
        }
        private void OnDestroy()
        {
            WeaponManager.ListenChange(OnChange, false);
        }

        private void OnChange(bool value)
        {
            gameObject.SetActive(value);
        }
    }
}
