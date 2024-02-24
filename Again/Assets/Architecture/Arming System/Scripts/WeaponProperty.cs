using System;
using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.Arming
{
    [Serializable]
    public class WeaponProperty
    {
        [SerializeField] private bool _value;

        [SerializeField, HideInInspector] private bool _stored;

        [SerializeField] public UnityEvent<bool> OnChanged = new();

        [SerializeField] public UnityEvent OnArmed = new();
        [SerializeField] public UnityEvent OnDisarmed = new();

        public virtual bool Value
        {
            get => _value;
            set
            { 
                if (_stored.Equals(value)) return;

                var oldValue = _stored;
                _stored = _value = value;

                OnChanged?.Invoke(value);

                if (value) 
                    OnArmed?.Invoke();
                else 
                    OnDisarmed?.Invoke();
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
