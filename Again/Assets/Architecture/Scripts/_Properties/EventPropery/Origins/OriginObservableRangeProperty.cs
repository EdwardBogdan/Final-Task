using System;
using UnityEngine;
using UnityEngine.Events;

namespace Property.Origins
{
    public abstract class OriginObservableRangeProperty<TPropertyType> where TPropertyType : struct, IComparable, IComparable<TPropertyType>, IEquatable<TPropertyType>, IFormattable
    {
        [SerializeField] private TPropertyType _value;
        [SerializeField] private Range _range;

        [SerializeField, HideInInspector] protected TPropertyType _stored;

        [SerializeField] public UnityEvent<TPropertyType, TPropertyType> OnChanged = new();

        public virtual TPropertyType Value
        {
            get => _value;
            set
            {
                TPropertyType clampedValue = ClampValue(value, _range._min, _range._max);

                if (_stored.Equals(clampedValue)) return;

                var oldValue = _stored;
                _stored = _value = clampedValue;

                OnChanged?.Invoke(_value, oldValue);
            }
        }

        public TPropertyType MinRange
        {
            get => _range._min;
            set => _range._min = value;
        }
        public TPropertyType MaxRange
        {
            get => _range._max;
            set
            {
                _range._max = value;
                Value = Value;
            } 
        }

        protected abstract TPropertyType ClampValue(TPropertyType value, TPropertyType min, TPropertyType max);

        [Serializable]
        private class Range
        {
            [SerializeField, Min(0)] internal TPropertyType _min;
            [SerializeField] internal TPropertyType _max;
        }

#if UNITY_EDITOR
        public void Validate()
        {
            _value = ClampValue(_value, _range._min, _range._max);

            Value = _value;
        }
#endif
    }
}

