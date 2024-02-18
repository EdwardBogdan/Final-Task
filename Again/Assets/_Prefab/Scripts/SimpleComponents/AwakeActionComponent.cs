using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class AwakeActionComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        private void Awake()
        {
            _action?.Invoke();
        }
    }
}
