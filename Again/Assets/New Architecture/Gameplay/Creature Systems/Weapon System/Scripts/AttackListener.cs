using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Weapon
{
    public class AttackListener : MonoBehaviour
    {
        [SerializeField] private UnityEvent _attackEvent;

        private void Invoke()
        {
            _attackEvent?.Invoke();
        }

        private void OnEnable()
        {
            AttackManager.ListenAttack(Invoke, true);
        }
        private void OnDisable()
        {
            AttackManager.ListenAttack(Invoke, false);
        }

        
    }
}
