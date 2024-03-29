using UnityEngine;

namespace SystemID
{
    public abstract class IDUser : MonoBehaviour
    {
        [SerializeField] private IDComponent _id;

        public string ID => gameObject.name + ": " + _id.ID;
    }
}
