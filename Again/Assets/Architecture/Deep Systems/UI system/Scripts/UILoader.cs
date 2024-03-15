using DeepSystem.General;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace DeepSystem.Overlay
{
    [CreateAssetMenu(menuName = "UI/Loader", fileName = "UILoader")]
    public class UILoader : SystemOrigin<UILoader>
    {
        private static readonly List<string> _keyList = new List<string>();

        internal static void LoadUI<T>(string key, UnityAction<T> func) where T : UnitUI
        {
            if (CheckStatus(key))
            {
                Debug.LogError($"Attempt to load opened UI: {key}");
                return;
            }

            _keyList.Add(key);

            Addressables.LoadAssetAsync<GameObject>(key).Completed += handle =>
            {
                if (handle.Status == AsyncOperationStatus.Succeeded)
                {
                    GameObject asset = handle.Result;

                    T component = asset.GetComponent<T>();

                    if (component == null)
                    {
                        Debug.LogError($"Failed to load component {typeof(T)} from prefab by key: {key}.");
                        _keyList.Remove(key);
                        return;
                    }

                    T inst = Instantiate(component, ScreenManager.OverlayCanvas.transform);

                    inst._onUnitDead += () => { _keyList.Remove(key); };
                    func(inst);
                }
                else
                {
                    Debug.LogError($"Failed to load UI prefab by key: {key}.");
                    _keyList.Remove(key);
                }
            };
        }

        private static bool CheckStatus(string key)
        {
            return _keyList.Contains(key);
        }

        #region Init
        private const string AddressableKey = "UILoader";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion
    }
}
