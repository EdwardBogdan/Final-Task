using System;
using UnityEngine;

namespace GameSystem.Overlay
{
    public abstract class UnitUI : MonoBehaviour
    {
        internal Action _onUnitDead;

        protected virtual void OnDestroy()
        {
            _onUnitDead?.Invoke();
        }
    }
}
