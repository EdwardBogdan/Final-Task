using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpawnSystem.Components
{
    public class SpawnManager : MonoBehaviour
    {
        [SerializeField] SpawnData[] _objects;

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
                return spawnPoint.GetSpawnedObject();
            }

            Debug.Log($"{name} - not found");
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
