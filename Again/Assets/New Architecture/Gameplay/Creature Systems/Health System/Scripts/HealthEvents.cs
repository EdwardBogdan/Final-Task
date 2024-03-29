using System;
using UnityEngine;
using UnityEngine.Events;

namespace HealthSystem
{
    [Serializable]
    internal class HealthEvents
    {
        [SerializeField] internal UnityEvent<int, int> OnCountChanged = new();

        [SerializeField] internal UnityEvent<int, int> OnMaxChanged = new();

        [SerializeField] internal UnityEvent OnHit;
        [SerializeField] internal UnityEvent OnHeal;
        [SerializeField] internal UnityEvent OnDeath;
    }
}
