using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    internal class DataHealth : AbstractData
    {
        [SerializeField] private int _data = 0;
        [SerializeField] private int _saved = 0;

        public DataHealth(string id) : base(id)
        {
        }

        internal int GetData() => _data;
        internal void SetData(int data)
        {
            _data = data;
        }
        internal void SetSave(int data)
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
