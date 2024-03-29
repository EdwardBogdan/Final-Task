using System;
using UnityEngine;
using UnityEngine.Events;

namespace Overlay.UIManagment.HudManagment
{
    [Serializable]
    public class HudMode
    {
        [SerializeField] private bool _show = true;

        private bool _stored = true;

        [SerializeField, Space(10)] public UnityEvent<bool> OnChanged = new();

        public virtual bool Value
        {
            get => _show;
            set
            {
                if (_stored.Equals(value)) return;

                _stored = _show = value;

                OnChanged?.Invoke(_stored);
            }
        }

#if UNITY_EDITOR
        public void Validate()
        {
            Value = _show;
        }
#endif
    }
}