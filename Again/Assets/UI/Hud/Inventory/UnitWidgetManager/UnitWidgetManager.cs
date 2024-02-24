using InventorySystem;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Hud.Inventory
{
    public class UnitWidgetManager : MonoBehaviour
    {
        [SerializeField] private ItemUnitWidget _prefab;

        private Action OnUnsubscribe;

        private List<ItemUnitWidget> line = new();

        private void Awake()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                GameObject foo = transform.GetChild(i).gameObject;

                if (foo.TryGetComponent<ItemUnitWidget>(out var section))
                {
                    line.Add(section);
                }
            }
        }

        private void OnEnableMode(IReadOnlyList<ItemUnit> items)
        {
            int count = items.Count;

            if(count <= 0)
            {
                gameObject.SetActive(false);
                return;
            }

            int currentCount = line.Count;

            // Увеличиваем количество виджетов
            if (count > currentCount)
            {
                for (int i = currentCount; i < count; i++)
                {
                    ItemUnitWidget foo = Instantiate(_prefab, transform);
                    line.Add(foo);
                }
            }

            // Сокращаем количество виджетов
            else if (count < currentCount)
            {
               int numberOfChildrenToDelete = currentCount - count;
           
               // Удаляем нужное количество дочерних объектов, начиная с последнего
                for (int i = 0; i < numberOfChildrenToDelete; i++)
                {
                    int lastIndex = transform.childCount - 1; // Индекс последнего дочернего объекта

                    GameObject widget = line[lastIndex].gameObject;

                    line.RemoveAt(lastIndex);

                    Destroy(widget);
                }
            }

            //Устанавливаем данные в виджеты
            for (int i = 0; i < count; i++)
            {
                ItemUnitWidget widget = line[i];

                ItemUnit item = items[i];

                widget.ItemTarget = item;
            }
        }
        private void OnDisableMode(IReadOnlyList<ItemUnit> items)
        {
            int count = items.Count;

            if (count > 0)
            {
                gameObject.SetActive(true);
            }
        }


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
    }
}
