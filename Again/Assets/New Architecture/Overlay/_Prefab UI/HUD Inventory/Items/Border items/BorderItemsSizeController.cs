using InventorySystem;
using Property.ObservableProperty;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class BorderItemsSizeController : MonoBehaviour
    {
        [SerializeField] private RectTransform _border;

        [SerializeField, Min(1)] private int _cellsInRow;

        [SerializeField] private Vector2IntObsProperty _maxCells;

        [SerializeField] private Vector2 _cellSize = new(13, 13);
        [SerializeField] private Vector2 _borderSize = new(5.5f, 7.5f);

        private Action OnUnsubscribe;

        public void OnCellMaxChanged(Vector2Int value, Vector2Int oldValue)
        {
            Vector2 borderSize = new(value.x * _cellSize.x + _borderSize.x, value.y * _cellSize.y + _borderSize.y);

            _border.sizeDelta = borderSize;
        }
        private void OnEnableMode(IReadOnlyList<ItemUnit> items)
        {
            int count = items.Count;

            if (count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            int rows = count / _cellsInRow;

            if (count % _cellsInRow > 0) rows++;

            _maxCells.Value = new(_cellsInRow, rows);
        }
        private void OnDisableMode(IReadOnlyList<ItemUnit> items)
        {
            int count = items.Count;

            if (count > 0)
            {
                gameObject.SetActive(true);
            }
        }

        #region Mono
        private void OnEnable()
        {
            OnUnsubscribe?.Invoke();

            InventoryManager.ListenUnitCountChanged(OnEnableMode, true);

            OnUnsubscribe = () => InventoryManager.ListenUnitCountChanged(OnEnableMode, false);

            OnEnableMode(InventoryManager.Items);
        }

        private void OnDisable()
        {
            OnUnsubscribe?.Invoke();

            InventoryManager.ListenUnitCountChanged(OnDisableMode, true);

            OnUnsubscribe = () => InventoryManager.ListenUnitCountChanged(OnDisableMode, false);
        }

        private void OnDestroy()
        {
            OnUnsubscribe?.Invoke();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Vector2Int vector = _maxCells.Value;

            if (vector.x <= 0) vector.x = 1;

            if (vector.y <= 0) vector.y = 1;

            _maxCells.Value = vector;

            _maxCells.Validate();
        }
#endif
        #endregion
    }
}
