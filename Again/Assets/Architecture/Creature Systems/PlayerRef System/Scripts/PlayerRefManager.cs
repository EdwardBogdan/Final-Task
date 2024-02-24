using GameSystem.General;
using UnityEngine;

namespace Creature.Player.Ref
{
    [CreateAssetMenu(fileName = "PlayerRefManager", menuName = "Player/Refs/Manager")]
    public class PlayerRefManager : SystemOrigin<PlayerRefManager>
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private GameObject _playerGO;
        [SerializeField, EditorAttributes.EditorReadOnly] private Transform _spawnArea;

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

        #region Init
        private const string AddressableKey = "PlayerRefManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
