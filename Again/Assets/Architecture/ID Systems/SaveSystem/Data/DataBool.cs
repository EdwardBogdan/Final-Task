using System;
using UnityEngine;

namespace SaveSystem
{
    [Serializable]
    internal class DataBool : AbstractData
    {
        [SerializeField] private bool _data = true;
        [SerializeField] private bool _saved = true;

        public DataBool(string id) : base(id)
        {
        }

        internal bool GetData() => _data;
        internal void SetData(bool data)
        {
            _data = data;
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
