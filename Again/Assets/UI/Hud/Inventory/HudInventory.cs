using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using GameSystem.Screen;
using Creature.Player;

namespace UI.Hud.Inventory
{
    public class HudInventory : MonoBehaviour
    {
        #region Init
        private static bool loaded = false;

        private static GameObject instance;

        private const string AddressableKey = "Hud Inventory";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            PlayerInitializer.OnPlayerCreated += OnCreateHud;
            PlayerInitializer.OnPlayerDestroyed += OnDestroyHud;
        }
        private static void OnCreateHud()
        {
            if (loaded) return;

            loaded = true;

            Addressables.LoadAssetAsync<GameObject>(AddressableKey).Completed += asset =>
            {
                if (asset.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject gameObject = asset.Result;

                    instance = Instantiate(gameObject, ScreenManager.OverlayCanvas.transform);
                }
                else
                {
                    Debug.LogError($"Failed to load window prefab from {AddressableKey}.");
                }
            };
        }

        private static void OnDestroyHud()
        {
            if (instance == null) return;

            Destroy(instance);

            loaded = false;
    }
        #endregion
    }
}
