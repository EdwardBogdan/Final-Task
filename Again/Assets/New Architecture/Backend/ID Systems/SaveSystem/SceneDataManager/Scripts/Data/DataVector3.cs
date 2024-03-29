using System;
using UnityEngine;

namespace SaveSystem.SceneDataManagment
{
    [Serializable]
    internal class DataVector3 : AbstractData
    {
        [SerializeField] private Vector3 _data = Vector3.zero;
        [SerializeField] private Vector3 _saved = Vector3.zero;

        public DataVector3(string id) : base(id)
        {
        }

        internal Vector3 GetData() => _data;
        internal void SetData(Vector3 data)
        {
            _data = data;
        }
        internal void SetSave(Vector3 data)
        {
            _saved = data;
        }

        protected override void SaveData()
        {
            _saved = _data;
        }
        protected override void LoadData()
        {
            _data = _saved;
        }
    }
}