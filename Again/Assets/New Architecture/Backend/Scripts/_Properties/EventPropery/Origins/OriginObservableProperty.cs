using UnityEngine;
using UnityEngine.Events;

namespace Property.Origins
{
    public abstract class ObservableProperty<TPropertyType>
    {
        [SerializeField] private TPropertyType _value;

        [SerializeField, HideInInspector] private TPropertyType _stored;

        [SerializeField] public UnityEvent<TPropertyType, TPropertyType> OnChanged = new();


        public virtual TPropertyType Value
        {
            get => _value;
            set
            {
                if (_stored != null && _stored.Equals(value)) return;
                 
                var oldValue = _stored;
                _stored = _value = value;

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
