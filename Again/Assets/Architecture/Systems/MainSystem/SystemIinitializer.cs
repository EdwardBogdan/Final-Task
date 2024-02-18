using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using System.Collections;

namespace GameCore.Init
{
    public class SystemIinitializer : MonoBehaviour
    {
        [SerializeField] private UnityEvent _afterInit;

        private static string _sceneToLoad = "_Main Menu";

        private const string InitSceneName = "_Open Scene";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.SubsystemRegistration)]
        private static void InitializationSystem()
        {
#if UNITY_EDITOR
            Scene activeScene = SceneManager.GetActiveScene();

            string sceneName = activeScene.name;

            if (sceneName != InitSceneName)
            {
                _sceneToLoad = sceneName;

                GameObject[] rootObjects = FindObjectsOfType<GameObject>();

                foreach (GameObject obj in rootObjects)
                {
                    obj.SetActive(false);
                }
            } 
#endif
            SceneManager.LoadScene(InitSceneName);
        }

        private void Start()
        {
            AsyncOperation op = SceneManager.LoadSceneAsync(_sceneToLoad);
            op.allowSceneActivation = false;
            
            StartCoroutine(Waiter(op));

            IEnumerator Waiter(AsyncOperation operation)
            {
                const float timeStep = 0.5f;

                yield return new WaitForSeconds(timeStep);

                operation.allowSceneActivation = true;

                while (!operation.isDone)
                {
                    yield return new WaitForSeconds(timeStep);
                }

                _afterInit?.Invoke();
                
                Destroy(this);
            }
        }
    }
}
