using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem.Property
{
    [Serializable]
    internal class TextProperty
    {
        [SerializeField, TextArea(15,0), EditorAttributes.EditorReadOnly] private string _value;
        [SerializeField, Space(10)] internal UnityEvent<string> _onChanged;

        internal string Value
        {
            get => _value;
            set
            {
                _value = value;

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
