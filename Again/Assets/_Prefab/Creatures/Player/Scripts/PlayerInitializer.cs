using System;
using UnityEngine;

namespace Creature.Player
{
    public class PlayerInitializer : MonoBehaviour
    {
        public static event Action OnPlayerCreated;
        public static event Action OnPlayerDestroyed;

        private void Awake()
        {
            OnPlayerCreated?.Invoke();
        }

        private void OnDestroy()
        {
            OnPlayerDestroyed?.Invoke();
        }
    }
}
