using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem.Components
{
    [RequireComponent(typeof(SpriteRenderer))]
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

#if UNITY_EDITOR
        [ContextMenu("Change Icon")]
        private void ChangeIcon()
        {
            var comp = GetComponent<SpriteRenderer>();
            comp.sprite = _item?.Def.Icon;
        }
#endif
    }
}
