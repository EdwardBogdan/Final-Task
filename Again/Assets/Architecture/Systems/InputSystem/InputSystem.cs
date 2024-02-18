using GameSystem.General;
using Property.ObservableProperty;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameSystem.Input
{
    public class InputSystem : SingletonSystem<InputSystem>
    {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private StringObsProperty _defaultMap;

        internal static StringObsProperty DefaultMap => I._defaultMap;

        protected override void Awake()
        {
            base.Awake();

            _defaultMap.Value = _input.defaultActionMap;

            _defaultMap.OnChanged.AddListener(OnChangeMap);
        }
        private void OnDestroy()
        {
            _defaultMap.OnChanged.RemoveListener(OnChangeMap);
        }

        private void OnChangeMap(string mapName, string oldMapName)
        {
            _input.SwitchCurrentActionMap(mapName);
        }
    }
}
