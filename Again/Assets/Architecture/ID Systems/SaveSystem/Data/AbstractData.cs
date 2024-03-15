using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    internal abstract class AbstractData : IDisposable
    {
        [SerializeField,EditorAttributes.EditorReadOnly] private string _id;

        public string ID => _id;

        protected abstract void SaveData();
        protected abstract void LoadData();

        public AbstractData(string id)
        {
            _id = id;

            SaveManager.Save += SaveData;
            SaveManager.Load += LoadData;
        }
        public void Dispose()
        {
            SaveManager.Save -= SaveData;
            SaveManager.Load -= LoadData;
        }
    }
}
