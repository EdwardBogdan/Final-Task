using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem.Components
{
    public class SpawnManager : MonoBehaviour
    {
#if UNITY_EDITOR
        [SerializeField] private bool _debug = false;
#endif
        [SerializeField] private SpawnData[] _objects;

        private Dictionary<string, SpawnPoint> _spawnDictionary;

        private void Awake()
        {
            _spawnDictionary = new Dictionary<string, SpawnPoint>();
            foreach (var obj in _objects)
            {
                _spawnDictionary[obj._name] = obj._spawnUnit;
            }

#if !UNITY_EDITOR
            _objects = null;
#endif
        }
        public GameObject GetSpawnedTarget(string name)
        {
            if (_spawnDictionary.TryGetValue(name, out SpawnPoint spawnPoint))
            {
                if (spawnPoint.gameObject.activeSelf == true)
                {
                    return spawnPoint.GetSpawnedObject();
                }
#if UNITY_EDITOR
                if (_debug) Debug.Log($"{name} - not active");
#endif
                return null;
                
            }
#if UNITY_EDITOR
            if(_debug) Debug.Log($"{name} - not found");
#endif
            return null;
        }

        public void SpawnTarget(string name)
        {
            GetSpawnedTarget(name);
        }

        [Serializable]
        private class SpawnData
        {
            public string _name;
            public SpawnPoint _spawnUnit;
        }
    }
}
