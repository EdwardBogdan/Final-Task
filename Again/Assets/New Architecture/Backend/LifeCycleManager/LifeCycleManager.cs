using GameSystem.Initialization;
using UnityEngine;
using UnityEngine.Events;

namespace LifeCycleManagment
{
    [CreateAssetMenu(menuName = "Backend/LifeCycleManager", fileName = "LifeCycleManager")]
    public class LifeCycleManager : SystemOrigin<LifeCycleManager>
    {
        #region Pause
        [SerializeField] private UnityEvent<bool> _onPause;


        #endregion

        #region Quit
        [SerializeField] private UnityEvent _onQuit;

        public static void ListenApplicationQuit(UnityAction func, bool subscribe = true)
        {
            if (subscribe) I._onQuit.AddListener(func);
            else I._onQuit.RemoveListener(func);
        }

        private static void OnQuit()
        {
            I?._onQuit?.Invoke();

#if UNITY_EDITOR
            Debug.LogWarning("The application is quit");
#endif
        }
        #endregion

        #region Init
        private const string Group = "Backend";
        private const string AddressableKey = "LifeCycleManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(Group, AddressableKey);

            Application.quitting += OnQuit;
        }
        #endregion
    }
}
