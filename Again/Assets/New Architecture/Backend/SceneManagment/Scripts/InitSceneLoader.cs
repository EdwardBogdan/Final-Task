using UnityEngine;
using UnityEngine.SceneManagement;

namespace SceneManagment
{
    public class InitSceneLoader : MonoBehaviour
    {
        [SerializeField] private SceneConfig _defaultLoadingScene;

        private const string InitSceneName = "_Open Scene";

#if UNITY_EDITOR
        private static string _editorSceneToLoad = "_Main Menu";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializationSystem()
        {
            Scene activeScene = SceneManager.GetActiveScene();

            string sceneName = activeScene.name;

            if (sceneName != InitSceneName)
            {
                _editorSceneToLoad = sceneName;

                GameObject[] rootObjects = FindObjectsOfType<GameObject>();

                foreach (GameObject obj in rootObjects)
                {
                    obj.SetActive(false);
                }
            }

            SceneManager.LoadScene(InitSceneName);
        }
#endif

        public void Load()
        {
#if UNITY_EDITOR
            SceneLoader.LoadScene(SceneLoader.EditorConfig, LoadingType.Level);
#else
            SceneLoader.LoadScene(_defaultLoadingScene, LoadingType.Menu);
#endif
        }
    }
}
