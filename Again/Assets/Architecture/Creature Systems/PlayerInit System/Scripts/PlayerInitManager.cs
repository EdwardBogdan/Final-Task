using GameSystem.General;
using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.PlayerInit
{
    [CreateAssetMenu(fileName = "PlayerInitManager", menuName = "Player/Init/Manager")]
    public class PlayerInitManager : SystemOrigin<PlayerInitManager>
    {
        [SerializeField] private UnityEvent _onPlayerCreated;
        [SerializeField] private UnityEvent _onPlayerDestroyed;

        public static event Action OnPlayerCreated;
        public static event Action OnPlayerDestroyed;

        internal static void OnCreated()
        {
            I._onPlayerCreated?.Invoke();
            OnPlayerCreated?.Invoke();
        }
        internal static void OnDestroyed()
        {
            I._onPlayerDestroyed?.Invoke();
            OnPlayerDestroyed?.Invoke();
        }

        #region Init
        private const string AddressableKey = "PlayerInitManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
