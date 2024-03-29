using System;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem.Properties
{
    [Serializable]
    public class ItemCountProperty
    {
        private const int ItemMinLimit = 1;
        private const int ItemMaxLimit = 10;

        [SerializeField, Min(0)] private int _value;
        [SerializeField, Range(ItemMinLimit, ItemMaxLimit)] private int _maxValue;

        [SerializeField, HideInInspector] private int _stored;
        [SerializeField, HideInInspector] private int _storedMax;

        [SerializeField] public UnityEvent<int, int> OnCountChanged = new();
        [SerializeField] public UnityEvent<int, int> OnMaxChanged = new();

        public int Value
        {
            get => _value;
            set
            {
                int clampedValue = Mathf.Clamp(value, 0, _maxValue);

                if (_stored.Equals(clampedValue)) return;

                var oldValue = _stored;
                _stored = _value = clampedValue;

                OnCountChanged?.Invoke(_value, oldValue);
            }
        }
        public int ValueMax
        {
            get => _maxValue;
            set
            {
                int clampedValue = Mathf.Clamp(value, ItemMinLimit, ItemMaxLimit);

                if (_storedMax.Equals(clampedValue)) return;

                var oldValue = _storedMax;
                _storedMax = _maxValue = clampedValue;

                OnMaxChanged?.Invoke(_maxValue, oldValue);

                if (_storedMax < _stored)
                {
                    Value = _storedMax;
                }
            }
        }

#if UNITY_EDITOR
        public void Validate()
        {
            _maxValue = Mathf.Clamp(_maxValue, ItemMinLimit, ItemMaxLimit);
            ValueMax = _maxValue;

            _value = Mathf.Clamp(_value, 0, _maxValue);
            Value = _value;
        }
#endif
    }
}