using GameSystem.Initialization;
using UnityEngine;

namespace Creature.Player.Ref
{
    [CreateAssetMenu(fileName = "PlayerRefManager", menuName = "Player/Refs/Manager")]
    public class PlayerRefManager : SystemOrigin<PlayerRefManager>
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private GameObject _playerGO;
        [SerializeField, EditorAttributes.EditorReadOnly] private Transform _spawnArea;
        [SerializeField, EditorAttributes.EditorReadOnly] private Animator _animator;

        public static GameObject Player
        { 
            get => I._playerGO; 
            internal set 
            { 
                I._playerGO = value; 
            } 
        }

        public static Transform SpawnArea
        {
            get => I._spawnArea;
            internal set
            {
                I._spawnArea = value;
            }
        }

        public static Animator Animator
        {
            get => I._animator;
            internal set
            {
                I._animator = value;
            }
        }

        #region Init
        private const string Group = "Player";
        private const string AddressableKey = "PlayerRefManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
