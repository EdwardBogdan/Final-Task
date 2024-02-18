using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class ActionMasComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _action;

        public void InvokeActionMas()
        {
            _action?.Invoke();
        }
    }
}
