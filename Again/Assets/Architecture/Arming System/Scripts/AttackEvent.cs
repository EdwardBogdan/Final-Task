using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Arming
{
    public class AttackEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _attackEvent;

        private void Awake()
        {
            WeaponManager.ListenAttack(Event, true);
        }
        private void OnDestroy()
        {
            WeaponManager.ListenAttack(Event, false);
        }

        private void Event()
        {
            _attackEvent?.Invoke();
        }
    }
}
