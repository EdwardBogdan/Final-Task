using GameSystem.General;
using UnityEngine;
using UnityEngine.Events;

namespace InputControll
{
    [CreateAssetMenu(fileName = "InputManager", menuName = "Player/Input/Manager")]
    public class InputManager : SystemOrigin<InputManager>
    {
        #region Keys
        [SerializeField] private InputMapKeys _keys;
        internal static InputMapKeys Keys => I._keys;
        #endregion

        [SerializeField] internal InputProperty _inputMap;

        internal static InputMap Map
        {
            get => I._inputMap.Map;
            set => I._inputMap.Map = value;
        }
        internal static void ListenChangeMap(UnityAction<InputMap, InputMap> action, bool subscribe)
        {
            I._inputMap.ListenChangeMap(action, subscribe);
        }

        #region Init
        private const string AddressableKey = "InputManager";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterAssembliesLoaded)]
        private static void Init()
        {
            InitializeSystem(AddressableKey);
        }
        #endregion

        #region Mono
#if UNITY_EDITOR
        private void OnValidate()
        {
            _inputMap.Validate();
        }
#endif
        #endregion
    }
}
