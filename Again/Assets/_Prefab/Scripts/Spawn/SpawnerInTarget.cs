using UnityEngine;

namespace SpawnSystem.Components
{
    public class SpawnerInTarget : MonoBehaviour
    {
        [SerializeField] GameObject _prefab;

        public GameObject GetSpawnedObject(GameObject target)
        {
            return Instantiate(_prefab, target.transform);

        }

        public void SpawnObject(GameObject target)
        {
            GetSpawnedObject(target);
        }
    }
}
