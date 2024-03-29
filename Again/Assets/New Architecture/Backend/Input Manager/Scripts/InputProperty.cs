using System;
using UnityEngine;
using UnityEngine.Events;

namespace InputControll
{
    [Serializable]
    public class InputProperty
    {
        [SerializeField] private InputMap _currentMap;
        [SerializeField, HideInInspector] private InputMap _storedMap;
        [SerializeField] private UnityEvent<InputMap, InputMap> _onMapChanged = new();
        internal InputMap Map
        {
            get => _currentMap;
            set
            {
                if (value.Equals(_storedMap)) return;

                var oldMap = _currentMap;

                _currentMap = _storedMap = value;
                _onMapChanged?.Invoke(value, oldMap);
            }
        }

        internal void ListenChangeMap(UnityAction<InputMap, InputMap> action, bool subscribe)
        {
            if (subscribe) _onMapChanged.AddListener(action);
            else _onMapChanged.RemoveListener(action);
        }

#if UNITY_EDITOR
        internal void Validate()
        {
            Map = _currentMap;
        }
#endif
    }
}
