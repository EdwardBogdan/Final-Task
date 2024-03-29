using System;
using UnityEngine;
using UnityEngine.Events;

namespace TreasureSystem
{
    [Serializable]
    public class TreasureProperty
    {
        [SerializeField, Min(0)] private int _value;

        [SerializeField, HideInInspector] private int _stored;

        [SerializeField] public UnityEvent<int, int> OnCountChanged = new();

        public int Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value)) return;

                var oldValue = _stored;
                _stored = _value = value;

                OnCountChanged?.Invoke(_value, oldValue);
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
