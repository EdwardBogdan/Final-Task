using Coroutines;
using GameSystem.Initialization;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    [CreateAssetMenu(menuName = "SceneManagment/SceneLoader", fileName = "SceneLoader")]
    public class SceneLoader : SystemOrigin<SceneLoader>
    {
        #region Editor
#if UNITY_EDITOR
        [SerializeField] private SceneConfig _editorDefaultLoadingScene;

        internal static SceneConfig EditorConfig => I._editorDefaultLoadingScene;
#endif
        #endregion

        [SerializeField, EditorAttributes.EditorReadOnly] private SceneConfig _config;

        public static SceneConfig Config { get => I._config; }

        private const string LoadingSceneName = "_Loading Scene";

        #region Controll
        public static void LoadScene(SceneConfig config) //For UnityEvent
        {
            LoadScene(config, LoadingType.Menu);
        }
        public static void LoadScene(SceneConfig config, LoadingType type)
        {
            CoroutineRunner.StartCoroutine(Loading(config));

            IEnumerator Loading(SceneConfig conf)
            {
                const float timeInitScene = 0.1f;
                const float minLoadingInterval = 0.5f;

                I._config = config;

                I._onBeforeStartLoad?.Invoke(config, type);

                SceneManager.LoadScene(LoadingSceneName);
                yield return new WaitForSeconds(timeInitScene);

                var async = SceneManager.LoadSceneAsync(conf.Name);

                async.allowSceneActivation = false;

                async.completed += op =>
                {
                    I._onAfterFinishLoad?.Invoke(conf, type);
                };

                yield return new WaitForSeconds(minLoadingInterval);

                async.allowSceneActivation = true;
            }
        }
        #endregion

        public void OnQuit()
        {
            _config = null;
        }

        #region Callback
        [SerializeField] private UnityEvent<SceneConfig, LoadingType> _onBeforeStartLoad;
        [SerializeField] private UnityEvent<SceneConfig, LoadingType> _onAfterFinishLoad;

        public static void ListenStart(UnityAction<SceneConfig, LoadingType> func, bool mode = true)
        {
            if (mode) I._onBeforeStartLoad.AddListener(func);
            else I._onBeforeStartLoad.RemoveListener(func);
        }
        public static void ListenFinish(UnityAction<SceneConfig, LoadingType> func, bool mode = true)
        {
            if (mode) I._onAfterFinishLoad.AddListener(func);
            else I._onAfterFinishLoad.RemoveListener(func);
        }
        #endregion

        #region Init
        private const string Group = "Backend";
        private const string AddressableKey = "SceneLoader";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);
        }
        #endregion
    }
}
