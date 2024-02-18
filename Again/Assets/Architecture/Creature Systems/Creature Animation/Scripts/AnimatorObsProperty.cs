using System;
using UnityEngine.Events;
using UnityEngine;

namespace PlayerSystems.AnimatorSystem
{
    [Serializable]
    public class AnimatorObsProperty
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private AnimatorType _value;
        [SerializeField, HideInInspector] private AnimatorType _stored;
        [SerializeField] public UnityEvent<AnimatorType> OnChanged = new();


        public virtual AnimatorType Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value)) return;

                _stored = _value = value;

                OnChanged?.Invoke(_value);
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