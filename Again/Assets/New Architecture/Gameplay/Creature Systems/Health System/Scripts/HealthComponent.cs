using HealthSystem.Property;
using Property.TimeProperty;
using UnityEngine;

namespace HealthSystem
{
    public class HealthComponent : MonoBehaviour, IHealth
    {
        #region Immortal
        [SerializeField] private bool _immortal;
        [SerializeField] private Cooldown _immunityCd;
        public bool ReadyForDamage => _immunityCd.IsReady;

        public void SetImmortal(bool value)
        {
            _immortal = value;
        }
        #endregion

        [SerializeField] private HealthProperty _health;
        public HealthProperty Health => _health;

        public void ApplyHeal(int value)
        {
            _health.ApplyHeal(value);
        }
        public void ApplyDamage(int damage)
        {
            if (_immortal) damage = 0;

            _immunityCd.Reset();

            _health.ApplyDamage(damage);
        }
        public void SetHealth(int value)
        {
            if (value <= 0)
            {
                Destroy(gameObject);
                return;
            }
            _health.Value = value;
        }

        #region Mono
        private void Start()
        {
            _immunityCd.Ready();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            _health.Validate();
        }
#endif
        #endregion
    }
}
