using Property.TimeProperty;
using UnityEngine;

namespace InventorySystem.ItemProperies
{
    public abstract class ItemProperty : ScriptableObject
    {
        [SerializeField] protected int _consumption = 1;
        [SerializeField] protected Cooldown _useCooldown;
        public void OnUsed(ItemUnit item)
        {
            if (item.TryRemove(_consumption))
            {
                OnUsedProperty();
            }
        }

        protected abstract void OnUsedProperty();
    }
}