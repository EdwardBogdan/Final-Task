using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace SaveSystem.SceneDataManagment
{
    [Serializable]
    internal class SceneData
    {
        [SerializeField, EditorAttributes.EditorReadOnly] private string _scene;

        internal string SceneName => _scene;

        internal SceneData(string sceneName)
        {
            _scene = sceneName;
        }

        #region Bool
        [SerializeField] private List<DataBool> _dataBool = new();
        internal bool GetBool(string id, out DataBool data)
        {
            data = _dataBool.FirstOrDefault(data => data.ID == id);

            bool isExist = true;

            if (data == null)
            {
                data = new DataBool(id);
                _dataBool.Add(data);
                isExist = false;
            }

            return isExist;
        }
        #endregion

        #region Vector3
        [SerializeField] private List<DataVector3> _dataVector3 = new();
        internal bool GetVector3(string id, out DataVector3 data)
        {
            data = _dataVector3.FirstOrDefault(data => data.ID == id);

            bool isExist = true;

            if (data == null)
            {
                data = new DataVector3(id);
                _dataVector3.Add(data);
                isExist = false;
            }

            return isExist;
        }
        #endregion

        #region Health
        [SerializeField] private List<DataHealth> _dataHealth = new();
        internal bool GetHealth(string id, out DataHealth data)
        {
            data = _dataHealth.FirstOrDefault(data => data.ID == id);

            bool isExist = true;

            if (data == null)
            {
                data = new DataHealth(id);
                _dataHealth.Add(data);
                isExist = false;
            }

            return isExist;
        }
        #endregion
    }
}
