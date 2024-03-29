using Property.TimeProperty;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Weapon
{
    [CreateAssetMenu(fileName = "AttackManager", menuName = "Player/Weapon/AttackManager")]
    public class AttackManager : ScriptableObject
    {
        [SerializeField] private Cooldown _attackReload = new(0);
        [SerializeField] private UnityEvent _attackEvent;

        private static AttackManager I => WeaponManager.AttackManager;

        public static void Attack()
        {
            if (!WeaponManager.Armed) return;

            if (!I._attackReload.IsReady) return;
            I._attackReload.Reset();
            I._attackEvent?.Invoke();
        }
        public static void ListenAttack(UnityAction func, bool mode)
        {
            if (mode) I._attackEvent.AddListener(func);
            else I._attackEvent.RemoveListener(func);
        }

        #region Mono
        private void OnEnable()
        {
            _attackReload.Ready();
        }
        #endregion
    }
}
