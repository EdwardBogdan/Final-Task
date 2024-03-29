using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    public class DamageComponent : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _damage = 1;

        [SerializeField] private UnityEvent<GameObject> _attackBuff;

        public void OnDealDamage(GameObject go)
        {
            if (go == null | !go.TryGetComponent(out IHealth healthComponent)) return;

            if (!healthComponent.ReadyForDamage) return;

            healthComponent.ApplyDamage(_damage);

            _attackBuff?.Invoke(go);
        }
    }
}
