using UnityEngine;

namespace SpawnSystem
{
    public static class Spawning
    {
        public static GameObject Spawn(GameObject prefab, Vector3 pos)
        {
            GameObject _prefab = Object.Instantiate(prefab, pos, default);

            return _prefab;
        }
        public static GameObject Spawn(GameObject prefab, Transform transform, bool useScale = false, bool spawnInside = false)
        {
            GameObject _prefab = Object.Instantiate(prefab, transform.position, transform.rotation);

            if (useScale)
            {
                _prefab.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
            }

            if (spawnInside)
            {
                _prefab.transform.SetParent(transform);
            }

            return _prefab;
        }

        public static T Spawn<T>(T prefab, Vector3 pos) where T : MonoBehaviour
        {
            GameObject _prefab = Object.Instantiate(prefab.gameObject, pos, default);

            return _prefab.GetComponent<T>();
        }
        public static T Spawn<T>(T prefab, Transform transform, bool useScale = false, bool spawnInside = false) where T : MonoBehaviour
        {
            GameObject _prefab = Object.Instantiate(prefab.gameObject, transform.position, transform.rotation);

            if (useScale)
            {
                _prefab.transform.localScale = Vector3.Scale(transform.lossyScale, _prefab.transform.localScale);
            }

            if (spawnInside)
            {
                _prefab.transform.SetParent(transform);
            }

            return _prefab.GetComponent<T>();
        }
    }
}
