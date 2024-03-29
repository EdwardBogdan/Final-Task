using GameSystem.Initialization;
using SceneManagment;
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

        public static void ListenOnCreate(UnityAction func, bool mode = true)
        {
            if (mode) I._onPlayerCreated.AddListener(func);
            else I._onPlayerCreated.RemoveListener(func);
        }
        public static void ListenOnDeastroy(UnityAction func, bool mode = true)
        {
            if (mode) I._onPlayerDestroyed.AddListener(func);
            else I._onPlayerDestroyed.RemoveListener(func);
        }

        internal static void OnCreated()
        {
            I._onPlayerCreated?.Invoke();
        }
        internal static void OnDestroyed()
        {
            I._onPlayerDestroyed?.Invoke();
        }

        #region Init
        private const string Group = "Player";
        private const string AddressableKey = "PlayerInitManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
