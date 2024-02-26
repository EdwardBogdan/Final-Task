using TreasureSystem;
using UnityEngine;
using UnityEngine.Events;

namespace UI.Hud.Inventory
{
    public class TreasureUnitWidget : MonoBehaviour
    {
        [SerializeField] private UnityEvent<TreasureUnit> OnSetItem;

        public TreasureUnit TreasureTarget
        {
            set => OnSetItem?.Invoke(value);
        }
    }
}
