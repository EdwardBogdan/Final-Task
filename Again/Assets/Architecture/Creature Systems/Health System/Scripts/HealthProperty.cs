using System;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem.Property
{
    [Serializable]
    public class HealthProperty
    {
        private const int Min = 0;
        private const int MaxMin = Min + 1;

        [SerializeField, Min(Min)] private int _value = MaxMin;
        [SerializeField, HideInInspector] private int _stored;

        [SerializeField, Min(MaxMin)] private int _valueMax = MaxMin;
        [SerializeField, HideInInspector] private int _storedMax;

        public int Value
        {
            get => _value;
            internal set
            {
                int clampedValue = Mathf.Clamp(value, 0, _valueMax);

                if (_stored.Equals(clampedValue)) return;

                var oldValue = _stored;
                _stored = _value = clampedValue;

                _events.OnCountChanged?.Invoke(_value, oldValue);
            }
        }
        public int ValueMax
        {
            get => _valueMax;
            internal set
            {
                int clampedValue = value;

                if (value < MaxMin) clampedValue = MaxMin;

                if (_storedMax.Equals(clampedValue)) return;

                var oldValue = _storedMax;
                _storedMax = _valueMax = clampedValue;

                _events.OnMaxChanged?.Invoke(_valueMax, oldValue);

                if (_storedMax < _stored)
                {
                    Value = _storedMax;
                }
            }
        }

        internal void ApplyHeal(int value)
        {
            Value += value;

            _events.OnHeal?.Invoke();
        }
        internal void ApplyDamage(int damage)
        {
            Value -= damage;

            if (Value <= 0)
            {
                _events.OnDeath?.Invoke();
                return;
            }

            _events.OnHit?.Invoke();
        }

        #region Events
        [SerializeField] private HealthEvents _events;

        public void ListenCountChanged(UnityAction<int, int> func, bool mode)
        {
            if (mode) _events.OnCountChanged.AddListener(func);
            else _events.OnCountChanged.RemoveListener(func);
        }
        public void ListenMaxChanged(UnityAction<int, int> func, bool mode)
        {
            if (mode) _events.OnMaxChanged.AddListener(func);
            else _events.OnMaxChanged.RemoveListener(func);
        }
        public void ListenHit(UnityAction func, bool mode)
        {
            if (mode) _events.OnHit.AddListener(func);
            else _events.OnHit.RemoveListener(func);
        }
        public void ListenHeal(UnityAction func, bool mode)
        {
            if (mode) _events.OnHeal.AddListener(func);
            else _events.OnHeal.RemoveListener(func);
        }
        public void ListenDeath(UnityAction func, bool mode)
        {
            if (mode) _events.OnDeath.AddListener(func);
            else _events.OnDeath.RemoveListener(func);
        }

        #endregion

        #region Editor
#if UNITY_EDITOR
        internal void Validate()
        {
            if (_valueMax < MaxMin) _valueMax = MaxMin;
            ValueMax = _valueMax;

            _value = Mathf.Clamp(_value, 0, _valueMax);
            Value = _value;
        }
#endif
        #endregion
    }
}
