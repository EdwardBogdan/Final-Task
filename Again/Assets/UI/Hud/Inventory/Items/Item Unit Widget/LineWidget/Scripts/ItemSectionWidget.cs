using InventorySystem;
using Property.ObservableRangeProperty;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class ItemSectionWidget : MonoBehaviour
    {
        [SerializeField] private SectionWidget sectionPrefab;
        [SerializeField] private IntObsRangeProperty _currentSectionCount;
        [SerializeField] private IntObsRangeProperty _maxSectionCount;

        private List<SectionWidget> line = new();

        private ItemUnit storedItemUnit;

        public void OnSetItem(ItemUnit item)
        {
            Subscribe(false);

            storedItemUnit = item;

            Subscribe(true);

            _maxSectionCount.Value = storedItemUnit.MaxCount;

            _currentSectionCount.MaxRange = _maxSectionCount.Value;

            _currentSectionCount.Value = storedItemUnit.Count;
        }

        #region Subscribe
        private void Subscribe(bool value)
        {
            if (storedItemUnit == null) return;

            storedItemUnit.ListenCountEvent(OnChargesChanged, value);
            storedItemUnit.ListenMaxCountEvent(OnMaxChargesChanged, value);
        }
        private void OnChargesChanged(int value, int oldValue)
        {
            if (value != storedItemUnit.Count) return; // <= FUCKING CRUTCH, FIND WAY TO FIX IT

            _currentSectionCount.Value = storedItemUnit.Count;
        }
        private void OnMaxChargesChanged(int value, int oldValue)
        {
            _maxSectionCount.Value = value;
            _currentSectionCount.MaxRange = _maxSectionCount.Value;
        }
        
        #endregion

        #region Unity Events
        public void OnSetCurrentSectionCount(int value, int oldValue)
        {
            for (int i = 0; i < line.Count; i++)
            {
                line[i].Status = i < value;
            }
        }
        public void OnSetMaxSectionCount(int value, int oldValue)
        {
            int currentCount = line.Count;

            if (value > currentCount)
            {
                for (int i = currentCount; i < value; i++)
                {
                    SectionWidget section = Instantiate(sectionPrefab, transform);
                    line.Add(section);
                }
            }
            else if (value < currentCount)
            {
                int numberOfChildrenToDelete = currentCount - value;

                // Удаляем нужное количество дочерних объектов, начиная с последнего
                for (int i = 0; i < numberOfChildrenToDelete; i++)
                {
                    int lastIndex = line.Count - 1; // Индекс последнего дочернего объекта

                    SectionWidget section = line[lastIndex];

                    line.RemoveAt(lastIndex);

                    Destroy(section.gameObject);
                }
            }

            _currentSectionCount.MaxRange = line.Count;
        }
        #endregion

        #region Mono
        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject foo = transform.GetChild(i).gameObject;

                if (foo.TryGetComponent<SectionWidget>(out var section))
                {
                    line.Add(section);
                }
            }
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            _maxSectionCount.Validate();

            _currentSectionCount.Validate();
        }
#endif
        private void OnDestroy()
        {
            Subscribe(false);
        }
        #endregion
    }
}
