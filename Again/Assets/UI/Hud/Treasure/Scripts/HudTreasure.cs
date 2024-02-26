using Creature.Player.PlayerInit;
using GameSystem.Screen;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace UI.Hud.Treasure
{
    public class HudTreasure : MonoBehaviour
    {
        #region Init
        private static bool loaded = false;

        private static GameObject instance;

        private const string AddressableKey = "Hud Treasure";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Initialize()
        {
            PlayerInitManager.OnPlayerCreated += OnCreateHud;
            PlayerInitManager.OnPlayerDestroyed += OnDestroyHud;
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
                    Debug.LogError($"Failed to load window prefab by key: {AddressableKey}.");
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
