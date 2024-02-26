using TreasureSystem;
using UI.Hud.Inventory;
using UnityEngine;

namespace UI.Hud.Treasure
{
    public class TreasureUnitWidgetManager : MonoBehaviour
    {
        [SerializeField] private TreasureUnitWidget _prefab;

        private void Awake()
        {
            var list = TreasureManager.TreasureList;

            foreach (var item in list)
            {
                TreasureUnitWidget widget = Instantiate(_prefab, transform);
                widget.TreasureTarget = item;
            }
        }
    }
}
