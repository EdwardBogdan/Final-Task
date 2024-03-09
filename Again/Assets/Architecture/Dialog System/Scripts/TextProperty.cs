using System;
using UnityEngine;
using UnityEngine.Events;

namespace DialogSystem.Text
{
    [Serializable]
    internal class TextProperty
    {
        [SerializeField] private string _value;
        [SerializeField] internal UnityEvent<string> _onChanged;

        internal string Value
        {
            get => _value;
            set
            {
                _value = value;

                _onChanged?.Invoke(value);
            }
        }
    }
}
