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
            GameObject prefab = Instantiate(_prefab, transform.position, transform.rotation);

            if (_useScale)
            {
                prefab.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
            }

            if (_spawnInside)
            {
                prefab.transform.SetParent(transform);
            }

            return prefab;
        
        }

        public void SpawnObject()
        {
            GetSpawnedObject();
        }
    }
}
