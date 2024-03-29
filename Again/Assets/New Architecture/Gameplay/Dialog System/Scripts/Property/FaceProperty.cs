using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem.Property
{
    [Serializable]
    internal class FaceProperty
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private Sprite _value;
        [SerializeField, HideInInspector] private Sprite _stored;
        [SerializeField] internal UnityEvent<Sprite> _onChanged;

        internal Sprite Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value)) return;

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