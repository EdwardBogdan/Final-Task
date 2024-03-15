using DeepSystem.General;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Weapon
{
    [CreateAssetMenu(fileName = "WeaponManager", menuName = "Player/Weapon/Manager")]
    public class WeaponManager : SystemOrigin<WeaponManager>
    {
        [SerializeField] private AttackManager _attackManager;
        [SerializeField] private WeaponProperty _armed;

        public static bool Armed => I._armed.Value;
        internal static AttackManager AttackManager => I._attackManager;
        public static void ListenArming(UnityAction func, bool arming, bool mode)
        {
            if (arming)
            {
                if (mode) I._armed.OnArmed.AddListener(func);
                else I._armed.OnArmed.RemoveListener(func);
            }
            else
            {
                if (mode) I._armed.OnDisarmed.AddListener(func);
                else I._armed.OnDisarmed.RemoveListener(func);
            }
        }
        public static void ListenChange(UnityAction<bool> func, bool mode)
        {
            if (mode) I._armed.OnChanged.AddListener(func);
            else I._armed.OnChanged.RemoveListener(func);
        }
        
        public void SetArming(bool value)
        {
            _armed.Value = value;
        }

        #region Init
        private const string AddressableKey = "WeaponManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion

        #region Mono
#if UNITY_EDITOR
        private void OnValidate()
        {
            _armed.Validate();
        }
#endif
        #endregion
    }
}
