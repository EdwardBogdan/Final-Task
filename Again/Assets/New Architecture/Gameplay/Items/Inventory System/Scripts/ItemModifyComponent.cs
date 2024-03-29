using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem.Components
{
    public class ItemModifyComponent : MonoBehaviour
    {
        [SerializeField] private ItemUnit _item;
        [SerializeField] private bool _adding = true;
        [SerializeField, Min(1)] private int _count = 1;

        [SerializeField] private UnityEvent _OnSuccessful;
        [SerializeField] private UnityEvent _OnFailed;

        public void OnModify()
        {
            bool result = _adding ? _item.TryAdd(_count) : _item.TryRemove(_count);

            var call = result ? _OnSuccessful : _OnFailed;

            call?.Invoke();
        }
    }
}
