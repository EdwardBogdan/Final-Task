using GameSystem.Screen;
using UI.Windows.Controller;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;

namespace UI.Windows.SceneTransition
{
    public class SceneTransitionController : MonoBehaviour
    {
        [SerializeField] private string _windowName = "Window Scene Transition";

        [SerializeField] private string _sceneName;
        [SerializeField] private UnityEvent _beforeChangeScene;

        [SerializeField] private Color _panel1;
        [SerializeField] private Color _panel2;
        [SerializeField] private Color _text;

        private static AsyncOperation loadOperation;

        public void TransitionScene()
        {
            var op = OpenWindowController.OpenWindow(_windowName);

            op.Completed += OnWindowLoaded;

            WindowSceneTransition window = null;

            void OnWindowLoaded(AsyncOperationHandle<GameObject> operation)
            {
                // Проверяем статус загрузки
                if (operation.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject prefab = operation.Result;
                    GameObject instance = Instantiate(prefab, ScreenManager.OverlayCanvas.transform);
                    window = instance.GetComponent<WindowSceneTransition>();

                    window.ChangeColors(_panel1, _panel2, _text);
                    
                    window.EventShowStart.AddListener(OnLoadScene);

                    window.EventShowFinish.AddListener(OnBeforeChangeScene);

                    window.EventShowFinish.AddListener(OnFinishAnimation);
                }
                else
                {
                    Debug.LogError($"Failed to load prefab {_windowName}");
                }
            }
        
            void OnLoadScene()
            {
                loadOperation = SceneManager.LoadSceneAsync(_sceneName);
                loadOperation.allowSceneActivation = false;
        
                window.EventShowStart.RemoveListener(OnLoadScene);
            }

            void OnBeforeChangeScene()
            {
                _beforeChangeScene?.Invoke();

                window.EventShowFinish.RemoveListener(OnBeforeChangeScene);
            }
        
            void OnFinishAnimation()
            {
                AsyncOperation operation = loadOperation;
        
                operation.allowSceneActivation = true;
        
                window.WaitOperation(operation);
        
                window.EventShowFinish.RemoveListener(OnFinishAnimation);
            }
        }
    }
}
