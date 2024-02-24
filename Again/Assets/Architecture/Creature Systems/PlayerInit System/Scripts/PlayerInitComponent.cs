using UnityEngine;
using UnityEngine.Events;

namespace Creature.Player.PlayerInit
{
    public class PlayerInitComponent : MonoBehaviour
    {
        [SerializeField] private UnityEvent _onCreated;
        [SerializeField] private UnityEvent _onDestroyed;

        private void Awake()
        {
            _onCreated?.Invoke();

            PlayerInitManager.OnCreated();
        }

        private void OnDestroy()
        {
            _onDestroyed?.Invoke();

            PlayerInitManager.OnDestroyed();
        }
    }
}
