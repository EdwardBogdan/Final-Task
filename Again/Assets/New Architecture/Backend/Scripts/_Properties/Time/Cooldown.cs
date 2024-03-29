using System;
using UnityEngine;

namespace Property.TimeProperty
{
    [Serializable]
    public class Cooldown
    {
        [SerializeField, Min(0.01f)] private float _duration;

        private float _timesUp = 0;

        public bool IsReady => _timesUp <= Time.time;
        public float Duration => _duration;
        public float Stage => (Time.time - (_timesUp - _duration)) / _duration;
        public void Reset()
        {
            _timesUp = Time.time + _duration;
        }
        public void Ready()
        {
            _timesUp = Time.time;
        }

        public Cooldown(float duration)
        {
            _duration = duration;
        }
    }
}
