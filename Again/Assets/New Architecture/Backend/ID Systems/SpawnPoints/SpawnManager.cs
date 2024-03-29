using GameSystem.Initialization;
using UnityEngine;

namespace SpawnSystem
{
    [CreateAssetMenu(fileName = "SpawnManager", menuName = "SpawnSystem/Manager")]
    public class SpawnManager : SystemOrigin<SpawnManager>
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private string _spawnID;
        [SerializeField, EditorAttributes.EditorReadOnly] private string _savedSpawnID;
        internal static string SpawnID { get => I._spawnID; }

        public static void ChangeSpawnPoint(string id)
        {
            I._spawnID = id;
        }

        #region Init
        private const string Group = "Player";
        private const string AddressableKey = "SpawnManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);

            //SaveManager.Save += Save;
            //SaveManager.Load += Load;

            //SystemInitializer.OnSystemsInitialized += () => SceneLoader.ListenStart(DropLoad, true);
            //
            //void Save()
            //{
            //    I._savedSpawnID = I._spawnID;
            //}
            //void Load()
            //{
            //    I._spawnID = I._savedSpawnID;
            //}
            //void DropLoad(SceneConfig conf, LoadingType type)
            //{
            //    if (type != LoadingType.Room)
            //        I._spawnID = I._savedSpawnID = string.Empty;
            //}
        }
        #endregion
    }
}
