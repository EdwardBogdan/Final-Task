using GameSystem.General;
using Property.TimeProperty;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Arming
{
    [CreateAssetMenu(fileName = "WeaponManager", menuName = "Player/Weapon/Manager")]
    public class WeaponManager : SystemOrigin<WeaponManager>
    {
        [SerializeField] private WeaponProperty _arming;

        
        public static bool Armed => I._arming.Value;
        public static void ListenArming(UnityAction func, bool arming, bool mode)
        {
            if (arming)
            {
                if (mode) I._arming.OnArmed.AddListener(func);
                else I._arming.OnArmed.RemoveListener(func);
            }
            else
            {
                if (mode) I._arming.OnDisarmed.AddListener(func);
                else I._arming.OnDisarmed.RemoveListener(func);
            }
        }
        public static void ListenChange(UnityAction<bool> func, bool mode)
        {
            if (mode) I._arming.OnChanged.AddListener(func);
            else I._arming.OnChanged.RemoveListener(func);
        }
        

        public void SetArming(bool value)
        {
            _arming.Value = value;
        }

        #region Attack 
        [SerializeField] private Cooldown _attackReload;
        [SerializeField] private UnityEvent _attackEvent;
       
        public static void Attack()
        {
            if (!I._attackReload.IsReady) return;
            I._attackReload.Reset();
            I._attackEvent?.Invoke();
        }
        public static void ListenAttack(UnityAction func, bool mode)
        {
            if (mode) I._attackEvent.AddListener(func);
            else I._attackEvent.RemoveListener(func);
        }
        #endregion

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
            _arming.Validate();
        }
#endif
        private void OnEnable()
        {
            _attackReload.Ready();
        }
        #endregion
    }
}
