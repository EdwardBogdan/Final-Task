using UnityEngine;
using UnityEngine.Events;

namespace SimpleComponents
{
    public class RepeatEventComponent : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _delay;
        [SerializeField] UnityEvent _action;

        private float timer;

        public void TimerReady()
        {
            timer = Time.time;
        }
        public void DropTimer()
        {
            timer = Time.time + _delay;
        }
        void Start()
        {
            DropTimer();
        }
        private void FixedUpdate()
        {
            if (timer <= Time.time)
            {
                _action?.Invoke();
                DropTimer();
            }
        }
    }
}
