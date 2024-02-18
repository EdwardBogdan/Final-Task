using System;
using UnityEngine;
using UnityEngine.Events;

namespace GameSystem.Audio.Property
{
    [Serializable]
    public class AudioFloatProperty
    {
        private const float min = 0;
        private const float max = 1;

        [SerializeField, Range(min,max)] private float _value;

        private float _stored;

        [SerializeField, Space(10)] public UnityEvent<float, float> OnChanged = new();

        public virtual float Value
        {
            get => _value;
            set
            {
                float clampedValue = Mathf.Clamp(value, min, max);

                if (_stored.Equals(clampedValue)) return;

                var oldValue = _stored;
                _stored = _value = clampedValue;

                OnChanged?.Invoke(_value, oldValue);
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
