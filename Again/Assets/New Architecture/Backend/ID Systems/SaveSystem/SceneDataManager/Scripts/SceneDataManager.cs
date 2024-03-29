using GameSystem.Initialization;
using SceneManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveSystem.SceneDataManagment
{
    [CreateAssetMenu(fileName = "SceneDataManager", menuName = "SaveSystem/SceneDataManager")]
    public class SceneDataManager : SystemOrigin<SceneDataManager>
    {
        [SerializeField] private List<SceneData> _scenesData;
        
        internal static bool GetSceneData(string sceneName, out SceneData data)
        {
            data = I._scenesData.FirstOrDefault(sd => sd.SceneName == sceneName);

            bool isExist = true;

            if (data == null)
            {
                data = new SceneData(sceneName);
                I._scenesData.Add(data);
                isExist = false;
            }

            return isExist;
        }

        #region Save/Load
        public static event Action SaveEvent;
        public static event Action LoadEvent;

        public void SaveData()
        {
            SaveEvent?.Invoke();
        }

        public void LoadData()
        {
            LoadEvent?.Invoke();
        }

        #endregion

        #region Init
        private const string Group = "SaveManagment";
        private const string AddressableKey = "SceneDataManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);

            Application.quitting += Drop;

            SystemInitializer.OnSystemsInitialized += () => SceneLoader.ListenStart(DropLoad, true);

            void Drop()
            {
                I._scenesData.Clear();
            }
            void DropLoad(SceneConfig conf, LoadingType type)
            {
                if(type != LoadingType.Room)
                I._scenesData.Clear();
            }
        }
        #endregion
    }
}
