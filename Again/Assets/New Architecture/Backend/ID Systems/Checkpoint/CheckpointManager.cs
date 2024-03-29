using GameSystem.Initialization;
using SceneManagment;
using UnityEngine;

namespace SaveSystem.CheckpointSystem
{
    [CreateAssetMenu(fileName = "CheckpointManager", menuName = "SaveSystem/CheckpointManager")]
    public class CheckpointManager : SystemOrigin<CheckpointManager>
    {
        [SerializeField,EditorAttributes.EditorReadOnly] private string _checkpointID;

        public static string CheckpointID { get => I._checkpointID; internal set => I._checkpointID = value; }

        #region Init
        private const string Group = "Gameplay";
        private const string AddressableKey = "CheckpointManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);

            SystemInitializer.OnSystemsInitialized += () => SceneLoader.ListenStart(DropLoad, true);

            void DropLoad(SceneConfig conf, LoadingType type)
            {
                if (type != LoadingType.Room)
                    CheckpointID = string.Empty;
            }
        }
        #endregion
    }
}
