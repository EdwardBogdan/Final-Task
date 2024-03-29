using System;
using UnityEngine;

namespace SaveSystem.SceneDataManagment
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

            SceneDataManager.SaveEvent += SaveData;
            SceneDataManager.LoadEvent += LoadData;
        }
        public void Dispose()
        {
            SceneDataManager.SaveEvent -= SaveData;
            SceneDataManager.LoadEvent -= LoadData;
        }
    }
}
