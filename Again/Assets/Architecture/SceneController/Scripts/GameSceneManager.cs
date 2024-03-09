using Coroutines;
using GameSystem.General;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameSystem.SceneManagment
{
    [CreateAssetMenu(menuName = "SceneManagment/Manager", fileName = "SceneManager")]
    public class GameSceneManager : SystemOrigin<GameSceneManager>
    {
        private const string LoadingSceneName = "_Loading Scene";

        [SerializeField] private UnityEvent<SceneConfig> _onStartLoad;
        [SerializeField] private UnityEvent<SceneConfig> _onFinishLoad;

        public static SceneConfig Config { get; private set; }

        public static void LoadScene(SceneConfig config)
        {
            CoroutineRunner.StartCoroutine(Loading(config));

            IEnumerator Loading(SceneConfig conf)
            {
                const float minLoadingInterval = 0.5f;

                Config = config;

                I._onStartLoad?.Invoke(config);

                SceneManager.LoadScene(LoadingSceneName);

                yield return new WaitForSeconds(0.1f);

                var async = SceneManager.LoadSceneAsync(conf.Name);

                async.allowSceneActivation = false;

                async.completed += op =>
                {
                    I._onFinishLoad?.Invoke(conf);
                };

                yield return new WaitForSeconds(minLoadingInterval);

                async.allowSceneActivation = true;
            }
        }

        public static void ListenStart(UnityAction<SceneConfig> func, bool mode = true)
        {
            if (mode) I._onStartLoad.AddListener(func);
            else I._onStartLoad.RemoveListener(func);
        }
        public static void ListenFinish(UnityAction<SceneConfig> func, bool mode = true)
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
