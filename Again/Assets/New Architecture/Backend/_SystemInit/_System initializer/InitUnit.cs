using System;
using UnityEngine;

namespace GameSystem.Initialization
{
    internal class InitUnit
    {
        public string SystemName { get; private set; }
        public Action<ScriptableObject, Transform> OnCompleted { get; private set; }

        internal InitUnit(string name, Action<ScriptableObject, Transform> onCompleted)
        {
            SystemName = name;
            OnCompleted = onCompleted;
        }
    }
}
