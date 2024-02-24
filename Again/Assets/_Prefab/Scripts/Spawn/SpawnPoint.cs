using UnityEngine;

namespace SpawnSystem.Components
{
    public class SpawnPoint : MonoBehaviour
    {
        [SerializeField] private bool _spawnInside = false;
        [SerializeField] private bool _useScale = true;

        [Space(3)]
        [SerializeField] private GameObject _prefab;

        public GameObject GetSpawnedObject()
        {
            return Spawning.Spawn(_prefab, transform, _useScale, _spawnInside);
        }

        public void SpawnObject()
        {
            GetSpawnedObject();
        }
    }
}
