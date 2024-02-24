using Property.TimeProperty;
using UnityEngine;
using UnityEngine.Events;

namespace InventorySystem.ItemProperies
{
    public abstract class ItemProperty : ScriptableObject
    {
        [SerializeField] protected int _consumption = 1;

        #region Cooldown
        [SerializeField] protected Cooldown _useCooldown;
        public bool ReloadReady => _useCooldown.IsReady;
        public float ReloadTime => _useCooldown.Duration;
        public float ReloadStage => _useCooldown.Stage;
        #endregion

        [SerializeField] protected UnityEvent _onUsedEvent;
        internal virtual void OnUsed(ItemUnit item)
        {
            if (!ReloadReady) return;

            if (item.TryRemove(_consumption))
            {
                _useCooldown.Reset();
                _onUsedEvent?.Invoke();
            }
        }

        public void ListenUsing(UnityAction func, bool mode)
        {
            if (mode) _onUsedEvent.AddListener(func);
            else _onUsedEvent.RemoveListener(func);
        }

        private void OnEnable()
        {
            _useCooldown.Ready();
        }
    }
}