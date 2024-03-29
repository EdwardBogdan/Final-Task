using TreasureSystem;
using UnityEngine;

namespace UIManagment.Windows.Components
{
    public class BorderTreasure : MonoBehaviour
    {
        [SerializeField] private RectTransform _border;

        [SerializeField] private Vector2 _cellSize = new(13, 13);
        [SerializeField] private Vector2 _borderSize = new(5.5f, 7.5f);

        private void ChangeCellCount(int count)
        {
            Vector2 borderSize = new(_cellSize.x + _borderSize.x, count * _cellSize.y + _borderSize.y);

            _border.sizeDelta = borderSize;
        }

        #region Mono
        private void Awake()
        {
            int count = TreasureManager.TreasureList.Count;

            ChangeCellCount(count);
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            int count = TreasureManager.TreasureList.Count;

            ChangeCellCount(count);
        }
#endif
        #endregion
    }
}
