using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI.Windows.Controller
{
    public class OpenWindowController : MonoBehaviour
    {
        public void OnOpenWindow(string windowName)
        {
            OpenWindow(windowName);
        }

        public static AsyncOperationHandle<GameObject> OpenWindow(string windowName)
        {
            AsyncOperationHandle<GameObject> gameObjectOperation = Addressables.LoadAssetAsync<GameObject>(windowName);

            gameObjectOperation.Completed += handle =>
            {
                if (handle.Status != AsyncOperationStatus.Succeeded)
                {
                    Debug.LogError($"Asset {windowName} not found.");
                }
            };

            return gameObjectOperation;
        }
    }
}
