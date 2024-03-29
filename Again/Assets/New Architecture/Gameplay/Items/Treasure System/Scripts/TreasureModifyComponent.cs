using UnityEngine;
using UnityEngine.Events;

namespace TreasureSystem.Components
{
    public class TreasureModifyComponent : MonoBehaviour
    {
        [SerializeField] private TreasureUnit _unit;
        [SerializeField] private bool _adding = true;
        [SerializeField, Min(1)] private int _count = 1;

        [SerializeField] private UnityEvent _OnSuccessful;
        [SerializeField] private UnityEvent _OnFailed;

        public void OnModify()
        {
            bool result = _adding ? _unit.TryAdd(_count) : _unit.TryRemove(_count);

            var call = result ? _OnSuccessful : _OnFailed;

            call?.Invoke();
        }
    }
}
