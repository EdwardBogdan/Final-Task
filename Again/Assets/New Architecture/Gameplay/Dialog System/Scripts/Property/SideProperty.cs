using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem.Property
{
    [Serializable]
    internal class SideProperty
    {
        [SerializeField] private SideType _value;
        [SerializeField, HideInInspector] private SideType _stored;
        [SerializeField] internal UnityEvent<SideType> _onChanged;

        internal SideType Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value)) return;

                var oldValue = _stored;
                _stored = _value = value;

                _onChanged?.Invoke(value);
            }
        }

#if UNITY_EDITOR
        public void Validate()
        {
            Value = _value;
        }
#endif
    }
}
