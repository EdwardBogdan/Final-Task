using System;
using UnityEngine;

namespace DeepSystem.Initialization
{
    internal class InitUnit
    {
        public string SystemName { get; private set; }
        public Action<ScriptableObject> OnCompleted { get; private set; }

        internal InitUnit(string name, Action<ScriptableObject> onCompleted)
        {
            SystemName = name;
            OnCompleted = onCompleted;
        }
    }
}
