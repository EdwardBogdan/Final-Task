using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Networking;

namespace Localization
{
    [CreateAssetMenu(menuName = "Data/Localization/Def", fileName = "LocaDef")]
    internal class LocaleDef : ScriptableObject
    {
        [SerializeField] private Language _language;
        [SerializeField] private Font _font;
        [SerializeField] private LocaleGroupData[] _base;

        internal Font GetFont => _font;
        internal string GetText(LocaGroup group, string key)
        {
            if (string.IsNullOrEmpty(key)) return "Key is Empty!";

            var set = _base.FirstOrDefault(item => item.Group == group);

            string text = set?.Locale.FirstOrDefault(item => item.Key == key)?.Value;

            if (string.IsNullOrEmpty(text)) return $"{_language}: {key}";

            return text;
        }

        #region Private
        [ContextMenu("Reload Localization Library")]
        private void UpdateLocale()
        {
            foreach (var item in _base)
            {
                item.UpdateLocaleLibrary();
            }
        }

        [Serializable]
        private class LocaleGroupData
        {
            [SerializeField] private LocaGroup _group;
            [SerializeField] private string _url;
            [SerializeField] private List<TextData> locale;

            internal LocaGroup Group => _group;
            internal List<TextData> Locale => locale;

            private UnityWebRequest _request;

            internal void UpdateLocaleLibrary()
            {
                if (_request == null)
                {
                    _request = UnityWebRequest.Get(_url);
                    _request.SendWebRequest().completed += OnDataLoaded;
                }

                void OnDataLoaded(AsyncOperation operation)
                {
                    if (operation.isDone)
                    {
                        locale.Clear();
                        _request.downloadHandler.text
                            .Split('\n')
                            .Select(row => row.Split('\t'))
                            .Where(parts => parts.Length == 2)
                            .ToList()
                            .ForEach(parts => locale.Add(new TextData(parts[0], parts[1])));

                        _request = null;
                    }
                }
            }
        }

        [Serializable]
        private class TextData
        {
            public string Key;
            public string Value;

            internal TextData(string key, string value)
            {
                Key = key;
                Value = value;
            }
        }
        #endregion
    }
}
