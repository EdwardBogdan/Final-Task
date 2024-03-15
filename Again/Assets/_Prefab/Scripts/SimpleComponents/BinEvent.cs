using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class BinEvent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void InvokeActionMas()
        {
            _action?.Invoke();
        }
    }
}
