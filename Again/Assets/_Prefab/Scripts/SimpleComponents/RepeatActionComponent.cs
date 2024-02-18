using UnityEngine;
using UnityEngine.Events;
using Property.TimeProperty;

namespace SimpleComponents
{
    public class RepeatActionComponent : MonoBehaviour
    {
        [SerializeField] Cooldown _delay;
        [SerializeField] UnityEvent _action;

        private void Start()
        {
            _delay.Reset();
        }
        private void FixedUpdate()
        {
            if (_delay.IsReady)
            {
                _action?.Invoke();
                _delay.Reset();
            }
        }
    }
}