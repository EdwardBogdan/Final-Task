using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CreatureAnimation
{
    public class AnimatorEventManager : MonoBehaviour
    {
        #region SIMPLE Event
        [SerializeField] private UnityEventRef[] _eventRefs;

        private Dictionary<string, UnityEvent> events;

        public void InvokeEvent(string key)
        {
            GetEvent(key).Invoke();
        }
        public UnityEvent GetEvent(string key)
        {
            return events[key];
        }

        [Serializable]
        private class UnityEventRef
        {
            public string _key;
            public UnityEvent _event;
        }
        #endregion

        #region STRING Event
        [SerializeField] private UnityEventStrRef[] _eventStringRefs;

        private Dictionary<string, UnityEvent<string>> stringEvents;

        public void InvokeStringEvent(string key)
        {
            string[] keys = key.Split(", ");

            GetStringEvent(keys[0]).Invoke(keys[1]);
        }

        public UnityEvent<string> GetStringEvent(string key)
        {
            return stringEvents[key];
        }

        [Serializable]
        private class UnityEventStrRef
        {
            public string _key;
            public UnityEvent<string> _event;
        }
        #endregion

        private void Awake()
        {
            events = new Dictionary<string, UnityEvent>(_eventRefs.Length);

            foreach (var item in _eventRefs)
            {
                events[item._key] = item._event;
            }

#if !UNITY_EDITOR
        _eventRefs = null;
#endif
            stringEvents = new Dictionary<string, UnityEvent<string>>(_eventStringRefs.Length);

            foreach (var item in _eventStringRefs)
            {
                stringEvents[item._key] = item._event;
            }

#if !UNITY_EDITOR
        _eventStringRefs = null;
#endif
        }
    }
}
