using Coroutines;
using DeepSystem.General;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace DeepSystem.SceneManagment
{
    [CreateAssetMenu(menuName = "SceneManagment/Manager", fileName = "SceneManager")]
    public class GameSceneManager : SystemOrigin<GameSceneManager>
    {
        private const string LoadingSceneName = "_Loading Scene";

        [SerializeField] private UnityEvent<SceneConfig, LoadingType> _beforeStartLoad;
        [SerializeField] private UnityEvent<SceneConfig, LoadingType> _onFinishLoad;

        public static SceneConfig Config { get; private set; }

        public static void LoadScene(SceneConfig config, LoadingType type)
        {
            CoroutineRunner.StartCoroutine(Loading(config));

            IEnumerator Loading(SceneConfig conf)
            {
                const float minLoadingInterval = 0.5f;

                Config = config;

                I._beforeStartLoad?.Invoke(config, type);

                SceneManager.LoadScene(LoadingSceneName);

                yield return new WaitForSeconds(0.1f);

                var async = SceneManager.LoadSceneAsync(conf.Name);

                async.allowSceneActivation = false;

                async.completed += op =>
                {
                    I._onFinishLoad?.Invoke(conf, type);
                };

                yield return new WaitForSeconds(minLoadingInterval);

                async.allowSceneActivation = true;
            }
        }

        public static void ListenStart(UnityAction<SceneConfig, LoadingType> func, bool mode = true)
        {
            if (mode) I._beforeStartLoad.AddListener(func);
            else I._beforeStartLoad.RemoveListener(func);
        }
        public static void ListenFinish(UnityAction<SceneConfig, LoadingType> func, bool mode = true)
        {
            if (mode) I._onFinishLoad.AddListener(func);
            else I._onFinishLoad.RemoveListener(func);
        }

        #region Init
        private const string AddressableKey = "Scene Manager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
