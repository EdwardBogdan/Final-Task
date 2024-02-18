using UnityEngine;

namespace DestroySystem.Components
{
    public class DestroyObjectComponent : MonoBehaviour
    {
        [SerializeField] Object[] _objects;

        public void DestroyObjects()
        {
            foreach (Object _object in _objects)
            {
                Destroy(_object);
            }
        }
    }
}