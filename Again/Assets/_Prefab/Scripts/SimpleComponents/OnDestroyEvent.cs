using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class OnDestroyEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _event;

        private void OnDestroy()
        {
            _event?.Invoke();
        }
    }
}
