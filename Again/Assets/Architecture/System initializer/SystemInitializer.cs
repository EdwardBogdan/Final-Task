using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Events;
using System;

namespace GameSystem.Initialization
{
    public class SystemInitializer : MonoBehaviour
    {
        [SerializeField] private UnityEvent OnDone;

        private static readonly List<InitUnit> queue = new();

        public static void InitNewSystem(string systemKey, Action<ScriptableObject> onCompleted)
        {
            if (queue.Count == 0)
            {
                overallProgress = 0; // Сброс общего прогресса при начале новой серии загрузок
            }

            InitUnit unit = new(systemKey, onCompleted);
            queue.Add(unit);
        }

        private void Start()
        {
            totalSystemsCount = queue.Count;

            // Рассчитаем вес одной системы в общем прогрессе
            singleSystemWeight = 100f / totalSystemsCount;

            InitNextSystem();
        }

        private void InitNextSystem()
        {
            if (queue.Count > 0)
            {
                InitUnit unit = queue[0];
                AsyncOperationHandle<ScriptableObject> handle = Addressables.LoadAssetAsync<ScriptableObject>(unit.SystemName);
                OnSystemNameChanged?.Invoke(unit.SystemName);

                handle.Completed += op =>
                {
                    if (op.Status == AsyncOperationStatus.Succeeded)
                    {
                        ScriptableObject result = op.Result;
                        unit.OnCompleted?.Invoke(result);
                        queue.Remove(unit);
                        InitNextSystem();

                        CallBack(unit);
                    }
                    else
                    {
                    #if UNITY_EDITOR
                        Debug.LogError($"Failed to init system: {unit.SystemName}");
                        UnityEditor.EditorApplication.isPlaying = false;
                    #else
                        Application.Quit();
                    #endif
                    }
                };
            }
            else
            {
                OnDone?.Invoke();
            }
        }

        #region CallBack
        [SerializeField] private UnityEvent<string> OnSystemNameChanged;
        [SerializeField] private UnityEvent<float> OnPercentChanged;

        private static int totalSystemsCount;
        private static float overallProgress = 0;
        private static float singleSystemWeight;

        private void CallBack(InitUnit unit)
        {
            overallProgress += singleSystemWeight;

            OnPercentChanged?.Invoke(overallProgress);

            Debug.Log($"{unit.SystemName}: Successful! Init progress: {overallProgress}%");
        }
        #endregion
    }
}
