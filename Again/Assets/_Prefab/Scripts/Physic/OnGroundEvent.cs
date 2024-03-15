using System;
using UnityEngine;
using UnityEngine.Events;

namespace PhysicCustom.Components
{
    public class OnGroundEvent : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private GroundEvent[] _events;

        private float SavedVelocity = 0;

        public void OnFly()
        {
            enabled = true;
        }
        public void OnGround()
        {
            enabled = false;

            foreach (var item in _events)
            {
                if (-item.YVelocity > SavedVelocity)
                {
                    item.Invoke();
                }
            }
        }


        private void FixedUpdate()
        {
            SavedVelocity = _body.velocity.y;
        }

        [Serializable]
        private class GroundEvent
        {
            [SerializeField, Min(0)] private float _yVelocity;
            [SerializeField] private UnityEvent _event;

            internal float YVelocity => _yVelocity;
            internal void Invoke() => _event?.Invoke();
        }
    }
}
