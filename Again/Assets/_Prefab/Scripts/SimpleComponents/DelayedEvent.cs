using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class DelayedEvent : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _delay;
        [SerializeField] private UnityEvent _event;

        private float timer;
        void Start()
        {
            timer = Time.time + _delay;
        }
        private void FixedUpdate()
        {
            if (timer <= Time.time)
            {
                _event?.Invoke();
                Destroy(this);
            }
        }
    }
}
