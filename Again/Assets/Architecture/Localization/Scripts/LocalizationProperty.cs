using System;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Localization
{
    [Serializable]
    internal class LocalizationProperty
    {
        [SerializeField] private Language _value;
        [SerializeField, HideInInspector] private Language _stored;

        [SerializeField, EditorAttributes.EditorReadOnly] private LocaleDef _localeDef;

        [SerializeField] internal UnityEvent<LocaleDef> OnChanged = new();

        

        internal LocaleDef Def => _localeDef;
        internal Language Value
        {
            get => _value;
            set
            {
                if (_stored.Equals(value)) return;

                _stored = _value = value;

                ChangeDef(_value);

                OnChanged?.Invoke(_localeDef);
            }
        }

        #region Load Def
        private AsyncOperationHandle<LocaleDef> loading;

        private void ChangeDef(Language localeKey)
        {
            if (loading.IsValid())
            {
                Addressables.Release(loading);
                loading = default;
            }

            string key = $"{localeKey}".First().ToString().ToUpper() + $"{localeKey}".Substring(1);
            loading = Addressables.LoadAssetAsync<LocaleDef>(key);

            loading.Completed += handle =>
            {
                _localeDef = handle.Result;
                loading = default;
            };
        }
        #endregion

#if UNITY_EDITOR
        public void Validate()
        {
            Value = _value;
        }
#endif
    }
}
