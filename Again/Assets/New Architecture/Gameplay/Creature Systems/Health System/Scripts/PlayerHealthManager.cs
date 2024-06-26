using GameSystem.Initialization;
using HealthSystem.Property;
using UnityEngine;

namespace HealthSystem
{
    [CreateAssetMenu(fileName = "HealthManager", menuName = "Player/Health/Manager")]
    public class PlayerHealthManager : SystemOrigin<PlayerHealthManager>
    {
        [SerializeField] private HealthProperty _health;
        public static HealthProperty PlayerHealth => I._health;

        #region UnityAction
        public void OnSetHealth(int value, int oldValue)
        {
            _health.Value = value;
        }
        public void OnSetMaxHealth(int value, int oldValue)
        {
            _health.ValueMax = value;
        }
        public void ChangeHealth(int value)
        {
            if (value > 0) _health.ApplyHeal(value);
            else if (value < 0) _health.ApplyDamage(value);
        }
        public void ChangeMaxHealth(int value)
        {
            _health.ValueMax += value;
        }
        #endregion

        #region Init
        private const string Group = "Player";
        private const string AddressableKey = "HealthManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion

        #region Editor
#if UNITY_EDITOR
        private void OnValidate()
        {
            _health.Validate();
        }
#endif
        #endregion
    }
}

