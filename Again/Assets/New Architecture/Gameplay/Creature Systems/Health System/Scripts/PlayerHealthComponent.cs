using HealthSystem.Property;
using Property.TimeProperty;
using UnityEngine;

namespace HealthSystem
{
    public class PlayerHealthComponent : MonoBehaviour, IHealth
    {
        public HealthProperty Health => PlayerHealthManager.PlayerHealth;

        public void ApplyDamage(int damage)
        {
            if (_immortal) damage = 0;
            _immunityCd.Reset();

            PlayerHealthManager.PlayerHealth.ApplyDamage(damage);
        }
        public void ApplyHeal(int value)
        {
            PlayerHealthManager.PlayerHealth.ApplyHeal(value);
        }

        #region Immortal
        [SerializeField] private bool _immortal;
        [SerializeField] private Cooldown _immunityCd;
        public bool ReadyForDamage => _immunityCd.IsReady;

        public void SetImmortal(bool value)
        {
            _immortal = value;
        }
        #endregion

        #region Events
        [SerializeField] private HealthEvents _events;

        private void OnCountChanged(int value, int oldValue)
        {
            _events.OnCountChanged?.Invoke(value, oldValue);
        }
        private void OnMaxChanged(int value, int oldValue)
        {
            _events.OnMaxChanged?.Invoke(value, oldValue);
        }
        private void OnHit()
        {
            _events.OnHit?.Invoke();
        }
        private void OnHeal()
        {
            _events.OnHeal?.Invoke();
        }
        private void OnDeath()
        {
            _events.OnDeath?.Invoke();
        }
        #endregion

        #region Mono
        private void Awake()
        {
            Health.ListenCountChanged(OnCountChanged, true);
            Health.ListenMaxChanged(OnMaxChanged, true);
            Health.ListenHit(OnHit, true);
            Health.ListenHeal(OnHeal, true);
            Health.ListenDeath(OnDeath, true);
        }
        private void Start()
        {
            _immunityCd.Ready();
        }
        private void OnDestroy()
        {
            Health.ListenCountChanged(OnCountChanged, false);
            Health.ListenMaxChanged(OnMaxChanged, false);
            Health.ListenHit(OnHit, false);
            Health.ListenHeal(OnHeal, false);
            Health.ListenDeath(OnDeath, false);
        }
        #endregion
    }
}
