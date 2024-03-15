using DeepSystem.General;
using DeepSystem.Initialization;
using DeepSystem.SceneManagment;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace SaveSystem
{
    [CreateAssetMenu(fileName = "SaveManager", menuName = "SaveSystem/Manager")]
    public class SaveManager : SystemOrigin<SaveManager>
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

        #region SAVE LOAD
        [SerializeField] private UnityEvent _onSave;
        [SerializeField] private UnityEvent _onLoad;

        internal static event Action Save;
        internal static event Action Load;

        public static void SAVE()
        {
            I._onSave?.Invoke();
            Save?.Invoke();
        }

        public static void LOAD()
        {
            I._onLoad?.Invoke();
            Load?.Invoke();
        }
        #endregion


        #region Init
        private const string AddressableKey = "SaveManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);

            Application.quitting += Drop;

            SystemInitializer.OnSystemsInitialized += () => GameSceneManager.ListenStart(DropLoad, true);

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
