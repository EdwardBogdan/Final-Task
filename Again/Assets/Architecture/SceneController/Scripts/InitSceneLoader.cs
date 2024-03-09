using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace GameSystem.SceneManagment
{
    public class InitSceneLoader : MonoBehaviour
    {
        [SerializeField, SceneKey] private string _sceneToLoad;

        [SerializeField] private UnityEvent<string> OnSceneNameChanged;
        [SerializeField] private UnityEvent<float> OnPercentChanged;

        [SerializeField] private UnityEvent _onDone;

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
        public void OnLoadScene()
        {
#if UNITY_EDITOR
            _sceneToLoad = _editorSceneToLoad;
#endif
            AsyncOperation op = SceneManager.LoadSceneAsync(_sceneToLoad);

            StartCoroutine(Waiter(op));

            IEnumerator Waiter(AsyncOperation operation)
            {
                OnSceneNameChanged?.Invoke(_sceneToLoad);

                float percentOld = -1;

                while (!operation.isDone)
                {
                    float percentNew = op.progress;

                    if (!percentOld.Equals(percentNew))
                    {
                        percentOld = percentNew;
                        OnPercentChanged?.Invoke(percentOld);
                    }

                    yield return new WaitForFixedUpdate();
                }

                operation.allowSceneActivation = true;

                _onDone?.Invoke();
            }
        }
    }
}
