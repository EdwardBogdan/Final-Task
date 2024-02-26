using TreasureSystem;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Hud.Treasure
{
    public class TreasureIconWidget : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        public void OnSetItem(TreasureUnit item)
        {
            _icon.sprite = item.Icon;
        }
    }
}
