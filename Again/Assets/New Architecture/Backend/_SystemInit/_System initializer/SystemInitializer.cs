using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using System.Collections.Generic;
using UnityEngine.Events;
using System;
using System.Linq;

namespace GameSystem.Initialization
{
    public class SystemInitializer : MonoBehaviour
    {
        private static readonly List<InitGroup> queue = new();

        private static InitGroup currentGroup;
        private static Transform currentGroupTransform;

        public static void AddToQueueSystem(string groupName, string systemKey, Action<ScriptableObject, Transform> onCompleted)
        {
            var group = queue.Find(g => g.groupName == groupName) ?? AddNewGroup(groupName);
            group.AddUnit(new InitUnit(systemKey, onCompleted));

            static InitGroup AddNewGroup(string groupName)
            {
                var newGroup = new InitGroup(groupName);
                queue.Add(newGroup);
                return newGroup;
            }
        }
        private void InitNextGroup()
        {
            if (queue.Count > 0)
            {
                currentGroup = queue[0];
                queue.RemoveAt(0);

                currentGroupTransform = CreateGroup(currentGroup.groupName);
                OnStartGroupInitCallback(currentGroup);

                InitNextSystem();
            }
            else
            {
                OnDoneCallback();
            }

            static Transform CreateGroup(string groupName)
            {
                var group = new GameObject($"[ ] {groupName}");
                DontDestroyOnLoad(group);
                return group.transform;
            }
        }

        

        private void InitNextSystem()
        {
            if(currentGroup.GetNextUnit(out InitUnit unit))
            {
                LoadSystem(unit);
            }
            else
            {
                InitNextGroup();
            }
        }

        private void LoadSystem(InitUnit unit)
        {
            var handle = Addressables.LoadAssetAsync<ScriptableObject>(unit.SystemName);
            _onSystemNameChanged?.Invoke(unit.SystemName);

            handle.Completed += op =>
            {
                if (op.Status == AsyncOperationStatus.Succeeded)
                {
                    unit.OnCompleted?.Invoke(op.Result, currentGroupTransform);
                    UpdateCallback(unit);
                    InitNextSystem();
                }
                else
                {
                    HandleInitError(unit);
                }
            };
        }

        #region Mono
        private void Start()
        {
            CalculateTotalSystemsCount();
            InitNextGroup();
        }
        #endregion

        #region Callback
        [SerializeField] private UnityEvent _onDone;

        [SerializeField] private UnityEvent<string> _onGroupNameChanged;
        [SerializeField] private UnityEvent<string> _onSystemNameChanged;
        [SerializeField] private UnityEvent<float> _onPercentChanged;
        

        private static int totalSystemsCount;
        private static float overallProgress;
        private static float singleSystemWeight;

        public static event Action OnSystemsInitialized;

        private void OnStartGroupInitCallback(InitGroup unit)
        {
            _onGroupNameChanged?.Invoke(unit.groupName);
#if UNITY_EDITOR
            const string line = "- - - - - -";
            Debug.Log(line + unit.groupName + line);
#endif
        }
        private void UpdateCallback(InitUnit unit)
        {
            overallProgress += singleSystemWeight;
            int progress = Mathf.RoundToInt(overallProgress);

            _onPercentChanged?.Invoke(Mathf.RoundToInt(progress));
#if UNITY_EDITOR
            Debug.Log($"{unit.SystemName}: Successful! Init progress: {progress}%");
#endif
        }
        private void HandleInitError(InitUnit unit)
        {
#if UNITY_EDITOR
            Debug.LogError($"Failed to init system: {unit.SystemName}");
            UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        }
        private void OnDoneCallback()
        {
            OnSystemsInitialized?.Invoke();
            _onDone?.Invoke();
            OnSystemsInitialized = null;
            Destroy(this);
        }
        private void CalculateTotalSystemsCount()
        {
            totalSystemsCount = queue.Sum(group => group.Count);
            singleSystemWeight = 100f / totalSystemsCount;
        }

        #endregion
    }
}
